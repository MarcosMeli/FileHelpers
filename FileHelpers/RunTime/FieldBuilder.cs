using System;
using System.ComponentModel;
using System.Text;

namespace FileHelpers.RunTime
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class FieldBuilder
	{
		private string mFieldName;
		private string mFieldType;

		internal FieldBuilder(string fieldName, Type fieldType)
		{
			mFieldName = fieldName;
			mFieldType = fieldType.FullName;
		}

		internal FieldBuilder(string fieldName, string fieldType)
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
			set { mFieldName = value; }
		}

		public string FieldType
		{
			get { return mFieldType; }
			set { mFieldType = value; }
		}

		private object mFieldNullValue = null;

		public object FieldNullValue
		{
			get { return mFieldNullValue; }
			set { mFieldNullValue = value; }
		}


		private ConverterBuilder mConverter = new ConverterBuilder();
		
		internal string GetFieldCode(NetLanguage lang)
		{
			StringBuilder sb = new StringBuilder(100);
			
			AttributesBuilder attbs = new AttributesBuilder(lang);
			
			AddAttributesInternal(attbs, lang);
			AddAttributesCode(attbs, lang);
			
			sb.Append(attbs.GetAttributesCode());
			
			switch (lang)
			{
				case NetLanguage.VbNet:
					sb.Append(ClassBuilder.GetVisibility(lang, mVisibility) + mFieldName + " As " + mFieldType);
					break;
				case NetLanguage.CSharp:
					sb.Append(ClassBuilder.GetVisibility(lang, mVisibility) + mFieldType + " " + mFieldName+ ";");
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
		
		private NetVisibility mVisibility = NetVisibility.Public;

		public NetVisibility Visibility
		{
			get { return mVisibility; }
			set { mVisibility = value; }
		}

	}
}
