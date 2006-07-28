using System;
using System.ComponentModel;
using System.Text;

namespace FileHelpers.RunTime
{
	/// <summary>Used to build the ConverterAttribute for the run time classes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class ConverterBuilder
	{
		internal ConverterBuilder()
		{}

		private ConverterKind mKind = ConverterKind.None;

		/// <summary>The ConverterKind to be used , like the one in the FieldConverterAttribute.</summary>
		public ConverterKind Kind
		{
			get { return mKind; }
			set { mKind = value; }
		}

		private string mTypeName = string.Empty;

		/// <summary>The type name of your custom converter.</summary>
		public string TypeName
		{
			get { return mTypeName; }
			set { mTypeName = value; }
		}


		private string mArg1;
		private string mArg2;
		private string mArg3;

		/// <summary>The first argument pased to the converter.</summary>
		public string Arg1
		{
			get { return mArg1; }
			set { mArg1 = value; }
		}

		/// <summary>The first argument pased to the converter.</summary>
		public string Arg2
		{
			get { return mArg2; }
			set { mArg2 = value; }
		}

		
		/// <summary>The first argument pased to the converter.</summary>
		public string Arg3
		{
			get { return mArg3; }
			set { mArg3 = value; }
		}

	
		internal string GetConverterCode(NetLanguage leng)
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
				sb.Append(", \"" + mArg1 + "\"");

				if (mArg2 != null)
				{
					sb.Append(", \"" + mArg2 + "\"");
					
					if (mArg3 != null)
					{
						sb.Append(", \"" + mArg3 + "\"");
					}
				}
			}
			
			sb.Append(")");
			
			return sb.ToString();
		}
	}
}
