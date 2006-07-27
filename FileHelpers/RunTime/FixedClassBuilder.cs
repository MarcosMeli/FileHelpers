using System;

namespace FileHelpers.RunTime
{
	/// <summary>Used to create classes that maps to Fixed Length records.</summary>
	public sealed class FixedClassBuilder: ClassBuilder
	{

		public FixedClassBuilder(string className): base(className)
		{
		}


		/// <summary>Adds a new Fixed Length field.</summary>
		/// <param name="fieldName">The name of the field.</param>
		/// <param name="length">The length of the field.</param>
		/// <param name="fieldType">The Type of the field.</param>
		/// <returns>The just created field.</returns>
		public FixedFieldBuilder AddField(string fieldName, int length, Type fieldType)
		{
			FixedFieldBuilder fb = new FixedFieldBuilder(fieldName, length, fieldType);
			AddFieldInternal(fb);
			return fb;
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
	}
}
