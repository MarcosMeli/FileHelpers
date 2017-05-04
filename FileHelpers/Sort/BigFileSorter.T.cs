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
    /// <typeparam name="T">Type of record to sort</typeparam>
    public class BigFileSorter<T>
        where T : class, IComparable<T>
    {
        /// <summary>
        /// set the default encoding to the default encoding for C#
        /// </summary>
        static BigFileSorter()
        {
            DefaultEncoding = Encoding.Default;
        }

        /// <summary>
        /// Default block size,  10 meg
        /// </summary>
        private const int DefaultBlockSize = 10*1024*1024;

        /// <summary>
        /// Maximum block size,  50 meg
        /// </summary>
        private const int MaxBufferSize = 40*1024*1024;

        /// <summary>
        /// Minimum block size 0.5 meg
        /// </summary>
        private const int MinBlockSize = 1*1024*1024 / 2;

        /// <summary>
        /// Ratio of file size to block size, used when auto assigning block size
        /// </summary>
        private const int AutoBlockSizeRatio = 10;

        /// <summary>
        /// Comparison operator for this sort
        /// </summary>
        private readonly Comparison<T> mSorter;

        /// <summary>
        /// Indicates if the block size should be determined based on file size
        /// </summary>
        private bool AutoSetBlockSize = false;

        /// <summary>
        /// Sort big files using the External Sorting algorithm
        /// </summary>
        public BigFileSorter()
            : this(0) {}

        /// <summary>
        /// Sort a large file
        /// </summary>
        /// <param name="blockFileSizeInBytes">block size for sort to work on</param>
        public BigFileSorter(int blockFileSizeInBytes)
            : this(null, blockFileSizeInBytes) {}

        /// <summary>
        /// Large file sorter
        /// </summary>
        /// <param name="encoding">Encoding of the file</param>
        public BigFileSorter(Encoding encoding)
            : this(encoding, 0) {}

        /// <summary>
        /// Large file sorter
        /// </summary>
        /// <param name="encoding">Encoding of the file</param>
        /// <param name="blockFileSizeInBytes">Size of the blocks in bytes</param>
        public BigFileSorter(Encoding encoding, int blockFileSizeInBytes)
            : this(null, encoding, blockFileSizeInBytes) {}

        /// <summary>
        /// Large file sorter specifying the comparison operator
        /// </summary>
        /// <param name="sorter">Comparison operator</param>
        /// <param name="encoding">Encoding of the file</param>
        /// <param name="blockFileSizeInBytes">Block size to work on</param>
        internal BigFileSorter(Comparison<T> sorter, Encoding encoding, int blockFileSizeInBytes)
        {
            mSorter = sorter;
            if (blockFileSizeInBytes <= 0)
            {
                blockFileSizeInBytes = DefaultBlockSize;
                AutoSetBlockSize = true;
            }
            else if (blockFileSizeInBytes < MinBlockSize)
                blockFileSizeInBytes = MinBlockSize;
            else if (blockFileSizeInBytes > MaxBufferSize)
                blockFileSizeInBytes = MaxBufferSize;

            BlockFileSizeInBytes = blockFileSizeInBytes;

            if (encoding == null)
                encoding = DefaultEncoding;

            Encoding = encoding;
            RunGcCollectForEachPart = true;
            DeleteTempFiles = true;
        }


        private static Encoding DefaultEncoding { get; set; }

        /// <summary>
        /// The directory for the temp files (by the default the same directory
        /// as sourceFile)
        /// </summary>
        public string TempDirectory { get; set; }

        /// <summary>
        /// The Size of each block that will be sorted in memory which are
        /// later merged together
        /// </summary>
        public int BlockFileSizeInBytes { get; set; }

        /// <summary>
        /// Indicates if the temporary files will be deleted (True by default)
        /// </summary>
        public bool DeleteTempFiles { get; set; }

        /// <summary> The Encoding used to read and write the files </summary>
        public Encoding Encoding { get; set; }

        /// <summary> Indicates if the Sorter run a GC.Collect() after sort and write each part. Default is true.</summary>
        public bool RunGcCollectForEachPart { get; set; }


        /// <summary>
        /// Sort a file from one filename to another filename
        /// </summary>
        /// <remarks>Handles very large files</remarks>
        /// <param name="sourceFile">File to read in</param>
        /// <param name="destinationFile">File to write out</param>
        public void Sort(string sourceFile, string destinationFile)
        {
            if (AutoSetBlockSize) BlockFileSizeInBytes = BlockSizeFromFileSize(sourceFile);
            var parts = new List<string>();
            var engine = SplitAndSortParts(sourceFile, parts);
            var queues = CreateQueues(parts);
            MergeTheChunks(queues, destinationFile, engine.HeaderText, engine.FooterText);
        }

        private SortQueue<T>[] CreateQueues(List<string> parts)
        {
            var res = new SortQueue<T>[parts.Count];
            for (int i = 0; i < parts.Count; i++)
                res[i] = new SortQueue<T>(Encoding, parts[i], DeleteTempFiles);
            return res;
        }

        private FileHelperAsyncEngine<T> SplitAndSortParts(string file, List<string> res)
        {
            int partNumber = 1;

            var lines = new List<T>();

            try {
                long writtenBytes = 0;
                long lastWrittenBytes = 0;
                var readEngine = new FileHelperAsyncEngine<T>(Encoding);

                readEngine.Progress += (sender, e) => writtenBytes = e.CurrentBytes;

                using (readEngine.BeginReadFile(file, EngineBase.DefaultReadBufferSize*2)) {
                    foreach (var item in readEngine) {
                        lines.Add(item);

                        if ((writtenBytes - lastWrittenBytes) > BlockFileSizeInBytes) {
                            WritePart(file, lines, partNumber, res);
                            partNumber++;
                            lastWrittenBytes = writtenBytes;
                        }
                    }
                }

                return readEngine;
            }
            finally {
                if (lines.Count > 0)
                    WritePart(file, lines, partNumber, res);
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

            var writeEngine = new FileHelperEngine<T>(Encoding) {
                Options = {
                    IgnoreFirstLines = 0,
                    IgnoreLastLines = 0
                }
            };
            writeEngine.WriteFile(splitName, lines);

            lines.Clear();

            if (RunGcCollectForEachPart)
                GC.Collect();
        }

        /// <summary>
        /// Returns a block size based on the source file size
        /// </summary>
        /// <param name="sourceFile">Source file path</param>
        /// <returns></returns>
        private int BlockSizeFromFileSize(string sourceFile) {

            int blockSize = DefaultBlockSize;

            var fi = new FileInfo(sourceFile);
            var fileSize = fi.Length;

            var initialBlockSize = (int)(fileSize / AutoBlockSizeRatio);
            if (initialBlockSize >= MinBlockSize && initialBlockSize <= MaxBufferSize)
                blockSize = initialBlockSize;
            else if (initialBlockSize < MinBlockSize)
                blockSize = MinBlockSize;
            else
                blockSize = MaxBufferSize;

            return blockSize;
        }

        /// <summary>
        /// Create a StreamWriter given the filename and buffer size
        /// </summary>
        /// <param name="filename">Filename to write</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <returns>StreamWriter object</returns>
        protected StreamWriter CreateStream(string filename, int bufferSize)
        {
            return new StreamWriter(filename, false, Encoding, Math.Min(MaxBufferSize, bufferSize));
        }

        /// <summary>
        /// Create a part of a file,  files are split into chunks for sort
        /// processing
        /// </summary>
        /// <param name="file">Filename (prefix)</param>
        /// <param name="splitNum">This chunk number</param>
        /// <returns>revised filename to write</returns>
        protected string GetSplitName(string file, int splitNum)
        {
            var dir = TempDirectory;
            if (string.IsNullOrEmpty(dir))
                dir = Path.GetDirectoryName(file);

            return Path.Combine(dir,
                Path.GetFileNameWithoutExtension(file) + ".part" + splitNum.ToString().PadLeft(4, '0'));
        }

        /// <summary>
        /// Large files are sorted in chunk then merged together in the final process
        /// </summary>
        /// <param name="queues">list of chunks to merge</param>
        /// <param name="destinationFile">output filename</param>
        /// <param name="headerText">The header of the destinationFile</param>
        /// <param name="footerText">The footer of the destinationFile</param>
        internal void MergeTheChunks(SortQueue<T>[] queues, string destinationFile, string headerText, string footerText)
        {
            try {
                // Merge!
                using (var sw = new FileHelperAsyncEngine<T>(Encoding)) {
                    sw.HeaderText = headerText;
                    sw.FooterText = footerText;
                    sw.BeginWriteFile(destinationFile, EngineBase.DefaultWriteBufferSize*4);
                    while (true) {
                        // Find the chunk with the lowest value
                        int lowestIndex = -1;
                        T lowestValue = null;

                        for (int j = 0; j < queues.Length; j++) {
                            var current = queues[j].Current;

                            if (current != null) {
                                if (lowestIndex < 0 ||
                                    current.CompareTo(lowestValue) < 0) {
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
            finally {
                for (int i = 0; i < queues.Length; i++)
                    queues[i].Dispose();
            }
        }

    }
}