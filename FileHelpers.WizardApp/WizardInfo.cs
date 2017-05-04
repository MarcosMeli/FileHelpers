using System;
using System.Xml.Serialization;
using FileHelpers.Dynamic;

namespace FileHelpers.WizardApp
{
    [XmlInclude(typeof (DelimitedFieldBuilder))]
    [XmlInclude(typeof (FixedFieldBuilder))]
    public class WizardInfo
    {
        /// <summary>
        /// Allow the application to track the change of Language
        /// </summary>
        public event EventHandler<EventArgs> LanguageUpdated;

        private NetLanguage mLanguage;

        public NetLanguage Language
        {
            get { return mLanguage; }
            set
            {
                if (mLanguage == value)
                    return;
                mLanguage = value;
                if (LanguageUpdated != null)
                    LanguageUpdated(this, new EventArgs());
            }
        }

        private ClassBuilder mClassBuilder;

        /// <summary>
        /// Contains compiler and source management functions
        /// </summary>
        public ClassBuilder ClassBuilder
        {
            get { return mClassBuilder; }
            set { mClassBuilder = value; }
        }

        /// <summary>
        /// Complete class information about a delimited record
        /// </summary>
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

        /// <summary>
        /// Complete class information about a fixed length record
        /// </summary>
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

        /// <summary>
        /// Visibility of the fields created as a whole,  public, internal, protected or private
        /// </summary>
        public NetVisibility FieldVisibility
        {
            get { return mFieldVisibility; }
            set { mFieldVisibility = value; }
        }


        /// <summary>
        /// Get and set the visibility of the class as a whole,  internal or public
        /// </summary>
        public NetVisibility ClassVisibility
        {
            get { return mClassBuilder.Visibility; }
            set { mClassBuilder.Visibility = value; }
        }


        public string WizardOutput(NetLanguage lang)
        {
            if (mClassBuilder == null)
                return string.Empty;

            try {
                return mClassBuilder.GetClassSourceCode(lang);
            }
            catch (Exception ex) {
                //MessageBox.Show(ex.Message, "Error generating class code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ex.Message;
            }
        }

        private string mDefaultType;

        public string DefaultType
        {
            get { return mDefaultType; }
            set { mDefaultType = value; }
        }
    }
}