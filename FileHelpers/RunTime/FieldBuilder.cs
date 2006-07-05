using System;
using System.ComponentModel;
using System.Text;

namespace FileHelpers
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class FieldBuilder
	{
		private string mFieldName;
		private Type mFieldType;

		internal FieldBuilder(string fieldName, Type fieldType)
		{
			mFieldName = fieldName;
			mFieldType = fieldType;
		}

		#region TrimMode

		private TrimMode mTrimMode = TrimMode.None;

		public TrimMode TrimMode
		{
			get { return mTrimMode; }
			set { mTrimMode = value; }
		}
		
		private string mTrimChars = " \t";
		
		public string TrimChars
		{
			get { return mTrimChars; }
			set { mTrimChars = value; }
		}

		#endregion

		
		


		internal int mFieldIndex = -1;

		public int FieldIndex
		{
			get { return mFieldIndex; }
		}

		private bool mFieldInNewLine = false;

		public bool FieldInNewLine
		{
			get { return mFieldInNewLine; }
			set { mFieldInNewLine = value; }
		}

		private bool mFieldIgnored = false;

		public bool FieldIgnored
		{
			get { return mFieldIgnored; }
			set { mFieldIgnored = value; }
		}

		private bool mFieldOptional = false;

		public bool FieldOptional
		{
			get { return mFieldOptional; }
			set { mFieldOptional = value; }
		}

		public ConverterBuilder Converter
		{
			get { return mConverter; }
		}

		public string FieldName
		{
			get { return mFieldName; }
		}

		public Type FieldType
		{
			get { return mFieldType; }
		}

		private object mFieldNullValue = null;

		public object FieldNullValue
		{
			get { return mFieldNullValue; }
			set { mFieldNullValue = value; }
		}


		private ConverterBuilder mConverter = new ConverterBuilder();
		
		internal string GetFieldCode(NetLanguage leng)
		{
			StringBuilder sb = new StringBuilder(100);
			
			AttributesBuilder attbs = new AttributesBuilder(leng);
			
			AddAttributesInternal(attbs, leng);
			AddAttributesCode(attbs, leng);
			
			sb.Append(attbs.GetAttributesCode());
			
			switch (leng)
			{
				case NetLanguage.VbNet:
					sb.Append("Public " + mFieldName + " As " + mFieldType.FullName);
					break;
				case NetLanguage.CSharp:
					sb.Append("public " + mFieldType.FullName + " " + mFieldName+ ";");
					break;
				default:
					break;
			}

			sb.Append(StringHelper.NewLine);
			
			return sb.ToString();
		}
		
		
		internal abstract void AddAttributesCode(AttributesBuilder attbs, NetLanguage leng);

		private void AddAttributesInternal(AttributesBuilder attbs, NetLanguage leng)
		{

			if (mFieldOptional == true)
				attbs.AddAttribute("FieldOptional()");

			if (mFieldIgnored == true)
				attbs.AddAttribute("FieldIgnored()");

			if (mFieldInNewLine == true)
				attbs.AddAttribute("FieldInNewLine()");


			if (mFieldNullValue != null)
			{
				string t = mFieldNullValue.GetType().FullName;
				string gt = string.Empty;
				if (leng == NetLanguage.CSharp)
					gt = "typeof(" + t + ")";
				else if (leng == NetLanguage.VbNet)
					gt = "GetType(" + t + ")";

				attbs.AddAttribute("FieldNullValue("+ gt +", \""+ mFieldNullValue.ToString() +"\")");
			}

			
		
			attbs.AddAttribute(mConverter.GetConverterCode(leng));
			
			if (mTrimMode != TrimMode.None)
			{
				attbs.AddAttribute("FieldTrim(TrimMode."+ mTrimMode.ToString()+", \""+ mTrimChars.ToString() +"\")");
			}
		}
		
	}
}
