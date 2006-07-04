using System;
using System.Text;

namespace FileHelpers
{
	public sealed class FixedFieldBuilder: FieldBuilder
	{

		private int mLength;

		public FixedFieldBuilder(string fieldName, int length, Type fieldType): base(fieldName, fieldType)
		{
			mLength = length;
		}

		public int Length
		{
			get { return mLength; }
		}

		internal string GetFieldDef(NetLenguage leng)
		{
			StringBuilder sb = new StringBuilder(100);
			switch(leng)
			{
				case NetLenguage.CSharp:

					break;

			}

			return sb.ToString();
		}
	}
}
