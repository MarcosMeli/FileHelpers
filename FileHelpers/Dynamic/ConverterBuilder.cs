using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;

namespace FileHelpers.Dynamic
{
	/// <summary>Used to build the ConverterAttribute for the run time classes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class ConverterBuilder
	{
        /// <summary>
        /// build a converter attribute for runtime class.
        /// Writes source code for attributes on class
        /// </summary>
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

		/// <summary>The first argument passed to the converter.</summary>
		public string Arg1
		{
			get { return mArg1; }
			set { mArg1 = value; }
		}

		/// <summary>The first argument passed to the converter.</summary>
		public string Arg2
		{
			get { return mArg2; }
			set { mArg2 = value; }
		}


		/// <summary>The first argument passed to the converter.</summary>
		public string Arg3
		{
			get { return mArg3; }
			set { mArg3 = value; }
		}


        /// <summary>
        /// serialize converter attribute details to XML
        /// </summary>
        /// <param name="writer">XML writer</param>
		internal void WriteXml(XmlHelper writer)
		{
			if (mKind == ConverterKind.None && mTypeName == string.Empty) return;

			writer.Writer.WriteStartElement("Converter");

			writer.WriteAttribute("Kind", Kind.ToString(), "None");
			writer.WriteAttribute("TypeName", mTypeName.ToString(), string.Empty);

			writer.WriteAttribute("Arg1", Arg1, string.Empty);
			writer.WriteAttribute("Arg2", Arg2, string.Empty);
			writer.WriteAttribute("Arg3", Arg3, string.Empty);

			writer.Writer.WriteEndElement();

		}

        /// <summary>
        /// get converter information for the class from serialised version
        /// </summary>
        /// <param name="node">node to get attributes</param>
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


        /// <summary>
        /// Create the converter in source code
        /// </summary>
        /// <param name="lang">C# or Visual Basic</param>
        /// <returns>Converter attribute in appropriate language</returns>
		internal string GetConverterCode(NetLanguage lang)
		{
			var sb = new StringBuilder();

			if (mKind != ConverterKind.None)
				sb.Append("FieldConverter(ConverterKind." + mKind.ToString());
			else if (mTypeName != string.Empty)
			{
				if (lang == NetLanguage.CSharp)
					sb.Append("FieldConverter(typeof(" + mTypeName + ")");
				else if (lang == NetLanguage.VbNet)
					sb.Append("FieldConverter(GetType(" + mTypeName + ")");
			}
			else
				return string.Empty;

			if (!string.IsNullOrEmpty(mArg1))
			{
				sb.Append(", \"" + mArg1 + "\"");

				if (!string.IsNullOrEmpty(mArg2))
				{
					sb.Append(", \"" + mArg2 + "\"");

					if (!string.IsNullOrEmpty(mArg3))
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
