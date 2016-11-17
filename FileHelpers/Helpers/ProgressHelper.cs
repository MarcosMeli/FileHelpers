

using FileHelpers.Events;
using System;

namespace FileHelpers
{
	internal static class ProgressHelper
	{
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