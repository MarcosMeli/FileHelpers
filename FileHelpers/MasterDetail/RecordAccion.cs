#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers.MasterDetail
{
	/// <summary>Used with the <see cref="MasterDetailSelector"/> Delegate to determines the action used by the <see cref="MasterDetailEngine"/></summary>
	public enum RecordAction
	{
		/// <summary><b>Ignore</b> the current record.</summary>
		Skip = 0,
		/// <summary>Use the current record as <b>Master</b>.</summary>
		Master,
		/// <summary>Use the current record as <b>Detail</b>.</summary>
		Detail
	}
}