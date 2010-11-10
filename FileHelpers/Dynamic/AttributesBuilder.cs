using System;
using System.Text;

namespace FileHelpers.Dynamic
{

    /// <summary>
    /// Create attributes in the specified language and return as text to paste
    /// into code
    /// </summary>
	internal sealed class AttributesBuilder
	{
        /// <summary>
        /// Attribute text is created here...
        /// </summary>
		StringBuilder mSb = new StringBuilder(250);

        /// <summary>
        /// C# or Visual Basic
        /// </summary>
		NetLanguage mLang;
		
        /// <summary>
        /// Create an attribute in the language selected
        /// </summary>
        /// <param name="lang"></param>
		public AttributesBuilder(NetLanguage lang)
		{
			mLang = lang;
		}
		
        /// <summary>
        /// Do we have any attributes, do we have to start and close VB attributes?
        /// </summary>
		private bool mFirst = true;

		/// <summary>
		/// Add an attribute,  C#  [att1] [att2],  VB   &lt;att1,_att2
		/// </summary>
		/// <param name="attribute"></param>
		public void AddAttribute(string attribute)
		{
			if (attribute == null || attribute == string.Empty)
				return;
			
			if (mFirst)
			{
				switch(mLang)
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
				switch(mLang)
				{
					case NetLanguage.VbNet:
						mSb.Append(", _"); //  new line continuation
						mSb.Append(StringHelper.NewLine);
                        mSb.Append(" ");
						break;
					case NetLanguage.CSharp:
						mSb.Append("[");
						break;
				}
			}
		
			mSb.Append(attribute);
						
			switch(mLang)
			{
				case NetLanguage.CSharp:
					mSb.Append("]");
					mSb.Append(StringHelper.NewLine);
					break;
				case NetLanguage.VbNet:
					break;
			}


		}
		
        /// <summary>
        /// Return all of attributes as text, if first then it is empty
        /// </summary>
        /// <returns></returns>
		public string GetAttributesCode()
		{
			if (mFirst == true)
				return string.Empty;
			
			switch(mLang)
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
