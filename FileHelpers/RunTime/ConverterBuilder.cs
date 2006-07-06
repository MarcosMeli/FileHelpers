using System;
using System.ComponentModel;
using System.Text;

namespace FileHelpers.RunTime
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class ConverterBuilder
	{
		internal ConverterBuilder()
		{}

		private ConverterKind mKind = ConverterKind.None;

		public ConverterKind Kind
		{
			get { return mKind; }
			set { mKind = value; }
		}

		private string mTypeName = string.Empty;

		public string TypeName
		{
			get { return mTypeName; }
			set { mTypeName = value; }
		}


		public object Arg1
		{
			get { return mArg1; }
			set { mArg1 = value; }
		}

		private object mArg1;

		public object Arg2
		{
			get { return mArg2; }
			set { mArg2 = value; }
		}

		private object mArg2;
		
		public object Arg3
		{
			get { return mArg3; }
			set { mArg3 = value; }
		}

		private object mArg3;
		
		
		internal string GetFieldDef(NetLanguage leng)
		{
			StringBuilder sb = new StringBuilder(100);
			switch(leng)
			{
				case NetLanguage.CSharp:

					break;

			}

			return sb.ToString();
		}
		
		public string GetConverterCode(NetLanguage leng)
		{
			StringBuilder sb = new StringBuilder();
			
			if (mKind != ConverterKind.None)
				sb.Append("FieldConverter(ConverterKind." + mKind.ToString());
			else if (mTypeName != string.Empty)
			{
				if (leng == NetLanguage.CSharp)
					sb.Append("FieldConverter(typeof(" + mTypeName + ")");
				else if (leng == NetLanguage.VbNet)
					sb.Append("FieldConverter(GetType(" + mTypeName + ")");
			}
			else
				return string.Empty;
			
			if (mArg1 != null)
			{
				if (mArg1 is string)
					sb.Append(", \"" + mArg1.ToString() + "\"");
				else
					sb.Append(", " + mArg1.ToString());

				if (mArg2 != null)
				{
					if (mArg2 is string)
						sb.Append(", \"" + mArg2.ToString() + "\"");
					else
						sb.Append(", " + mArg2.ToString());
					
					if (mArg3 != null)
					{
						if (mArg3 is string)
							sb.Append(", \"" + mArg3.ToString() + "\"");
						else
							sb.Append(", " + mArg3.ToString());
					
					}
				}

			}
			
			sb.Append(")");
			
			return sb.ToString();
		}
	}
}
