#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the visibility of a member.</summary>
	public enum NetVisibility
	{
		/// <summary>Public visivility.</summary>
		Public = 0,
		/// <summary>Internal visivility. (Friend in VB.NET)</summary>
		Internal,
		/// <summary>Protected visivility.</summary>
		Protected,
		/// <summary>Private visivility.</summary>
		Private
	}
}
