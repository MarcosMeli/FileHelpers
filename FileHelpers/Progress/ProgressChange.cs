namespace FileHelpers
{
#if ! MINI

	public class ProgressEventArgs
	{
		int CurrentProgress;
		int TotalProgress;
		ProgressMode ProgressMode = ProgressMode.DontNotify;
	}

	public delegate void ProgressChange(ProgressEventArgs e);

#endif
}