#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldInNewLine: Attribute
	{
		public FieldInNewLine()
		{}
	}
}