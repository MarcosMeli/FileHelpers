using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Xml;
using FileHelpers.Helpers;

namespace FileHelpers.Dynamic
{
    /// <summary>Base class for the field converters</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class FieldBuilder
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string mFieldName;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string mFieldType;

        /// <summary>
        /// Create a field of Name with type
        /// </summary>
        /// <param name="fieldName">name of the field</param>
        /// <param name="fieldType">Type of the field</param>
        internal FieldBuilder(string fieldName, Type fieldType)
        {
            fieldName = fieldName.Trim();

            if (ValidIdentifierValidator.ValidIdentifier(fieldName) == false) {
                throw new FileHelpersException(Messages.Errors.InvalidIdentifier
                    .Identifier(fieldName)
                    .Text);
            }

            mFieldName = fieldName;
            mFieldType = ClassBuilder.TypeToString(fieldType);
        }

        /// <summary>
        /// Create a field of name and type
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="fieldType">Type of the field</param>
        internal FieldBuilder(string fieldName, string fieldType)
        {
            fieldName = fieldName.Trim();

            if (ValidIdentifierValidator.ValidIdentifier(fieldName) == false) {
                throw new FileHelpersException(Messages.Errors.InvalidIdentifier
                    .Identifier(fieldName)
                    .Text);
            }

            if (ValidIdentifierValidator.ValidIdentifier(fieldType, true) == false) {
                throw new FileHelpersException(Messages.Errors.InvalidIdentifier
                    .Identifier(fieldType)
                    .Text);
            }

            mFieldName = fieldName;
            mFieldType = fieldType;
        }

        #region TrimMode

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TrimMode mTrimMode = TrimMode.None;

