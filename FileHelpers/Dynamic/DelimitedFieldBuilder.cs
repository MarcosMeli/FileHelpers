

using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace FileHelpers.Dynamic
{
	/// <summary>Used to create fields that are part of a delimited record class.</summary>
	public sealed class DelimitedFieldBuilder: FieldBuilder
	{

        /// <summary>
        /// Create a field with name and type
        /// </summary>
        /// <param name="fieldName">Field Name to create</param>
        /// <param name="fieldType">Type of the field</param>
		internal DelimitedFieldBuilder(string fieldName, string fieldType): base(fieldName, fieldType)
		{}

        /// <summary>
        /// Create a field with a name and a type
        /// </summary>
        /// <param name="fieldName">Field name to create</param>
        /// <param name="fieldType">Type of the field</param>
		internal DelimitedFieldBuilder(string fieldName, Type fieldType): base(fieldName, fieldType)
		{}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool mFieldQuoted = false;

		/// <summary>Indicates if the field is quoted with some char. (works with QuoteMode and QuoteChar)</summary>
		public bool FieldQuoted
		{
			get { return mFieldQuoted; }
			set { mFieldQuoted = value; }
		}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private char mQuoteChar = '"';

		/// <summary>Indicates the char used to quote this field. (only used when FieldQuoted is true)</summary>
		public char QuoteChar
		{
			get { return mQuoteChar; }
			set { mQuoteChar = value; }
		}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private QuoteMode mQuoteMode = QuoteMode.OptionalForRead;

		/// <summary>Indicates the QuoteMode for this field. (only used when FieldQuoted is true)</summary>
		public QuoteMode QuoteMode
		{
			get { return mQuoteMode; }
			set { mQuoteMode = value; }
		}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MultilineMode mQuoteMultiline = MultilineMode.AllowForRead;

		/// <summary>Indicates if this quoted field can span multiple lines. (only used when FieldQuoted is true)</summary>
		public MultilineMode QuoteMultiline
		{
			get { return mQuoteMultiline; }
			set { mQuoteMultiline = value; }
		}


        /// <summary>
        /// Add the attributes to the code text for the class.
        /// </summary>
        /// <param name="attbs">Where to add attributes to</param>
        /// <param name="lang">Language selected</param>
		internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage lang)
		{
			
			if (mFieldQuoted == true)
			{
				if (lang == NetLanguage.CSharp)
				{
					string quoteStr = mQuoteChar.ToString();
					if (mQuoteChar == '\'') quoteStr = @"\'";

					attbs.AddAttribute("FieldQuoted('" + quoteStr + "', QuoteMode." + mQuoteMode.ToString()+", MultilineMode." + mQuoteMultiline.ToString() +")");
				}
				else if (lang == NetLanguage.VbNet)
				{
					string quoteStr = mQuoteChar.ToString();
					if (mQuoteChar == '"') quoteStr = "\"\"";

					attbs.AddAttribute("FieldQuoted(\"" + quoteStr + "\"c, QuoteMode." + mQuoteMode.ToString()+", MultilineMode." + mQuoteMultiline.ToString() +")");
				}
			}
		}

        /// <summary>
        /// Write any header attributes - not used
        /// </summary>
        /// <param name="writer">XML writer to add element to</param>
		internal override void WriteHeaderAttributes(XmlHelper writer)
		{
		}

        /// <summary>
        /// Write the elements for this field to the XML
        /// </summary>
        /// <param name="writer"></param>
		internal override void WriteExtraElements(XmlHelper writer)
		{
			writer.WriteElement("FieldQuoted", this.FieldQuoted);
			writer.WriteElement("QuoteChar", this.QuoteChar.ToString(), "\"");
			writer.WriteElement("QuoteMode", this.QuoteMode.ToString(), "OptionalForRead");
			writer.WriteElement("QuoteMultiline", this.QuoteMultiline.ToString(), "AllowForRead");
		}

        /// <summary>
        /// Read the elements from the XML document for this field
        /// </summary>
        /// <param name="node">XML node to read elements from</param>
		internal override void ReadFieldInternal(XmlNode node)
		{
			XmlNode ele;
			
			FieldQuoted = node["FieldQuoted"] != null;
			
			ele = node["QuoteChar"];
            
			if (ele != null && ele.InnerText.Length > 0) QuoteChar = ele.InnerText[0];

			ele = node["QuoteMode"];
			if (ele != null && ele.InnerText.Length > 0) QuoteMode = (QuoteMode) Enum.Parse(typeof(QuoteMode), ele.InnerText);

			ele = node["QuoteMultiline"];
            if (ele != null && ele.InnerText.Length > 0) QuoteMultiline = (MultilineMode)Enum.Parse(typeof(MultilineMode), ele.InnerText);
		}
	}
}
