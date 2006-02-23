#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates that this class represents a delimited record. </summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DelimitedRecordAttribute : TypedRecordAttribute
	{
		private string mSeparator;

		/// <summary>The separator string used to split the fields of the record.</summary>
		public string Separator
		{
			get { return mSeparator; }
		}

		/// <summary>Indicates that this class represents a delimited record. </summary>
		/// <param name="sep">The separator string used to split the fields of the record.</param>
		public DelimitedRecordAttribute(string sep) : this(sep, String.Empty)
		{
		}

		/// <summary>Indicates that this class represents a delimited record. </summary>
		/// <param name="sep">The separator string used to split the fields of the record.</param>
		/// <param name="description">The description of the record. (for future use)</param>
		public DelimitedRecordAttribute(string sep, string description) : base(description)
		{
			if (mSeparator != String.Empty)
				this.mSeparator = sep;
			else
				throw new ArgumentException("sep debe ser <> \"\"");
		}

	}
}