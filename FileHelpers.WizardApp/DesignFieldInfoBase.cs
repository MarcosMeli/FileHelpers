using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace FileHelpers.WizardApp
{
    public abstract class DesignFieldInfoBase
    {

        protected string IndentString = "  ";

        public DesignFieldInfoBase()
        {
        }

        public DesignFieldInfoBase(string name)
        {
            this.mName = name;
        }
        

        private string mName = "NewField";
        [Description("The name of the Field")]
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        private string mType = "string";

        [Description("The declaring Type of the Field")]
        public string Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        private NetVisibility mVisibility;

        public NetVisibility Visibility
        {
            get { return mVisibility; }
            set { mVisibility = value; }
        }

        private TrimMode mTrimMode;

        public TrimMode TrimMode
        {
            get { return mTrimMode; }
            set { mTrimMode = value; }
        }


        protected abstract void AddAttributes(NetLanguage leng, StringBuilder sb);


        private void AddMainAttributes(NetLanguage leng, StringBuilder sb)
        {
           AddAttributes(leng, sb);
        }



        public void FillFieldDefinition(NetLanguage leng, StringBuilder sb)
        {
            AddMainAttributes(leng, sb);

            if (TrimMode != TrimMode.None)
            {
                switch (leng)
                {
                    case NetLanguage.VbNet:
                        sb.Append(IndentString);
                        sb.Append("<FieldTrim(TrimMode." + TrimMode.ToString() + ")> ");
                        break;
                    case NetLanguage.CSharp:
                        sb.Append(IndentString);
                        sb.AppendLine("[FieldTrim(TrimMode." + TrimMode.ToString() + ")]");
                        break;
                    default:
                        break;
                }
            }

            if (sb.ToString(sb.Length - 2, 2) != Environment.NewLine)
                sb.AppendLine();
            sb.Append(IndentString);


            string visi = EnumHelper.GetVisibility(leng, mVisibility);

            switch (leng)
            {
                case NetLanguage.VbNet:
                    sb.AppendLine( visi + " " + this.Name + " As " + this.Type );
                    break;
                case NetLanguage.CSharp:
                    sb.AppendLine( visi + " " + this.Type + " " + this.Name + ";");
                    break;
                default:
                    break;
            }
        }



    }
}
