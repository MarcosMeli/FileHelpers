using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FileHelpers.WizardApp
{
    
    [XmlInclude(typeof(DesignFieldInfoDelimited))]
    [XmlInclude(typeof(DesignFieldInfoFixed))]
    public class WizardInfo
    {

        public void LoadFields(Control.ControlCollection col)
        {
            mFields = new ArrayList(col.Count);

            foreach (FieldBaseControl ctrl in col)
            {
                mFields.Add(ctrl.FieldInfo);
            }
        }


        private NetVisibility mFieldVisibility = NetVisibility.Public;

        public NetVisibility FieldVisibility
        {
            get { return mFieldVisibility; }
            set { mFieldVisibility = value; }
        }

        private NetVisibility mClassVisibility = NetVisibility.Public;

        public NetVisibility ClassVisibility
        {
            get { return mClassVisibility; }
            set { mClassVisibility = value; }
        }


        private string mClassName;

        public string ClassName
        {
            get { return mClassName; }
            set { mClassName = value; }
        }


        private bool mMarkAsSealed = true;

        public bool MarkAsSealed
        {
            get { return mMarkAsSealed; }
            set { mMarkAsSealed = value; }
        }


        private int mIgnoreFirst;

        public int IgnoreFirst
        {
            get { return mIgnoreFirst; }
            set { mIgnoreFirst = value; }
        }

        private int mIgnoreLast;

        public int IgnoreLast
        {
            get { return mIgnoreLast; }
            set { mIgnoreLast = value; }
        }

        private bool mUseProperties = false;
        
        public bool UseProperties
        {
            get { return mUseProperties; }
            set { mUseProperties = value; }
        }

        private RecordKind mRecordKind;

        public RecordKind RecordKind
        {
            get { return mRecordKind; }
            set { mRecordKind = value; }
        }

        private string mDelimiter = string.Empty;

        public string Delimiter
        {
            get { return mDelimiter; }
            set { mDelimiter = value; }
        }


        ArrayList mFields = new ArrayList();

        public ArrayList Fields
        { 
            get { return mFields; }
        }

        public string WizardOutput(NetLanguage leng)
        {
            StringBuilder sb = new StringBuilder(500);

            switch (leng)
            {
                case NetLanguage.VbNet:
                    if (RecordKind == RecordKind.FixedLength)
                        sb.AppendLine("<FixedLengthRecord()> _");
                    else
                        sb.AppendLine("<DelimitedRecord(\"" + Delimiter + "\") >_");

                    if (IgnoreFirst > 0)
                        sb.AppendLine("<IgnoreFirst(" + IgnoreFirst.ToString() + ")> _");

                    if (IgnoreLast > 0)
                        sb.AppendLine("<IgnoreLast(" + IgnoreLast.ToString() + ")> _");

                    sb.Append(EnumHelper.GetVisibility(leng, mClassVisibility));

                    if (mMarkAsSealed)
                        sb.Append(" Not Inheritable");

                    sb.AppendLine(" Class " + this.mClassName);
                    sb.AppendLine();
                    break;
                
                case NetLanguage.CSharp:
                    if (RecordKind == RecordKind.FixedLength)
                        sb.AppendLine("[FixedLengthRecord()]");
                    else
                        sb.AppendLine("[DelimitedRecord(\"" + Delimiter + "\")]");

                    if (IgnoreFirst > 0)
                        sb.AppendLine("[IgnoreFirst("+ IgnoreFirst.ToString() + ")]");

                    if (IgnoreLast > 0)
                        sb.AppendLine("[IgnoreLast(" + IgnoreLast.ToString() + ")]");

                    sb.Append(EnumHelper.GetVisibility(leng, mClassVisibility));

                    if (mMarkAsSealed)
                        sb.Append(" sealed");

                    sb.AppendLine(" class " + this.mClassName);
                    sb.AppendLine("{");
                    break;
                default:
                    break;
            }

            foreach (DesignFieldInfoBase info in mFields)
            {
                info.FillFieldDefinition(leng, sb, mUseProperties);

                if (mUseProperties)
                    info.CreateProperty(leng, sb);
            }

            // Append End

            switch (leng)
            {
                case NetLanguage.VbNet:
                    sb.AppendLine("End Class");
                    break;
                case NetLanguage.CSharp:
                    sb.AppendLine("}");
                    break;
                default:
                    break;
            }   


            return sb.ToString();
        }

        private string mDefaultType;

        public string DefaultType
        {
            get { return mDefaultType; }
            set { mDefaultType = value; }
        }


    }
}
