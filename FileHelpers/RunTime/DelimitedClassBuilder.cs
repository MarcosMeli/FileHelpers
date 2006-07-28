using System;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create classes that maps to Delimited records.</summary>
	public sealed class DelimitedClassBuilder: ClassBuilder
	{
		private string mDelimiter = string.Empty;

		/// <summary>The Delimiter that marks the end of each field.</summary>
		public string Delimiter
		{
			get { return mDelimiter; }
			set { mDelimiter = value; }
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


		/// <summary>Add a new Delimited field to the current class.</summary>
		/// <param name="fieldName">The Name of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public DelimitedFieldBuilder AddField(string fieldName, Type fieldType)
		{
			DelimitedFieldBuilder fb = new DelimitedFieldBuilder(fieldName, fieldType);
			AddFieldInternal(fb);
			return fb;
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

	}
}
