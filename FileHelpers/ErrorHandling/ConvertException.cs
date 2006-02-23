#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>
	/// Indicates that a string value can't be converted to a dest type.
	/// </summary>
	public class ConvertException : FileHelperException
	{
		private string mStringValue;
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

		internal ConvertException(string origValue, Type destType) : this(origValue, destType, "")
		{
		}

		internal ConvertException(string origValue, Type destType, string extraInfo) : base("Error Converting '" + origValue + "' to type: '" + destType.Name + "'." + extraInfo)
		{
			mStringValue = origValue;
			mType = destType;
		}


	}
}