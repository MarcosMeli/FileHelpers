using System;
using System.Text;

namespace FileHelpers.RunTime
{
	public sealed class FixedFieldBuilder: FieldBuilder
	{
		private int mFiledLength;

		public FixedFieldBuilder(string fieldName, int length, Type fieldType): base(fieldName, fieldType)
		{
			mFiledLength = length;
		}

		public int FieldLength
		{
			get { return mFiledLength; }
			set { mFiledLength = value; }
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
		
		internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage lang)
		{
			if (mFiledLength <= 0)
				throw new BadUsageException("The Length of each field must be grater than 0");
			else
				attbs.AddAttribute("FieldFixedLength("+ mFiledLength.ToString() +")");

			if (mAlignMode != AlignMode.Left)
			{
				if (lang == NetLanguage.CSharp)
					attbs.AddAttribute("FieldAlign(AlignMode."+ mAlignMode.ToString()+", '"+ mAlignChar.ToString() +"')");

				else if (lang == NetLanguage.VbNet)
					attbs.AddAttribute("FieldAlign(AlignMode."+ mAlignMode.ToString()+", \""+ mAlignChar.ToString() +"\"c)");
			}

		}
	}
}
