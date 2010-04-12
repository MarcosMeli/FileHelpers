

using System;

namespace FileHelpers.Events
{
#if ! MINI

	/// <summary>Class used to notify the current progress position and other context info.</summary>
	public class ProgressEventArgs
        :EventArgs
	{
	    public double Percent { get; private set; }
        public int CurrentRecord { get; private set; }
	    //public int ReadBytes { get; private set; }
	    public int TotalRecords { get; private set; }
	    public long CurrentBytes { get; private set; }
	    public long TotalBytes { get; private set; }

	    internal ProgressEventArgs(int currentRecord, int totalRecords)
                    :this (currentRecord, totalRecords, -1, -1)
        	    {
        	    }

	    internal ProgressEventArgs(int currentRecord, int totalRecords, long currentBytes, long totalBytes)
        {
            CurrentRecord = currentRecord;
	        TotalRecords = totalRecords;
	        CurrentBytes = currentBytes;
	        TotalBytes = totalBytes;

	        if (totalRecords > 0)
                Percent = currentRecord / (double)totalRecords * 100.0;
            else if (totalBytes > 0)
                Percent = currentBytes / (double)totalBytes * 100.0;
            else

                Percent = -1;

	    }
	}

#endif
}