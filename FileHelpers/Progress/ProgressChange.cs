#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers
{
#if ! MINI

	/// <summary>Class used to notify the current progress position and other context info.</summary>
	public class ProgressEventArgs
	{
		internal ProgressEventArgs(ProgressMode mode, int current, int total)
		{
			mProgressMode = mode;
			mProgressCurrent = current;
			mProgressTotal = total;
		}

		internal ProgressEventArgs()
		{
			mProgressMode = ProgressMode.DontNotify;
		}


		private int mProgressCurrent;
		private int mProgressTotal;
		private ProgressMode mProgressMode = ProgressMode.DontNotify;

		/// <summary>The current progress position. Check also the ProgressMode property.</summary>
		public int ProgressCurrent
		{
			get { return mProgressCurrent; }
		}

		/// <summary>The total when the progress finish. (<b>-1 means undefined</b>)</summary>
		public int ProgressTotal
		{
			get { return mProgressTotal; }
		}

		/// <summary>The ProgressMode used.</summary>
		public ProgressMode ProgressMode
		{
			get { return mProgressMode; }
		}
	}

	/// <summary>Delegate used to notify progress to the user.</summary>
	/// <param name="e">The Event args with information about the progress.</param>
	public delegate void ProgressChangeHandler(ProgressEventArgs e);

#endif
}