#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{

	/// <summary>
	/// Interface used to provide record type transformations
	/// </summary>
	public interface ITransformable<T>
	{
		/// <summary>
		/// Method called to transform the current record to Type T.
		/// </summary>
		T TransformTo();
	}

}
