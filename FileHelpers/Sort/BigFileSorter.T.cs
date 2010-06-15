using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// This class help to sort really big files using the External Sorting algorithm
    /// http://en.wikipedia.org/wiki/External_sorting
    /// </summary>
    public class BigFileSorter<T> 
        where T: class, IComparable<T>
    {
        static BigFileSorter()
        {
            DefaultEncoding = new UTF8Encoding(false, true);
        }
        private const int DefaultBlockSize = 10 * 1024 * 1024;
        private const int MaxBufferSize = 50 * 1024 * 1024;
        private const int MinBlockSize = 2 * 1024 * 1024;
        private readonly Comparison<T> mSorter;

        public BigFileSorter()
            : this(0)
        {
        }

        public BigFileSorter(int blockFileSizeInBytes)
            :this(null , blockFileSizeInBytes)
        {
        }

        public BigFileSorter(Encoding encoding)
            : this(encoding, 0)
        {
        }

        public BigFileSorter(Encoding encoding, int blockFileSizeInBytes)
            :this(null, encoding, blockFileSizeInBytes)
        {
            
        }

        internal BigFileSorter(Comparison<T> sorter, Encoding encoding, int blockFileSizeInBytes)
        {
            mSorter = sorter;
            if (blockFileSizeInBytes <= 0)
                blockFileSizeInBytes = DefaultBlockSize;
            else if (blockFileSizeInBytes < MinBlockSize)
                blockFileSizeInBytes = MinBlockSize;
            else if (blockFileSizeInBytes > MaxBufferSize)
                blockFileSizeInBytes = MaxBufferSize;

            BlockFileSizeInBytes = blockFileSizeInBytes;

            if (encoding == null)
                encoding = DefaultEncoding;

            Encoding = encoding;
            DeleteTempFiles = true;

        }

        private static Encoding DefaultEncoding { get; set; }

        /// <summary> The directory for the temp files (by the default the same of sourceFile) </summary>
        public string WorkingDirectory { get; set; }

        /// <summary> The Size of each block that will be sorted in memory and later merged all togheter </summary>
        public int BlockFileSizeInBytes { get; set; }

        /// <summary> Indicates if the temporary files will be deleted (True by default) </summary>
        public bool DeleteTempFiles { get; set; }

        /// <summary> The Encoding used to read and write the files </summary>
        public Encoding Encoding { get; set; }

        public void Sort(string sourceFile, string destinationFile)
        {
            var parts = SplitAndSortParts(sourceFile);
            var queues = CreateQueues(parts);
            MergeTheChunks(queues, destinationFile);
        }

        private SortQueue<T>[] CreateQueues(List<string> parts)
        {
            var res = new SortQueue<T>[parts.Count];
            for (int i = 0; i < parts.Count; i++)
            {
                res[i] = new SortQueue<T>(Encoding, parts[i], DeleteTempFiles);
            }
            return res;
        }

        private List<string> SplitAndSortParts(string file)
        {
            int partNumber = 1;
            var res = new List<string>();

            var lines = new List<T>();

            try
            {
                long writtenBytes = 0;
                long lastWrittenBytes = 0;
                var readEngine = new FileHelperAsyncEngine<T>(Encoding);
                
                readEngine.Progress += (sender, e) => writtenBytes = e.TotalBytes;
                
                using (readEngine.BeginReadFile(file, EngineBase.DefaultReadBufferSize * 10))
                {
                    foreach (var item in readEngine)
                    {
                        lines.Add(item);

                        if ((lastWrittenBytes - writtenBytes) > BlockFileSizeInBytes)
                        {
                            WritePart(file, lines, partNumber, res);
                            partNumber++;
                            lastWrittenBytes += writtenBytes;
                            writtenBytes = 0;
                            lines.Clear();

                            GC.Collect();
                        }
                    }
                }

                return res;
            }
            finally
            {
                if (lines.Count > 0)
                {
                    WritePart(file, lines, partNumber, res);
                }
            }

        }

        private void WritePart(string file, List<T> lines, int partNumber, List<string> res)
        {
            var splitName = GetSplitName(file, partNumber);
            res.Add(splitName);

            if (mSorter != null)
                lines.Sort(mSorter);
            else
                lines.Sort();

            var writeEngine = new FileHelperEngine<T>(Encoding);
            writeEngine.WriteFile(splitName, lines);
        }


        protected StreamWriter CreateStream(string filename, int bufferSize)
        {
            return new StreamWriter(filename, false, Encoding, Math.Min(MaxBufferSize, bufferSize));
        }

        protected string GetSplitName(string file, int splitNum)
        {
            var dir = WorkingDirectory;
            if (string.IsNullOrEmpty(dir))
            {
                dir = Path.GetDirectoryName(file);
            }

            return Path.Combine(dir,
                                Path.GetFileNameWithoutExtension(file) + ".part" + splitNum.ToString().PadLeft(4, '0'));
        }

        internal void MergeTheChunks(SortQueue<T>[] queues, string destinationFile)
        {
   
            try
            {
                // Merge!
                using (var sw = new FileHelperAsyncEngine<T>(Encoding))
                {
                    sw.BeginWriteFile(destinationFile, EngineBase.DefaultWriteBufferSize * 4);
                    while (true)
                    {
                        // Find the chunk with the lowest value
                        int lowestIndex = -1;
                        T lowestValue = null;

                        for (int j = 0; j < queues.Length; j++)
                        {
                            var current = queues[j].Current;

                            if (current != null)
                            {
                                if (lowestIndex < 0 || current.CompareTo(lowestValue) < 0)
                                {
                                    lowestIndex = j;
                                    lowestValue = current;
                                }
                            }
                        }

                        // Was nothing found in any queue? We must be done then.
                        if (lowestIndex == -1)
                            break;

                        sw.WriteNext(lowestValue);

                        // Remove from queue
                        queues[lowestIndex].MoveNext();
                    }
                }
            }
            finally
            {
                for (int i = 0; i < queues.Length; i++)
                    queues[i].Dispose();
            }


        }
    }
}