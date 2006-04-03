using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace FileHelpers.WizardApp
{
    public class DesignFieldInfoFixed: DesignFieldInfoBase
    {
        public DesignFieldInfoFixed()
        {}

        public DesignFieldInfoFixed(string name)
            : base(name)
        { }

        private int mLength;

        [Description("Set the Size of the Field")]
        public int FieldLength
        {
            get { return mLength; }
            set { mLength = value; }
        }

        protected override void AddAttributes(NetLanguage leng, StringBuilder sb)
        {
            sb.Append(IndentString);

            switch (leng)
            {
                case NetLanguage.VbNet:
                    sb.Append("<FieldFixedLength(" + this.FieldLength.ToString() + ")> ");
                    break;
                case NetLanguage.CSharp:
                    sb.AppendLine("[FieldFixedLength(" + this.FieldLength.ToString() + ")]");
                    break;
                default:
                    break;
            }
        }



    }
}
