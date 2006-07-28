using System;
using System.Text;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create fields that are part of a dilimited record class.</summary>
	public sealed class DelimitedFieldBuilder: FieldBuilder
	{

		internal DelimitedFieldBuilder(string fieldName, string fieldType): base(fieldName, fieldType)
		{}

		internal DelimitedFieldBuilder(string fieldName, Type fieldType): base(fieldName, fieldType)
		{}

		private bool mFieldQuoted = false;

		/// <summary>Indicates if the field is quoted with some char. (works with QuoteMode and QuoteChar)</summary>
		public bool FieldQuoted
		{
			get { return mFieldQuoted; }
			set { mFieldQuoted = value; }
		}

		private char mQuoteChar = '"';

		/// <summary>Indicates the char used to quote this field. (only used when FieldQuoted is true)</summary>
		public char QuoteChar
		{
			get { return mQuoteChar; }
			set { mQuoteChar = value; }
		}

		private QuoteMode mQuoteMode = QuoteMode.OptionalForRead;

		/// <summary>Indicates the QuoteMode for this field. (only used when FieldQuoted is true)</summary>
		public QuoteMode QuoteMode
		{
			get { return mQuoteMode; }
			set { mQuoteMode = value; }
		}

		private MultilineMode mQuoteMultiline = MultilineMode.AllowForRead;

		/// <summary>Indicates if this quoted field can span multiple lines. (only used when FieldQuoted is true)</summary>
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

		internal override void WriteHeaderAttributes(XmlHelper writer)
		{
		}

		internal override void WriteExtraElements(XmlHelper writer)
		{
		}
	}
}
