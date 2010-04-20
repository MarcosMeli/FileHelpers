using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileHelpers
{

    // Based on this code: http://splinter.com.au/blog/?p=142

    /// <summary>
    /// This class help to sort really big files using the External Sorting algorithm
    /// http://en.wikipedia.org/wiki/External_sorting
    /// </summary>
    public sealed class ExternalFileSorter
    {
        /// <summary> The directory for the temp files (by the default the same of sourceFile) </summary>
        public string WorkingDirectory { get; set; }
        
        /// <summary> The Size of each block that will be sorted in memory and later merged all togheter </summary>
        public int BlockFileSize { get; set; }

        /// <summary> Indicates if the temporary files will be deleted (True by default) </summary>
        public bool DeleteTempFiles { get; set; }

        /// <summary> The Encoding used to read and write the files </summary>
        public Encoding Encoding { get; set; }

        private static Encoding DefaultEncoding { get; set; }

        static ExternalFileSorter()
        {
            DefaultEncoding = new UTF8Encoding(false, true);
        }

        public ExternalFileSorter()
            :this(40 * 1024 * 1024) // 40Mb default size
        {
        }

        public ExternalFileSorter(int blockFileSize)
        {
            BlockFileSize = blockFileSize;
            Encoding = DefaultEncoding;
            DeleteTempFiles = true;
        }

        public void Sort(string sourceFile, string destinationFile, Comparison<string> sorter)
        {
            var parts = SplitAndSortParts(sourceFile, sorter);
            MergeTheChunks(parts, sorter, destinationFile);
        }

        private List<string> SplitAndSortParts(string file, Comparison<string> sorter)
        {
            int partNumber = 1;
            var res = new List<string>();
            var splitName = GetSplitName(file, partNumber);
            res.Add(splitName);
            //var sw = CreateStream(splitName, BlockFileSize / 3);

            var lines = new List<string>();
            try
            {

                var writtenBytes = 0;
                using (var sr = new StreamReader(file, Encoding, true,  EngineBase.DefaultReadBufferSize * 10))
                {
                    while (sr.Peek() >= 0)
                    {
                        var line = sr.ReadLine();
                        lines.Add(line);
                        writtenBytes += line.Length + 2;

                        if (writtenBytes > BlockFileSize && sr.Peek() >= 0)
                        {
                            splitName = GetSplitName(file, partNumber);
                            res.Add(splitName);
                            lines.Sort(sorter);
                            File.WriteAllLines(splitName, lines.ToArray(), Encoding);
                            partNumber++;
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
                    splitName = GetSplitName(file, partNumber);
                    res.Add(splitName);
                    lines.Sort(sorter);
                    File.WriteAllLines(splitName, lines.ToArray(), Encoding);
                }
            }

        }

        private const int MaxBufferSize = 25 * 1024 * 1024; // 25 Mb
        private StreamWriter CreateStream(string filename, int bufferSize)
        {
            return new StreamWriter(filename, false, Encoding, Math.Min(MaxBufferSize, bufferSize));
        }

        private string GetSplitName(string file, int splitNum)
        {
            var dir = WorkingDirectory;
            if (string.IsNullOrEmpty(dir))
            {
                dir = Path.GetDirectoryName(file);
            }

            return Path.Combine(dir,
                                Path.GetFileNameWithoutExtension(file) + ".part" + splitNum.ToString().PadLeft(4, '0'));
        }


        private void MergeTheChunks(List<string> parts, Comparison<string> sorter, string destinationFile)
        {
            // Open the files
            var readers = new StreamReader[parts.Count];
            for (int i = 0; i < parts.Count; i++)
                readers[i] = new StreamReader(parts[i], Encoding, true, EngineBase.DefaultReadBufferSize * 4);

            // Make the queues
            var queues = new Queue<string>[parts.Count];
            for (int i = 0; i < parts.Count; i++)
                queues[i] = new Queue<string>(parts.Count);

            // Load the queues
            for (int i = 0; i < parts.Count; i++)
                LoadQueue(queues[i], readers[i], BlockFileSize / parts.Count);

            // Merge!
            var sw = CreateStream(destinationFile, BlockFileSize / 2);
            bool done = false;
            int lowest_index, j= 0;
            string lowest_value;
            while (!done)
            {
                // Find the chunk with the lowest value
                lowest_index = -1;
                lowest_value = "";
                for (j = 0; j < parts.Count; j++)
                {
                    if (queues[j] != null)
                    {
                        if (lowest_index < 0 || sorter(queues[j].Peek(), lowest_value) < 0)
                        {
                            lowest_index = j;
                            lowest_value = queues[j].Peek();
                        }
                    }
                }

                // Was nothing found in any queue? We must be done then.
                if (lowest_index == -1) { done = true; break; }

                // Output it
                sw.WriteLine(lowest_value);

                // Remove from queue
                queues[lowest_index].Dequeue();
                // Have we emptied the queue? Top it up
                if (queues[lowest_index].Count == 0)
                {
                    LoadQueue(queues[lowest_index], readers[lowest_index], BlockFileSize / parts.Count);
                    // Was there nothing left to read?
                    if (queues[lowest_index].Count == 0)
                    {
                        queues[lowest_index] = null;
                    }
                }
            }
            sw.Close();

            // Close and delete the files
            for (int i = 0; i < parts.Count; i++)
            {
                readers[i].Close();
                if (DeleteTempFiles)
                    File.Delete(parts[i]);
            }

        }

        /// <summary>
        /// Loads up to a number of records into a queue
        /// </summary>
        static void LoadQueue(Queue<string> queue, StreamReader file, int maxsize)
        {
            var bytes = 0;
            while (true)
            {
                if (file.Peek() < 0 || bytes > maxsize) break;
                
                var line = file.ReadLine();
                bytes += line.Length + 2;
                queue.Enqueue(line);
            }
        }

 
    }
}
