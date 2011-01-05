using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileHelpers.WizardApp
{
    /// <summary>
    /// Create a list of defined languages and their NetLanguage type
    /// </summary>
    public class NetLanguageList
    {
        public class LanguageType
        {
            public string Text;

            public NetLanguage Language;

            public override string ToString()
            {
                return Text;
            }
        }

        private static List<LanguageType> _Languages;

        public static List<LanguageType> Languages { get { return
_Languages; } }

        static NetLanguageList()
        {
            _Languages = new List<LanguageType>();
            LanguageType temp = new LanguageType()
            {
                Language = NetLanguage.CSharp,
                Text = "C#",
            };
            _Languages.Add(temp);
            temp = new LanguageType()
            {
                Language = NetLanguage.VbNet,
                Text = "VB.NET",
            };
            _Languages.Add(temp);
        }
    }
}