        /// <summary>Indicates the TrimMode for the field.</summary>
        public TrimMode TrimMode
        {
            get { return mTrimMode; }
            set { mTrimMode = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string mTrimChars = " \t";

        /// <summary>Indicates the trim chars used if TrimMode is set.</summary>
        public string TrimChars
        {
            get { return mTrimChars; }
            set { mTrimChars = value; }
        }

        #endregion

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal int mFieldIndex = -1;

        /// <summary>The position index inside the class.</summary>
        public int FieldIndex
        {
            get { return mFieldIndex; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool mFieldInNewLine = false;

        /// <summary>Indicates that this field is at the beginning of a new line.</summary>
        public bool FieldInNewLine
        {
            get { return mFieldInNewLine; }
            set { mFieldInNewLine = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool mFieldHidden = false;

        /// <summary>Indicates that this field must be ignored by the engine.</summary>
        public bool FieldHidden
        {
            get { return mFieldHidden; }
            set { mFieldHidden = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool mFieldValueDiscarded = false;

        /// <summary>Discards the values for the target field.</summary>
        public bool FieldValueDiscarded
        {
            get { return mFieldValueDiscarded; }
            set { mFieldValueDiscarded = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool mFieldOptional = false;

        /// <summary>Indicates that this field is optional.</summary>
        public bool FieldOptional
        {
            get { return mFieldOptional; }
            set { mFieldOptional = value; }
        }

        /// <summary>Used to create the converter for the current field.</summary>
        public ConverterBuilder Converter
        {
            get { return mConverter; }
        }

        /// <summary>The name of the field.</summary>
        public string FieldName
        {
            get { return mFieldName; }
            set { mFieldName = value; }
        }

        /// <summary>The Type of the field</summary>
        public string FieldType
        {
            get { return mFieldType; }
            set { mFieldType = value; }
        }


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object mFieldNullValue = null;

        /// <summary>The null value of the field when their value not is in the file.</summary>
        public object FieldNullValue
        {
            get { return mFieldNullValue; }
            set { mFieldNullValue = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool mFieldNotEmpty = false;

        /// <summary>Indicates that the field cannot be empty.</summary>
        public bool FieldNotEmpty
        {
            get { return mFieldNotEmpty; }
            set { mFieldNotEmpty = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConverterBuilder mConverter = new ConverterBuilder();

        /// <summary>
        /// Create the field with attributes so that it can be added to the class
        /// </summary>
        /// <param name="lang">Language C# of Visual Basic</param>
        /// <returns>Field as text</returns>
        internal string GetFieldCode(NetLanguage lang)
        {
            var sb = new StringBuilder(100);

            var attbs = new AttributesBuilder(lang);

            AddAttributesInternal(attbs, lang);
            AddAttributesCode(attbs, lang);

            sb.Append(attbs.GetAttributesCode());

            NetVisibility visi = mVisibility;
            string currentName = mFieldName;

            if (mClassBuilder.GenerateProperties) {
                visi = NetVisibility.Private;
                currentName = "m" + mFieldName;
            }

            switch (lang) {
                case NetLanguage.VbNet:
                    sb.Append(ClassBuilder.GetVisibility(lang, visi) + currentName + " As " + mFieldType);
                    break;
                case NetLanguage.CSharp:
                    sb.Append(ClassBuilder.GetVisibility(lang, visi) + mFieldType + " " + currentName + ";");
                    break;
                default:
                    break;
            }

            sb.Append(StringHelper.NewLine);

            if (mClassBuilder.GenerateProperties) {
                sb.Append(StringHelper.NewLine);

                switch (lang) {
                    case NetLanguage.VbNet:
                        sb.Append("Public Property " + mFieldName + " As " + mFieldType);
                        sb.Append(StringHelper.NewLine);
                        sb.Append("   Get");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("      Return m" + mFieldName);
                        sb.Append(StringHelper.NewLine);
                        sb.Append("   End Get");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("   Set (value As " + mFieldType + ")");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("      m" + mFieldName + " = value");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("   End Set");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("End Property");
                        break;
                    case NetLanguage.CSharp:
                        sb.Append("public " + mFieldType + " " + mFieldName);
                        sb.Append(StringHelper.NewLine);
                        sb.Append("{");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("   get { return m" + mFieldName + "; }");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("   set { m" + mFieldName + " = value; }");
                        sb.Append(StringHelper.NewLine);
                        sb.Append("}");
                        break;
                    default:
                        break;
                }

                sb.Append(StringHelper.NewLine);
                sb.Append(StringHelper.NewLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Allow child classes to add attributes at the right spot
        /// </summary>
        /// <param name="attbs">Attributes added here</param>
        /// <param name="lang">Language  C# or Visual Basic</param>
        internal abstract void AddAttributesCode(AttributesBuilder attbs, NetLanguage lang);


        /// <summary>
        /// Add the general attributes to the field
        /// </summary>
        /// <param name="attbs">Attributes added here</param>
        /// <param name="lang">Language  C# or Visual Basic</param>
        private void AddAttributesInternal(AttributesBuilder attbs, NetLanguage lang)
        {
            if (mFieldOptional)
                attbs.AddAttribute("FieldOptional()");

            if (mFieldHidden)
                attbs.AddAttribute("FieldHidden()");

            if (mFieldValueDiscarded)
                attbs.AddAttribute("FieldValueDiscarded()");

            if (mFieldInNewLine)
                attbs.AddAttribute("FieldInNewLine()");

            if (mFieldNotEmpty)
                attbs.AddAttribute("FieldNotEmpty()");

            if (mFieldNullValue != null) {
                if (mFieldNullValue is string)
                    attbs.AddAttribute("FieldNullValue(\"" + mFieldNullValue.ToString() + "\")");
                else {
                    string t = ClassBuilder.TypeToString(mFieldNullValue.GetType());
                    string gt = string.Empty;
                    if (lang == NetLanguage.CSharp)
                        gt = "typeof(" + t + ")";
                    else if (lang == NetLanguage.VbNet)
                        gt = "GetType(" + t + ")";

                    attbs.AddAttribute("FieldNullValue(" + gt + ", \"" + mFieldNullValue.ToString() + "\")");
                }
            }


            attbs.AddAttribute(mConverter.GetConverterCode(lang));

            if (mTrimMode != TrimMode.None) {
                if (" \t" == mTrimChars)
                    attbs.AddAttribute("FieldTrim(TrimMode." + mTrimMode.ToString() + ")");
                else {
                    attbs.AddAttribute("FieldTrim(TrimMode." + mTrimMode.ToString() + ", \"" + mTrimChars.ToString() +
                                       "\")");
                }
            }
        }

        /// <summary>
        /// Parent class of this field
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal ClassBuilder mClassBuilder;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private NetVisibility mVisibility = NetVisibility.Public;

        /// <summary>
        /// Gets or sets the visibility of the field.
        /// </summary>
        public NetVisibility Visibility
        {
            get { return mVisibility; }
            set { mVisibility = value; }
        }

        /// <summary>
        /// Serialise the FiledBuilder to XML
        /// </summary>
        /// <param name="writer">writer to add XML to</param>
        internal void SaveToXml(XmlHelper writer)
        {
            writer.Writer.WriteStartElement("Field");
            writer.Writer.WriteStartAttribute("Name", "");
            writer.Writer.WriteString(mFieldName);
            writer.Writer.WriteEndAttribute();
            writer.Writer.WriteStartAttribute("Type", "");
            writer.Writer.WriteString(mFieldType);
            writer.Writer.WriteEndAttribute();
            WriteHeaderAttributes(writer);

            Converter.WriteXml(writer);

            writer.WriteElement("Visibility", this.Visibility.ToString(), "Public");
            writer.WriteElement("FieldHidden", this.FieldHidden);
            writer.WriteElement("FieldOptional", this.FieldOptional);
            writer.WriteElement("FieldValueDiscarded", this.FieldValueDiscarded);
            writer.WriteElement("FieldInNewLine", this.FieldInNewLine);
            writer.WriteElement("TrimChars", this.TrimChars, " \t");
            writer.WriteElement("TrimMode", this.TrimMode.ToString(), "None");

            if (FieldNullValue != null) {
                writer.Writer.WriteStartElement("FieldNullValue");
                writer.Writer.WriteStartAttribute("Type", "");
                writer.Writer.WriteString(ClassBuilder.TypeToString(mFieldNullValue.GetType()));
                writer.Writer.WriteEndAttribute();

                writer.Writer.WriteString(mFieldNullValue.ToString());

                writer.Writer.WriteEndElement();
            }

            WriteExtraElements(writer);
            writer.Writer.WriteEndElement();
        }

        /// <summary>
        /// Write any attributes to the first element
        /// </summary>
        /// <param name="writer"></param>
        internal abstract void WriteHeaderAttributes(XmlHelper writer);

        /// <summary>
        /// Write any extra fields to the end of the XML
        /// </summary>
        /// <param name="writer">Writer to output XML to</param>
        internal abstract void WriteExtraElements(XmlHelper writer);

        /// <summary>
        /// Read the generic XML elements and store in the field details
        /// </summary>
        /// <param name="node"></param>
        internal void ReadField(XmlNode node)
        {
            XmlNode ele;

            ele = node["Visibility"];
            if (ele != null)
                Visibility = (NetVisibility) Enum.Parse(typeof (NetVisibility), ele.InnerText);

            FieldHidden = node["FieldHidden"] != null || node["FieldNotInFile"] != null || node["FieldIgnored"] != null;
            FieldValueDiscarded = node["FieldValueDiscarded"] != null;
            FieldOptional = node["FieldOptional"] != null;
            FieldInNewLine = node["FieldInNewLine"] != null;

            ele = node["TrimChars"];
            if (ele != null)
                TrimChars = ele.InnerText;

            ele = node["TrimMode"];
            if (ele != null)
                TrimMode = (TrimMode) Enum.Parse(typeof (TrimMode), ele.InnerText);

            ele = node["FieldNullValue"];
            if (ele != null)
                FieldNullValue = Convert.ChangeType(ele.InnerText, Type.GetType(ele.Attributes["Type"].InnerText));

            ele = node["Converter"];
            if (ele != null)
                Converter.LoadXml(ele);

            ReadFieldInternal(node);
        }

        /// <summary>
        /// Read field details from the main XML element
        /// </summary>
        /// <param name="node">Node to read</param>
        internal abstract void ReadFieldInternal(XmlNode node);
    }
}