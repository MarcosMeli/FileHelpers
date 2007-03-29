#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>
	/// Indicates that a string value can't be converted to a dest type.
	/// </summary>
	public sealed class ConvertException : FileHelpersException
	{
		private string mFieldName = string.Empty;
		private string mStringValue = string.Empty;
		private Type mType;

		/// <summary>The destination type.</summary>
		public Type Type
		{
			get { return mType; }
		}

		/// <summary>The source string.</summary>
		public string StringValue
		{
			get { return mStringValue; }
		}

		/// <summary>The name of the field related to the exception.</summary>
		public string FieldName
		{
			get { return mFieldName; }
			set { mFieldName = value; }
		}

		/// <summary>
		/// Create a new ConvertException object
		/// </summary>
		/// <param name="origValue">The value to convert.</param>
		/// <param name="destType">The destination Type.</param>
		public ConvertException(string origValue, Type destType) : this(origValue, destType, "")
		{
		}

		/// <summary>
		/// Create a new ConvertException object
		/// </summary>
		/// <param name="origValue">The value to convert.</param>
		/// <param name="destType">The destination Type.</param>
		/// <param name="extraInfo">Aditional info of the error.</param>
		public ConvertException(string origValue, Type destType, string extraInfo) : base("Error Converting '" + origValue + "' to type: '" + destType.Name + "'." + extraInfo)
		{
			mStringValue = origValue;
			mType = destType;
		}


	}
}