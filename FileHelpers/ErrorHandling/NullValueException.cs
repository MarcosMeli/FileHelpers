#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers
{
	/// <summary>
	/// Indicated that a Field must have a NullValueAttribute.
	/// </summary>
	public class NullValueException : FileHelperException
	{
		private FieldInfo mFieldInfo;

		/// <summary>The name of the field.</summary>
		public string FieldName
		{
			get { return mFieldInfo.Name; }
		}

		/// <summary>The type of the field. </summary>
		public Type FieldType
		{
			get { return mFieldInfo.FieldType; }
		}

		internal NullValueException(FieldInfo fi) : base("You must specify a NullValue in the " + fi.Name + " field of type " + fi.FieldType.Name + ", because this is a ValueType.")
		{
			mFieldInfo = fi;
		}
	}
}