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

        //public void LoadFields(Control.ControlCollection col)
        //{
        //    mFields = new ArrayList(col.Count);

        //    foreach (FieldBaseControl ctrl in col)
        //    {
        //        mFields.Add(ctrl.FieldInfo);
        //    }
        //}

        private ClassBuilder mClassBuilder;

        public ClassBuilder ClassBuilder
        {
            get { return mClassBuilder; }
            set { mClassBuilder = value; }
        }

        public DelimitedClassBuilder DelimitedBuilder
        {
            get 
            {
                if (mClassBuilder is DelimitedClassBuilder)
                    return (DelimitedClassBuilder) mClassBuilder;
                else
                    return null;
            }
        }

        public FixedLengthClassBuilder FixedLengthBuilder
        {
            get
            {
                if (mClassBuilder is FixedLengthClassBuilder)
                    return (FixedLengthClassBuilder) mClassBuilder;
                else
                    return null;
            }
        }

        private NetVisibility mFieldVisibility = NetVisibility.Public;

        public NetVisibility FieldVisibility
        {
            get { return mFieldVisibility; }
            set { mFieldVisibility = value; }
        }

        //private NetVisibility mClassVisibility = NetVisibility.Public;

        //public NetVisibility ClassVisibility
        //{
        //    get { return mClassVisibility; }
        //    set { mClassVisibility = value; }
        //}


        public string WizardOutput(NetLanguage lang)
        {
            if (mClassBuilder == null)
                return string.Empty;

            return mClassBuilder.GetClassSourceCode(lang);
        }

        private string mDefaultType;

        public string DefaultType
        {
            get { return mDefaultType; }
            set { mDefaultType = value; }
        }


    }
}
