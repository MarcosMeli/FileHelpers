using System;
using System.Text;

namespace FileHelpers.RunTime
{
	public sealed class DelimitedFieldBuilder: FieldBuilder
	{

		public DelimitedFieldBuilder(string fieldName, Type fieldType): base(fieldName, fieldType)
		{}

		private bool mFieldQuoted = false;

		public bool FieldQuoted
		{
			get { return mFieldQuoted; }
			set { mFieldQuoted = value; }
		}

		private char mQuoteChar = '"';

		public char QuoteChar
		{
			get { return mQuoteChar; }
			set { mQuoteChar = value; }
		}

		private QuoteMode mQuoteMode = QuoteMode.OptionalForRead;

		public QuoteMode QuoteMode
		{
			get { return mQuoteMode; }
			set { mQuoteMode = value; }
		}

		private MultilineMode mQuoteMultiline = MultilineMode.AllowForRead;

		public MultilineMode QuoteMultiline
		{
			get { return mQuoteMultiline; }
			set { mQuoteMultiline = value; }
		}


		internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage leng)
		{
			
			if (mFieldQuoted == true)
			{
				if (leng == NetLanguage.CSharp)
					attbs.AddAttribute("FieldQuoted('" + mQuoteChar.ToString() + "', QuoteMode." + mQuoteMode.ToString()+", MultilineMode." + mQuoteMultiline.ToString() +")");
				else if (leng == NetLanguage.VbNet)
					attbs.AddAttribute("FieldQuoted(\"" + mQuoteChar.ToString() + "\"c, QuoteMode." + mQuoteMode.ToString()+", MultilineMode." + mQuoteMultiline.ToString() +")");
			}
			
		}
	}
}
