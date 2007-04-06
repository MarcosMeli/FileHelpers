using System;
using System.Diagnostics;
using System.Data;
using System.Xml;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create classes that maps to Delimited records.</summary>
	public class DelimitedClassBuilder: ClassBuilder
	{
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private string mDelimiter = string.Empty;

		/// <summary>The Delimiter that marks the end of each field.</summary>
		public string Delimiter
		{
			get { return mDelimiter; }
			set { mDelimiter = value; }
		}

		
		
		/// <summary>Return the field at the specified index.</summary>
		/// <param name="index">The index of the field.</param>
		/// <returns>The field at the specified index.</returns>
		public new DelimitedFieldBuilder FieldByIndex(int index)
		{
			return (DelimitedFieldBuilder) base.FieldByIndex(index);
		}

		/// <summary>Returns the current fields of the class.</summary>
		public new DelimitedFieldBuilder[] Fields
		{
			get { return (DelimitedFieldBuilder[]) mFields.ToArray(typeof(DelimitedFieldBuilder)); }
		}
		
		
		/// <summary>Creates a new DelimitedClassBuilder.</summary>
		/// <param name="className">The valid class name.</param>
		/// <param name="delimiter">The delimiter for that class.</param>
		public DelimitedClassBuilder(string className, string delimiter): base(className)
		{
			mDelimiter = delimiter;
		}

		/// <summary>Creates a new DelimitedClassBuilder.</summary>
		/// <param name="className">The valid class name.</param>
		public DelimitedClassBuilder(string className): this(className, string.Empty)
		{
		}

		/// <summary>Creates a new DelimitedClassBuilder with the same structure than a DataTable.</summary>
		/// <param name="className">The valid class name.</param>
		/// <param name="delimiter">The delimiter for that class.</param>
		/// <param name="dt">The DataTable from where to get the field names and types</param>
		public DelimitedClassBuilder(string className, string delimiter, DataTable dt): this(className, delimiter)
		{
			foreach(DataColumn dc in dt.Columns)
			{
				AddField(StringToIdentifier(dc.ColumnName), dc.DataType);
			}
		}


		/// <summary>Add a new Delimited field to the current class.</summary>
		/// <param name="fieldName">The Name of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public virtual DelimitedFieldBuilder AddField(string fieldName, string fieldType)
		{
			DelimitedFieldBuilder fb = new DelimitedFieldBuilder(fieldName, fieldType);
			AddFieldInternal(fb);
			return fb;
		}

		/// <summary>Add a new Delimited field to the current class.</summary>
		/// <param name="fieldName">The Name of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public DelimitedFieldBuilder AddField(string fieldName, Type fieldType)
		{
			return AddField(fieldName, fieldType.FullName);
		}

		/// <summary>Add a new Delimited string field to the current class.</summary>
		/// <param name="fieldName">The Name of the string field.</param>
		/// <returns>The just created field.</returns>
		public virtual DelimitedFieldBuilder AddField(string fieldName)
		{
			return AddField(fieldName, "System.String");
		}

		/// <summary>Add a new Delimited field to the current class.</summary>
		/// <param name="field">The field definition.</param>
		/// <returns>The just added field.</returns>
		public DelimitedFieldBuilder AddField(DelimitedFieldBuilder field)
		{
			AddFieldInternal(field);
			return field;
		}

		/// <summary>Return the last added field. (use it reduce casts and code)</summary>
		public DelimitedFieldBuilder LastField
		{
			get
			{
				if (mFields.Count == 0)
					return null;
				else
					return (DelimitedFieldBuilder) mFields[mFields.Count -1];
			}
		}

		internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage leng)
		{
			if (mDelimiter == string.Empty)
				throw new BadUsageException("The Delimiter of the DelimiterClassBuilder can't be null or empty.");
			else
				attbs.AddAttribute("DelimitedRecord(\""+ mDelimiter +"\")");
			
		}

		internal override void WriteHeaderElement(XmlHelper writer)
		{
			writer.mWriter.WriteStartElement("DelimitedClass");
			writer.mWriter.WriteStartAttribute("Delimiter", "");
			writer.mWriter.WriteString(this.Delimiter);
			writer.mWriter.WriteEndAttribute();
		}

		internal override void WriteExtraElements(XmlHelper writer)
		{

		}

		internal static DelimitedClassBuilder LoadXmlInternal(XmlDocument document)
		{
			DelimitedClassBuilder res;
			string del = document.ChildNodes[0].Attributes[0].Value;
			
			string className = document.ChildNodes.Item(0).SelectNodes("/DelimitedClass/ClassName").Item(0).InnerText;
			
			res = new DelimitedClassBuilder(className, del);
//			
//			while(reader.mReader.EOF == false)
//			{
//				reader.ReadToNextElement();
////				if (reader.mReader.LocalName == "IgnoreEmptyLines")
//			}
			
			
			return res;
		}

		internal override void ReadClassElements(XmlDocument document)
		{}

		internal override void ReadField(XmlNode node)
		{
			AddField(node.Attributes.Item(0).InnerText, node.Attributes.Item(1).InnerText).ReadField(node);
		}
	}
}
