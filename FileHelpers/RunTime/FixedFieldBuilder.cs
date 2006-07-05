using System;
using System.Text;

namespace FileHelpers
{
	public sealed class FixedFieldBuilder: FieldBuilder
	{
		private int mLength;

		public FixedFieldBuilder(string fieldName, int length, Type fieldType): base(fieldName, fieldType)
		{
			mLength = length;
		}

		public int Length
		{
			get { return mLength; }
		}


		private AlignMode mAlignMode = AlignMode.Left;

		public AlignMode AlignMode
		{
			get { return mAlignMode; }
			set { mAlignMode = value; }
		}

		private char mAlignChar = ' ';
		public char AlignChar
		{
			get { return mAlignChar; }
			set { mAlignChar = value; }
		}
		
		internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage leng)
		{
			if (mLength <= 0)
				throw new BadUsageException("The Length of each field must be grater than 0");
			else
				attbs.AddAttribute("FieldFixedLength("+ mLength.ToString() +")");

			if (mAlignMode != AlignMode.Left)
			{
				if (leng == NetLanguage.CSharp)
					attbs.AddAttribute("FieldAlign(AlignMode."+ mAlignMode.ToString()+", '"+ mAlignChar.ToString() +"')");

				else if (leng == NetLanguage.VbNet)
					attbs.AddAttribute("FieldAlign(AlignMode."+ mAlignMode.ToString()+", \""+ mAlignChar.ToString() +"\"c)");
			}

		}
	}
}
