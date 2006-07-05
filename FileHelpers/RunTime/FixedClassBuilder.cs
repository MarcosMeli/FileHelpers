using System;

namespace FileHelpers
{
	public sealed class FixedClassBuilder: ClassBuilder
	{

		public FixedClassBuilder(string className): base(className)
		{
		}


		public FixedFieldBuilder AddField(string fieldName, int length, Type fieldType)
		{
			FixedFieldBuilder fb = new FixedFieldBuilder(fieldName, length, fieldType);
			AddFieldInternal(fb);
			return fb;
		}

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
