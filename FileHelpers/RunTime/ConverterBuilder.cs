using System;
using System.ComponentModel;
using System.Text;
using System.Xml;

namespace FileHelpers.RunTime
{
	/// <summary>Used to build the ConverterAttribute for the run time classes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class ConverterBuilder
	{
		internal ConverterBuilder()
		{}

		private ConverterKind mKind = ConverterKind.None;

		/// <summary>The ConverterKind to be used , like the one in the FieldConverterAttribute.</summary>
		public ConverterKind Kind
		{
			get { return mKind; }
			set { mKind = value; }
		}

		private string mTypeName = string.Empty;

		/// <summary>The type name of your custom converter.</summary>
		public string TypeName
		{
			get { return mTypeName; }
			set { mTypeName = value; }
		}

		private string mArg1 = string.Empty;
		private string mArg2 = string.Empty;
		private string mArg3 = string.Empty;

		/// <summary>The first argument pased to the converter.</summary>
		public string Arg1
		{
			get { return mArg1; }
			set { mArg1 = value; }
		}

		/// <summary>The first argument pased to the converter.</summary>
		public string Arg2
		{
			get { return mArg2; }
			set { mArg2 = value; }
		}

		
		/// <summary>The first argument pased to the converter.</summary>
		public string Arg3
		{
			get { return mArg3; }
			set { mArg3 = value; }
		}

		
		
		internal void WriteXml(XmlHelper writer)
		{
			if (mKind == ConverterKind.None && mTypeName == string.Empty) return;
			
			writer.mWriter.WriteStartElement("Converter");
			
			writer.WriteAttribute("Kind", Kind.ToString(), "None");
			writer.WriteAttribute("TypeName", mTypeName.ToString(), string.Empty);

			writer.WriteAttribute("Arg1", Arg1, string.Empty);
			writer.WriteAttribute("Arg2", Arg2, string.Empty);
			writer.WriteAttribute("Arg3", Arg3, string.Empty);
			
			writer.mWriter.WriteEndElement();

		}

		internal void LoadXml(XmlNode node)
		{

			XmlAttribute attb = node.Attributes["Kind"];
			if (attb != null) Kind = (ConverterKind) Enum.Parse(typeof(ConverterKind), attb.InnerText);
			
			attb = node.Attributes["TypeName"];
			if (attb != null) TypeName = attb.InnerText;
			
			attb = node.Attributes["Arg1"];
			if (attb != null) Arg1 = attb.InnerText;

			attb = node.Attributes["Arg2"];
			if (attb != null) Arg2 = attb.InnerText;

			attb = node.Attributes["Arg3"];
			if (attb != null) Arg3 = attb.InnerText;

		}
		
		
		
		
		
		
	
		internal string GetConverterCode(NetLanguage leng)
		{
			StringBuilder sb = new StringBuilder();
			
			if (mKind != ConverterKind.None)
				sb.Append("FieldConverter(ConverterKind." + mKind.ToString());
			else if (mTypeName != string.Empty)
			{
				if (leng == NetLanguage.CSharp)
					sb.Append("FieldConverter(typeof(" + mTypeName + ")");
				else if (leng == NetLanguage.VbNet)
					sb.Append("FieldConverter(GetType(" + mTypeName + ")");
			}
			else
				return string.Empty;
			
			if (mArg1 != null && mArg1 != string.Empty)
			{
				sb.Append(", \"" + mArg1 + "\"");

				if (mArg2 != null && mArg2 != string.Empty)
				{
					sb.Append(", \"" + mArg2 + "\"");
					
					if (mArg3 != null && mArg3 != string.Empty)
					{
						sb.Append(", \"" + mArg3 + "\"");
					}
				}
			}
			
			sb.Append(")");
			
			return sb.ToString();
		}
	}
}
