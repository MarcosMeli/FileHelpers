using System;

namespace FileHelpers
{
	public sealed class DelimitedClassBuilder: ClassBuilder
	{
		private string mDelimiter = string.Empty;

		public string Delimiter
		{
			get { return mDelimiter; }
			set { mDelimiter = value; }
		}


		public DelimitedClassBuilder(string className, string delimiter): base(className)
		{
			mDelimiter = delimiter;
		}

		public DelimitedClassBuilder(string className): this(className, string.Empty)
		{
		}


		public DelimitedFieldBuilder AddField(string fieldName, Type fieldType)
		{
			DelimitedFieldBuilder fb = new DelimitedFieldBuilder(fieldName, fieldType);
			AddFieldInternal(fb);
			return fb;
		}

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
	}
}
