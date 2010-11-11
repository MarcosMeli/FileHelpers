

namespace FileHelpers
{

	/// <summary>Indicate the method used to calculate the current progress</summary>

	public enum ProgressMode
	{
		/// <summary>Notify the percent completed.</summary>
		NotifyPercent,
		/// <summary>Notify the Record completed.</summary>
		NotifyRecords,
		/// <summary>Notify the bytes read so far</summary>
		NotifyBytes,
		/// <summary>Don't call to the progress handler.</summary>
		DontNotify = 0
	}
}