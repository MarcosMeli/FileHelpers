using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace FileHelpers.Dynamic
{
	/// <summary>Used to create Fixed Length fields and set their properties.</summary>
	public sealed class FixedFieldBuilder: FieldBuilder
	{

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int mFieldLength;

        /// <summary>
        /// Create a fixed length field of name, length and type
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="length">Length of field on file</param>
        /// <param name="fieldType">Type of the field</param>
		internal FixedFieldBuilder(string fieldName, int length, Type fieldType): this(fieldName, length, ClassBuilder.TypeToString(fieldType))
		{}

        /// <summary>
        /// Create a fixed length field of name, length and type
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="length">Length of field on file</param>
        /// <param name="fieldType">Type of the field</param>
        internal FixedFieldBuilder(string fieldName, int length, string fieldType)
            : base(fieldName, fieldType)
		{
			mFieldLength = length;
		}

		/// <summary>The fixed length of the field.</summary>
		public int FieldLength
		{
			get { return mFieldLength; }
			set { mFieldLength = value; }
		}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AlignMode mAlignMode = AlignMode.Left;

		/// <summary>The align of the field used for write operations.</summary>
		public AlignMode AlignMode
		{
			get { return mAlignMode; }
			set { mAlignMode = value; }
		}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private char mAlignChar = ' ';

		/// <summary>The align char of the field used for write operations.</summary>
		public char AlignChar
		{
			get { return mAlignChar; }
			set { mAlignChar = value; }
		}

		/// <summary>
		/// Add attributes to the field at the appropriate spot in the code
		/// </summary>
		/// <param name="attbs">Add attributes to this</param>
		/// <param name="lang">Language to use,  C# of Visual Basic</param>
		internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage lang)
		{
			if (mFieldLength <= 0)
				throw new BadUsageException("The Length of each field must be grater than 0");
			else
				attbs.AddAttribute("FieldFixedLength("+ mFieldLength.ToString() +")");

			if (mAlignMode != AlignMode.Left)
			{
				if (lang == NetLanguage.CSharp)
					attbs.AddAttribute("FieldAlign(AlignMode."+ mAlignMode.ToString()+", '"+ mAlignChar.ToString() +"')");
				else if (lang == NetLanguage.VbNet)
					attbs.AddAttribute("FieldAlign(AlignMode."+ mAlignMode.ToString()+", \""+ mAlignChar.ToString() +"\"c)");
			}
		}

        /// <summary>
        /// Serialise attributes for field to the XML 
        /// </summary>
        /// <param name="writer"></param>
		internal override void WriteHeaderAttributes(XmlHelper writer)
		{
			writer.Writer.WriteStartAttribute("Length", "");
			writer.Writer.WriteString(mFieldLength.ToString());
			writer.Writer.WriteEndAttribute();
		}

        /// <summary>
        /// Write any extra elements to the code
        /// </summary>
        /// <param name="writer">XML writer to serialise to</param>
		internal override void WriteExtraElements(XmlHelper writer)
		{
			writer.WriteElement("AlignMode", this.AlignMode.ToString(), "Left");
			writer.WriteElement("AlignChar", this.AlignChar.ToString(), " ");
		}

        /// <summary>
        /// Read the extra alignChar and alignMode elements back into the field details
        /// </summary>
        /// <param name="node">Field Node from XML</param>
		internal override void ReadFieldInternal(XmlNode node)
		{
			XmlNode ele;
			
			ele = node["AlignChar"];
			if (ele != null) AlignChar = ele.InnerText[0];

			ele = node["AlignMode"];
			if (ele != null) AlignMode = (AlignMode) Enum.Parse(typeof(AlignMode), ele.InnerText);
		}
	}
}
