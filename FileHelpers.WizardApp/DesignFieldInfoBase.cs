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
        {}

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

        private bool mIsOptional = false;
        
        
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

        public bool IsOptional
        {
            get { return mIsOptional; }
            set { mIsOptional = value; }
        }


        protected abstract void AddAttributes(NetLanguage leng, StringBuilder sb);


        private void AddMainAttributes(NetLanguage leng, StringBuilder sb)
        {
           AddAttributes(leng, sb);
        }



        public void FillFieldDefinition(NetLanguage leng, StringBuilder sb, bool properties)
        {
            AddMainAttributes(leng, sb);

            if (TrimMode != TrimMode.None)
            {
                switch (leng)
                {
                    case NetLanguage.VbNet:
                        sb.Append(IndentString);
                        sb.AppendLine("<FieldTrim(TrimMode." + TrimMode.ToString() + ")> ");
                        break;
                    case NetLanguage.CSharp:
                        sb.Append(IndentString);
                        sb.AppendLine("[FieldTrim(TrimMode." + TrimMode.ToString() + ")]");
                        break;
                    default:
                        break;
                }
            }

            if (mIsOptional == true)
            {
                switch (leng)
                {
                    case NetLanguage.VbNet:
                        sb.Append(IndentString);
                        sb.AppendLine("<FieldOptional()> _");
                        break;
                    case NetLanguage.CSharp:
                        sb.Append(IndentString);
                        sb.AppendLine("[FieldOptional()]");
                        break;
                    default:
                        break;
                }
            }
                
            if (sb.ToString(sb.Length - 2, 2) != Environment.NewLine)
                sb.AppendLine();
            sb.Append(IndentString);


            string visi = EnumHelper.GetVisibility(leng, mVisibility);

            string usedname;
                usedname = this.Name;

            if (properties)
            {
                usedname = "m" + this.Name;
                visi = EnumHelper.GetVisibility(leng, NetVisibility.Private);
            }

            switch (leng)
                {
                    case NetLanguage.VbNet:
                        sb.AppendLine(visi + " " + usedname + " As " + this.Type);
                        break;
                    case NetLanguage.CSharp:
                        sb.AppendLine(visi + " " + this.Type + " " + usedname + ";");
                        break;
                    default:
                        break;
                }

                sb.AppendLine();

        }
        

        
        public void CreateProperty(NetLanguage leng, StringBuilder sb)
        {

            switch (leng)
            {
                case NetLanguage.VbNet:
                    sb.Append(IndentString);
                    sb.AppendLine("Public Property " + this.Name + " As " + this.Type);
                    sb.Append(IndentString);
                    sb.AppendLine("Get");
                    sb.Append(IndentString);
                    sb.Append(IndentString);
                    sb.AppendLine("Return m" + this.Name);
                    sb.Append(IndentString);
                    sb.AppendLine("End Get");
                    sb.Append(IndentString);
                    sb.AppendLine("Set(Value As " + this.Type + ")");
                    sb.Append(IndentString);
                    sb.Append(IndentString);
                    sb.AppendLine("m" + this.Name + " = Value");
                    sb.Append(IndentString);
                    sb.AppendLine("End Set");
                    sb.Append(IndentString);
                    sb.AppendLine("End Property");
                    break;
                case NetLanguage.CSharp:
                    sb.Append(IndentString);
                    sb.AppendLine("public " + this.Type + " " + this.Name);
                    sb.Append(IndentString);
                    sb.AppendLine("{");
                    sb.Append(IndentString);
                    sb.Append(IndentString);
                    sb.AppendLine("get { return m" + this.Name + ";}");
                    sb.Append(IndentString);
                    sb.Append(IndentString);
                    sb.AppendLine("set { m" + this.Name + " = value;}");
                    sb.Append(IndentString);
                    sb.AppendLine("}");
                    break;
                default:
                    break;
            }

            sb.AppendLine();
            
        }



    }
}
