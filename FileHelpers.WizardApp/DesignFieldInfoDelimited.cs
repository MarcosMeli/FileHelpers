using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FileHelpers.WizardApp
{
    public class DesignFieldInfoDelimited: DesignFieldInfoBase
    {

        protected override void AddAttributes(NetLenguage leng, StringBuilder sb)
        {
            if (string.IsNullOrEmpty(QuotedChar) == false)
            {
                sb.Append(IndentString);

                switch (leng)
                {
                    case NetLenguage.VbNet:
                        sb.Append("<FieldQuoted(\"" + QuotedChar + "\")> ");
                        break;
                    case NetLenguage.CSharp:
                        sb.AppendLine("[FieldQuoted(\"" + QuotedChar + "\")]");
                        break;
                    default:
                        break;
                }
            }
        }

        public DesignFieldInfoDelimited()
        { }

        public DesignFieldInfoDelimited(string name)
            : base(name)
        { }

        private string mQuotedChar;

        [Description("Indicated the Quoted Char of the field (empty is nothing)")]
        public string QuotedChar
        {
            get { return mQuotedChar; }
            set { mQuotedChar = value; }
        }

    }
}
