#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers.Fields
{
	internal class FixedLengthField : FieldBase
	{

		#region "  Properties  "

		private int mLength;
		private FieldAlignAttribute mAlign = new FieldAlignAttribute(AlignMode.Left, ' ');

		public int Length
		{
			get { return mLength; }
		}

		public FieldAlignAttribute Align
		{
			get { return mAlign; }
		}

		#endregion

		#region "  Constructor  "

		internal FixedLengthField(FieldInfo fi, int length, FieldAlignAttribute align) : base(fi)
		{
			this.mLength = length;

			if (align != null)
				this.mAlign = align;
		}

		#endregion

		#region "  Overrides String Handling  "

		protected override ExtractInfo ExtractFieldString(string from)
		{
			ExtractInfo res;

			if (from.Length < this.Length)
			{
				if (mIsLast)
					res = new ExtractInfo(from);
				else
					throw new FileHelperException("The string for the field " + FieldInfo.Name + ": '" + from + "' don´t match the record length: " + Length);
			}
			else
				res = new ExtractInfo(from.Substring(0, this.mLength));

			return res;
		}

		protected override string CreateFieldString(object record)
		{
			string res;
			res = base.CreateFieldString(record);

			if (res.Length > this.mLength)
			{
				res = res.Substring(0, this.mLength);
			}

			if (mAlign.Align == AlignMode.Left)
				res = res.PadRight(mLength, mAlign.AlignChar);
			else if (mAlign.Align == AlignMode.Right)
				res = res.PadLeft(mLength, mAlign.AlignChar);
			else
			{
				int middle = (mLength - res.Length)/2;
				if (middle > 0)
					res = res.PadLeft(mLength - middle, mAlign.AlignChar).PadRight(mLength, mAlign.AlignChar);
			}

			return res;
		}

		#endregion

	}
}