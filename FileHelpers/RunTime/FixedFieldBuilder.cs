using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create Fixed Length fields and set their properties.</summary>
	public sealed class FixedFieldBuilder: FieldBuilder
	{
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private int mFieldLength;

		internal FixedFieldBuilder(string fieldName, int length, Type fieldType): this(fieldName, length, fieldType.FullName)
		{}

		internal FixedFieldBuilder(string fieldName, int length, string fieldType): base(fieldName, fieldType)
		{
			mFieldLength = length;
		}

		/// <summary>The fixed length of the field.</summary>
		public int FieldLength
		{
			get { return mFieldLength; }
			set { mFieldLength = value; }
		}


#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private AlignMode mAlignMode = AlignMode.Left;

		/// <summary>The align of the field used for write operations.</summary>
		public AlignMode AlignMode
		{
			get { return mAlignMode; }
			set { mAlignMode = value; }
		}

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private char mAlignChar = ' ';

		/// <summary>The align char of the field used for write operations.</summary>
		public char AlignChar
		{
			get { return mAlignChar; }
			set { mAlignChar = value; }
		}
		
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

		internal override void WriteHeaderAttributes(XmlHelper writer)
		{
			writer.mWriter.WriteStartAttribute("Length", "");
			writer.mWriter.WriteString(mFieldLength.ToString());
			writer.mWriter.WriteEndAttribute();
		}

		internal override void WriteExtraElements(XmlHelper writer)
		{
			writer.WriteElement("AlignMode", this.AlignMode.ToString(), "Left");
			writer.WriteElement("AlignChar", this.AlignChar.ToString(), " ");
		}

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
