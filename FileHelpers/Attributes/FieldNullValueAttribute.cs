#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the value to assign to the field in the case of find a "NULL".</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldNullValueAttribute : Attribute
	{
		private object mNullValue;

		/// <summary>The null value used when the file has a null string in the record position. </summary>
		public object NullValue
		{
			get { return mNullValue; }
		}

		/// <summary>Indicates directly the null value.</summary>
		/// <param name="nullValue">The value to assign in the "NULL" case.</param>
		public FieldNullValueAttribute(object nullValue)
		{
			mNullValue = nullValue;
		}

		/// <summary>Indicates a type and a string to be converted to that type.</summary>
		/// <param name="type">The type of the null value.</param>
		/// <param name="nullValue">The string to be converted to the specified type.</param>
		public FieldNullValueAttribute(Type type, string nullValue)
		{
			mNullValue = Convert.ChangeType(nullValue, type, null);
		}

	}
}