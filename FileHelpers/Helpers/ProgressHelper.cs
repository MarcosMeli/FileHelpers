#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	internal sealed class ProgressHelper
	{
		private ProgressHelper()
		{}

		public static void Notify(ProgressChangeHandler handler, ProgressMode mode, int current, int total)
		{
			if (handler == null)
				return;

			if (mode == ProgressMode.DontNotify)
				return;

			switch(mode)
			{
				case ProgressMode.NotifyBytes:
					handler(new ProgressEventArgs(mode, current, total));
					break;

				case ProgressMode.NotifyRecords:
					handler(new ProgressEventArgs(mode, current, total));
					break;

				case ProgressMode.NotifyPercent:
					if (total == -1)
						return;
					handler(new ProgressEventArgs(mode, (int) (current*100/total), 100));
					break;

			}

		}
	}
}