using System;
using System.Data;
using System.Xml;
using System.Diagnostics;

namespace FileHelpers.Dynamic
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

		/// <summary>
		/// Used to create classes that maps to Fixed Length records
		/// and automatically instantiate many string fields as values
		/// are passed in the lengths arg.
		/// </summary>
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
                AddField(StringHelper.ToValidIdentifier(dc.ColumnName), defaultLength, dc.DataType);
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
			var fb = new FixedFieldBuilder(fieldName, length, fieldType);
			AddFieldInternal(fb);
			return fb;
		}
		
		/// <summary>Adds a new Fixed Length field.</summary>
		/// <param name="fieldName">The name of the field.</param>
		/// <param name="length">The length of the field.</param>
        /// <param name="fieldType">The Type of the field. (For generic of nullable types use the string overload, like "int?")</param>
		/// <returns>The just created field.</returns>
		public FixedFieldBuilder AddField(string fieldName, int length, Type fieldType)
		{
			return AddField(fieldName, length, TypeToString(fieldType));
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

	    /// <summary>
	    /// add attributes to the class text
	    /// </summary>
	    /// <param name="attbs"></param>
	    /// <param name="lang"></param>
	    internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage lang)
		{
			attbs.AddAttribute("FixedLengthRecord(FixedMode."+ mFixedMode.ToString()+ ")");
		}
		
        /// <summary>
        /// write the fixed length XML to document
        /// </summary>
        /// <param name="writer">writer to put XML on</param>
		internal override void WriteHeaderElement(XmlHelper writer)
		{
			writer.Writer.WriteStartElement("FixedLengthClass");
			writer.Writer.WriteStartAttribute("FixedMode", "");
			writer.Writer.WriteString(mFixedMode.ToString());
			writer.Writer.WriteEndAttribute();			
		}

        /// <summary>
        /// write any extra elements required
        /// </summary>
        /// <param name="writer"></param>
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FixedMode mFixedMode = FixedMode.ExactLength;

		/// <summary>Indicates the behavior when variable length records are found </summary>
		public FixedMode FixedMode
		{
			get { return mFixedMode; }
			set { mFixedMode = value; }
		}

        /// <summary>
        /// read elements from XML for class
        /// </summary>
        /// <param name="document"></param>
		internal override void ReadClassElements(XmlDocument document)
		{
		}

        /// <summary>
        /// read attributes from element
        /// </summary>
        /// <param name="node">node to read</param>
		internal override void ReadField(XmlNode node)
		{
			AddField(node.Attributes.Item(0).InnerText, int.Parse(node.Attributes.Item(2).InnerText), node.Attributes.Item(1).InnerText).ReadField(node);
		}	
		
        /// <summary>
        /// load details of fixed length from XML
        /// </summary>
        /// <param name="document">document to check</param>
        /// <returns>fixed length details</returns>
		internal static FixedLengthClassBuilder LoadXmlInternal(XmlDocument document)
		{
			// Note: for some reason, ReSharper complains about the use of the 2-argument form
			// of Enum.Parse() used here (and elsewhere).  ReSharper marks it as a fatal error,
			// despite the fact that the code compiles just fine, with no warnings.
			FixedMode mode = (FixedMode)Enum.Parse(typeof(FixedMode), document.SelectNodes("/FixedLengthClass")[0].Attributes["FixedMode"].Value);
			string className = document.SelectNodes("/FixedLengthClass/ClassName")[0].InnerText;
			
			return new FixedLengthClassBuilder(className, mode);
		}
	}
}
