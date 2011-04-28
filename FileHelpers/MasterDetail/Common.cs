namespace FileHelpers.MasterDetail
{
	#region "  Delegate  "

	/// <summary>
	/// Delegate thats determines the Type of the current record (Master, Detail, Skip)
	/// </summary>
	/// <param name="recordString">The string of the current record.</param>
	/// <returns>the action used for the current record (Master, Detail, Skip)</returns>
	public delegate RecordAction MasterDetailSelector(string recordString);

	#endregion

	#region "  Common Actions and Selector  "

	/// <summary>The Action taken when the selector string is found.</summary>
	public enum CommonSelector
	{
		/// <summary>Parse the current record as <b>Master</b> if the selector string is found.</summary>
		MasterIfContains,
		/// <summary>Parse the current record as <b>Master</b> if the record starts with some string.</summary>
		MasterIfBegins,
		/// <summary>Parse the current record as <b>Master</b> if the record ends with some string.</summary>
		MasterIfEnds,
		/// <summary>Parse the current record as <b>Master</b> if the record begins and ends with some string.</summary>
		MasterIfEnclosed,
		/// <summary>Parse the current record as <b>Detail</b> if the selector string is found.</summary>
		DetailIfContains,
		/// <summary>Parse the current record as <b>Detail</b> if the record starts with some string.</summary>
		DetailIfBegins,
		/// <summary>Parse the current record as <b>Detail</b> if the record ends with some string.</summary>
		DetailIfEnds,
		/// <summary>Parse the current record as <b>Detail</b> if the record begins and ends with some string.</summary>
		DetailIfEnclosed
	}   


	#endregion
}