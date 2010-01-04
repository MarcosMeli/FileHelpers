#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

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

	    internal ProgressEventArgs(int currentRecord, int totalRecords)
        {

            CurrentRecord = currentRecord;
	        TotalRecords = totalRecords;

            if (totalRecords > 0)
                Percent = currentRecord / (double)totalRecords * 100.0;
            else
                Percent = -1;

	    }
	}

#endif
}