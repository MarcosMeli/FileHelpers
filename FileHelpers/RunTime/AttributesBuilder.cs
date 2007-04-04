using System;
using System.Text;

namespace FileHelpers.RunTime
{

	internal sealed class AttributesBuilder
	{
		StringBuilder mSb = new StringBuilder(250);
		NetLanguage mLeng;
		
		public AttributesBuilder(NetLanguage lang)
		{
			mLeng = lang;
		}
		
		private bool mFirst = true;
		
		public void AddAttribute(string attribute)
		{
			if (attribute == null || attribute == string.Empty)
				return;
			
			if (mFirst)
			{
				switch(mLeng)
				{
					case NetLanguage.CSharp:
						mSb.Append("[");
						break;
					case NetLanguage.VbNet:
						mSb.Append("<");
						break;
				}
				mFirst = false;
			}
			else
			{
				switch(mLeng)
				{
					case NetLanguage.VbNet:
						mSb.Append(", _");
						mSb.Append(StringHelper.NewLine);
                        mSb.Append(" ");
						break;
					case NetLanguage.CSharp:
						mSb.Append("[");
						break;
				}
			}
		
			mSb.Append(attribute);
						
			switch(mLeng)
			{
				case NetLanguage.CSharp:
					mSb.Append("]");
					mSb.Append(StringHelper.NewLine);
					break;
				case NetLanguage.VbNet:
					break;
			}


		}
		
		public string GetAttributesCode()
		{
			if (mFirst == true)
				return string.Empty;
			
			switch(mLeng)
			{
				case NetLanguage.VbNet:
					mSb.Append("> _");
					mSb.Append(StringHelper.NewLine);
					break;
			}
			
			return mSb.ToString();
		}
	}
}
