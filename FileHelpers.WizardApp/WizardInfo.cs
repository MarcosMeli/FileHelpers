using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Xml.Serialization;
using FileHelpers.RunTime;

namespace FileHelpers.WizardApp
{
    
    [XmlInclude(typeof(DelimitedFieldBuilder))]
    [XmlInclude(typeof(FixedFieldBuilder))]
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

        private ClassBuilder mClassBuilder = new ClassBuilder();

        public ClassBuilder ClassBuilder
        {
            get { return mClassBuilder; }
            set { mClassBuilder = value; }
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

        private bool mIgnoreEmptyLines = false;

        public bool IgnoreEmptyLines
        {
            get { return mIgnoreEmptyLines; }
            set { mIgnoreEmptyLines = value; }
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





        public string WizardOutput(NetLanguage leng)
        {
            return mClassBuilder.GetSourceCode();
        }

        private string mDefaultType;

        public string DefaultType
        {
            get { return mDefaultType; }
            set { mDefaultType = value; }
        }


    }
}
