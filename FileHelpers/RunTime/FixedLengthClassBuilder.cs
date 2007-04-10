using System;
using System.Data;
using System.Xml;
using System.Diagnostics;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create classes that maps to Fixed Length records.</summary>
	public sealed class FixedLengthClassBuilder: ClassBuilder
	{
	
		/// <summary>Return the field at the specified index.</summary>
		/// <param name="index">The index of the field.</param>
		/// <returns>The field at the specified index.</returns>
		public new FixedFieldBuilder FieldByIndex(int index)
		{
			return (FixedFieldBuilder) base.FieldByIndex(index);
		}

		/// <summary>Returns the current fields of the class.</summary>
		public new FixedFieldBuilder[] Fields
		{
			get { return (FixedFieldBuilder[]) mFields.ToArray(typeof(FixedFieldBuilder)); }
		}
		
		/// <summary>Used to create classes that maps to Fixed Length records.</summary>
		/// <param name="className">A valid class name.</param>
		public FixedLengthClassBuilder(string className): base(className)
		{
		}

		/// <summary>Used to create classes that maps to Fixed Length records and automatic instanciate many string fields as values are passed in the lengths arg. </summary>
		/// <param name="className">A valid class name.</param>
		/// <param name="lengths">The lengths of the fields (one string field will be create for each length)</param>
		public FixedLengthClassBuilder(string className, params int[] lengths): base(className)
		{
			for(int i = 0; i < lengths.Length; i++)
				AddField("Field"+((i+1).ToString()), lengths[i], typeof(string));
		}

		/// <summary>Used to create classes that maps to Fixed Length records with the same structure than a DataTable.</summary>
		/// <param name="className">A valid class name.</param>
		/// <param name="dt">The DataTable from where to get the field names and types</param>
		/// <param name="defaultLength">The initial length of all fields</param>
		public FixedLengthClassBuilder(string className, DataTable dt, int defaultLength): this(className)
		{
			foreach(DataColumn dc in dt.Columns)
			{
				AddField(StringToIdentifier(dc.ColumnName), defaultLength, dc.DataType);
			}
		}

		/// <summary>Used to create classes that maps to Fixed Length records.</summary>
		/// <param name="className">A valid class name.</param>
		/// <param name="mode">Indicates the behavior when variable length records are found.</param>
		public FixedLengthClassBuilder(string className, FixedMode mode): base(className)
		{
			mFixedMode = mode;
		}

		/// <summary>Adds a new Fixed Length field.</summary>
		/// <param name="fieldName">The name of the field.</param>
		/// <param name="length">The length of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public FixedFieldBuilder AddField(string fieldName, int length, string fieldType)
		{
			FixedFieldBuilder fb = new FixedFieldBuilder(fieldName, length, fieldType);
			AddFieldInternal(fb);
			return fb;
		}
		
		/// <summary>Adds a new Fixed Length field.</summary>
		/// <param name="fieldName">The name of the field.</param>
		/// <param name="length">The length of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public FixedFieldBuilder AddField(string fieldName, int length, Type fieldType)
		{
			return AddField(fieldName, length, fieldType.FullName);
		}

		/// <summary>Adds a new Fixed Length field.</summary>
		/// <param name="field">The field definition.</param>
		/// <returns>The just added field.</returns>
		public FixedFieldBuilder AddField(FixedFieldBuilder field)
		{
			AddFieldInternal(field);
			return field;
		}
		
		/// <summary>Return the last added field. (use it reduce casts and code)</summary>
		public FixedFieldBuilder LastField
		{
			get
			{
				if (mFields.Count == 0)
					return null;
				else
					return (FixedFieldBuilder) mFields[mFields.Count -1];
			}
		}


		internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage leng)
		{
			attbs.AddAttribute("FixedLengthRecord(FixedMode."+ mFixedMode.ToString()+ ")");
		}
		
		internal override void WriteHeaderElement(XmlHelper writer)
		{
			writer.mWriter.WriteStartElement("FixedLengthClass");
			writer.mWriter.WriteStartAttribute("FixedMode", "");
			writer.mWriter.WriteString(mFixedMode.ToString());
			writer.mWriter.WriteEndAttribute();			
		}

		internal override void WriteExtraElements(XmlHelper writer)
		{}

		/// <summary>
		/// Set the length of each field at once.
		/// </summary>
		/// <param name="lengths">The length of each field, must be the same that the number of fields.</param>
		public void SetFieldsLength(params int[] lengths)
		{
			if (lengths.Length != mFields.Count)
				throw new BadUsageException(string.Format("The number of elements is {0} and you pass {1}. This method require the same number of values than fields", mFields.Count, lengths.Length));

			for(int i = 0; i < mFields.Count; i++)
				FieldByIndex(i).FieldLength = lengths[i];
		}

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private FixedMode mFixedMode = FixedMode.ExactLength;

		/// <summary>Indicates the behavior when variable length records are found </summary>
		public FixedMode FixedMode
		{
			get { return mFixedMode; }
			set { mFixedMode = value; }
		}

		internal override void ReadClassElements(XmlDocument document)
		{
		}

		internal override void ReadField(XmlNode node)
		{
			AddField(node.Attributes.Item(0).InnerText, int.Parse(node.Attributes.Item(2).InnerText), node.Attributes.Item(1).InnerText).ReadField(node);
		}	
		
		internal static FixedLengthClassBuilder LoadXmlInternal(XmlDocument document)
		{
			FixedLengthClassBuilder res;

            FixedMode  mode = (FixedMode)Enum.Parse(typeof(FixedMode), document.ChildNodes[0].Attributes[0].Value);
			
			string className = document.ChildNodes.Item(0).SelectNodes("/FixedLengthClass/ClassName").Item(0).InnerText;
			
			return new FixedLengthClassBuilder(className, mode);
		}
	}
}
