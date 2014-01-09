using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
#if ! MINI

    /// <summary>Class used to notify the current progress position and other context info.</summary>
    public class ProgressEventArgs
        : EventArgs
    {
        /// <summary>
        /// Percentage of the file complete (estimate or completion time)
        /// </summary>
        public double Percent { get; private set; }

        /// <summary>
        /// Number of the record being processed
        /// </summary>
        public int CurrentRecord { get; private set; }

        //public int ReadBytes { get; private set; }

        /// <summary>
        /// Total records in the file  (-1 is unknown)
        /// </summary>
        public int TotalRecords { get; private set; }

        /// <summary>
        /// Current position in the file
        /// </summary>
        public long CurrentBytes { get; private set; }

        /// <summary>
        /// Total bytes in the file
        /// </summary>
        public long TotalBytes { get; private set; }

        /// <summary>
        /// Create a progress event argument
        /// </summary>
        /// <param name="currentRecord">Current record in file</param>
        /// <param name="totalRecords">Total records in file</param>
        public ProgressEventArgs(int currentRecord, int totalRecords)
            : this(currentRecord, totalRecords, -1, -1) {}

        /// <summary>
        /// Create a progress event argument
        /// </summary>
        /// <param name="currentRecord">Current record in file</param>
        /// <param name="totalRecords">Total number of records in file</param>
        /// <param name="currentBytes">Current position in bytes</param>
        /// <param name="totalBytes">Total bytes in file</param>
        public ProgressEventArgs(int currentRecord, int totalRecords, long currentBytes, long totalBytes)
        {
            CurrentRecord = currentRecord;
            TotalRecords = totalRecords;
            CurrentBytes = currentBytes;
            TotalBytes = totalBytes;

            if (totalRecords > 0)
                Percent = currentRecord/(double) totalRecords*100.0;
            else if (totalBytes > 0)
                Percent = currentBytes/(double) totalBytes*100.0;
            else

                Percent = -1;
        }
    }

#endif
}