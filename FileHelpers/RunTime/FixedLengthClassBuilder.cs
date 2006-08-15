using System;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create classes that maps to Fixed Length records.</summary>
	public sealed class FixedLengthClassBuilder: ClassBuilder
	{

		/// <summary>Used to create classes that maps to Fixed Length records.</summary>
		/// <param name="className">A valid class name.</param>
		public FixedLengthClassBuilder(string className): base(className)
		{
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
			attbs.AddAttribute("FixedLengthRecord()");
		}
		
		internal override void WriteHeaderElement(XmlHelper writer)
		{
			writer.mWriter.WriteStartElement("FixedLengthClass");
		}

		internal override void WriteExtraElements(XmlHelper writer)
		{
		}

	}
}
