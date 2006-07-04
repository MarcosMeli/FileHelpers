using System;
using System.Text;

namespace FileHelpers
{
	public sealed class DelimitedFieldBuilder: FieldBuilder
	{

		public DelimitedFieldBuilder(string fieldName, Type fieldType): base(fieldName, fieldType)
		{
		}

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

		internal string GetFieldDef(NetLenguage leng)
		{
			StringBuilder sb = new StringBuilder(100);
			switch(leng)
			{
				case NetLenguage.CSharp:

					break;

			}

			return sb.ToString();
		}
	}
}
