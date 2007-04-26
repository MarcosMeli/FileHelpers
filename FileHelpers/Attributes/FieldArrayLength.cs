#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldArrayLengthAttribute : Attribute
	{

		#region "  Constructors  "

		public FieldArrayLengthAttribute(int minLength, int maxLength)
		{
			mMinLength = minLength;
			mMaxLength = maxLength;
		}

		public FieldArrayLengthAttribute(int length) : this(length, length)
		{
		}


		#endregion

		internal int mMinLength;
		internal int mMaxLength;
	}
}