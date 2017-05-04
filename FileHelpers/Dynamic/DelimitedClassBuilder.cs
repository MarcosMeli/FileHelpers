using System;
using System.Data;
using System.Diagnostics;
using System.Xml;

namespace FileHelpers.Dynamic
{
    /// <summary>Used to create classes that maps to Delimited records.</summary>
    public class DelimitedClassBuilder : ClassBuilder
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string mDelimiter = string.Empty;

        /// <summary>The Delimiter that marks the end of each field.</summary>
        public string Delimiter
        {
            get { return mDelimiter; }
            set { mDelimiter = value; }
        }

        /// <summary>Return the field at the specified index.</summary>
        /// <param name="index">The index of the field.</param>
        /// <returns>The field at the specified index.</returns>
        public new DelimitedFieldBuilder FieldByIndex(int index)
        {
            return (DelimitedFieldBuilder) base.FieldByIndex(index);
        }

        /// <summary>Returns the current fields of the class.</summary>
        public new DelimitedFieldBuilder[] Fields
        {
            get { return (DelimitedFieldBuilder[]) mFields.ToArray(typeof (DelimitedFieldBuilder)); }
        }


        /// <summary>Creates a new DelimitedClassBuilder.</summary>
        /// <param name="className">The valid class name.</param>
        /// <param name="delimiter">The delimiter for that class.</param>
        public DelimitedClassBuilder(string className, string delimiter)
            : base(className)
        {
            mDelimiter = delimiter;
        }

        /// <summary>Creates a new DelimitedClassBuilder.</summary>
        /// <param name="className">The valid class name.</param>
        public DelimitedClassBuilder(string className)
            : this(className, string.Empty) {}

        /// <summary>Creates a new DelimitedClassBuilder with the same structure than a DataTable.</summary>
        /// <param name="className">The valid class name.</param>
        /// <param name="delimiter">The delimiter for that class.</param>
        /// <param name="dt">The DataTable from where to get the field names and types</param>
        public DelimitedClassBuilder(string className, string delimiter, DataTable dt)
            : this(className, delimiter)
        {
            foreach (DataColumn dc in dt.Columns)
                AddField(StringHelper.ToValidIdentifier(dc.ColumnName), dc.DataType);
        }

        /// <summary>Add a new Delimited field to the current class.</summary>
        /// <param name="fieldName">The Name of the field.</param>
        /// <param name="fieldType">The Type of the field.</param>
        /// <returns>The just created field.</returns>
        public virtual DelimitedFieldBuilder AddField(string fieldName, string fieldType)
        {
            var fb = new DelimitedFieldBuilder(fieldName, fieldType);
            AddFieldInternal(fb);
            return fb;
        }

        /// <summary>Add a new Delimited field to the current class.</summary>
        /// <param name="fieldName">The Name of the field.</param>
        /// <param name="fieldType">The Type of the field. (For generic of nullable types use the string overload, like "int?")</param>
        /// <returns>The just created field.</returns>
        public DelimitedFieldBuilder AddField(string fieldName, Type fieldType)
        {
            return AddField(fieldName, TypeToString(fieldType));
        }

        /// <summary>Add a new Delimited string field to the current class.</summary>
        /// <param name="fieldName">The Name of the string field.</param>
        /// <returns>The just created field.</returns>
        public virtual DelimitedFieldBuilder AddField(string fieldName)
        {
            return AddField(fieldName, "System.String");
        }

        /// <summary>Add a new Delimited field to the current class.</summary>
        /// <param name="field">The field definition.</param>
        /// <returns>The just added field.</returns>
        public DelimitedFieldBuilder AddField(DelimitedFieldBuilder field)
        {
            AddFieldInternal(field);
            return field;
        }

        /// <summary>Return the last added field. (use it reduce casts and code)</summary>
        public DelimitedFieldBuilder LastField
        {
            get
            {
                if (mFields.Count == 0)
                    return null;
                else
                    return (DelimitedFieldBuilder) mFields[mFields.Count - 1];
            }
        }

        /// <summary>
        /// Add any attributes to source (currently only the delimiter attribute)
        /// </summary>
        /// <param name="attbs">Attributes storage area to add to class</param>
        /// <param name="lang"></param>
        internal override void AddAttributesCode(AttributesBuilder attbs, NetLanguage lang)
        {
            if (mDelimiter == string.Empty)
                throw new BadUsageException("The Delimiter of the DelimiterClassBuilder can't be null or empty.");
            else
                attbs.AddAttribute("DelimitedRecord(" + GetDelimiter(mDelimiter, lang) + ")");
        }

        private static string GetDelimiter(string delimiter, NetLanguage lang)
        {
            switch (lang) {
                case NetLanguage.CSharp:
                    if (delimiter == "\t")
                        return "\"\\t\"";
                    else
                        return "\"" + delimiter + "\"";

                case NetLanguage.VbNet:
                    if (delimiter == "\t")
                        return "VbTab";
                    else
                        return "\"" + delimiter + "\"";
                default:
                    throw new ArgumentOutOfRangeException("lang");
            }
        }

        /// <summary>
        /// Serialise the XML header
        /// </summary>
        /// <param name="writer">Writer to serialise to</param>
        internal override void WriteHeaderElement(XmlHelper writer)
        {
            writer.Writer.WriteStartElement("DelimitedClass");
            writer.Writer.WriteStartAttribute("Delimiter", "");
            writer.Writer.WriteString(this.Delimiter);
            writer.Writer.WriteEndAttribute();
        }

        /// <summary>
        /// Write any extra elements (not used)
        /// </summary>
        /// <param name="writer"></param>
        internal override void WriteExtraElements(XmlHelper writer) {}

        /// <summary>
        /// extract delimited class details from xml
        /// </summary>
        /// <param name="document">XML document to check</param>
        /// <returns>delimited class information</returns>
        internal static DelimitedClassBuilder LoadXmlInternal(XmlDocument document)
        {
            DelimitedClassBuilder res;
            string del = document.SelectNodes("/DelimitedClass")[0].Attributes["Delimiter"].Value;

            string className = document.SelectNodes("/DelimitedClass/ClassName")[0].InnerText;

            res = new DelimitedClassBuilder(className, del);
            return res;
        }

        /// <summary>
        /// Extract the class element from the XML document
        /// </summary>
        /// <param name="document">XML to extract extra info from</param>
        internal override void ReadClassElements(XmlDocument document) {}

        /// <summary>
        /// Get the attributes off the XML element
        /// </summary>
        /// <param name="node">Node to read</param>
        internal override void ReadField(XmlNode node)
        {
            AddField(node.Attributes.Item(0).InnerText, node.Attributes.Item(1).InnerText).ReadField(node);
        }

        /// <summary>
        /// Adds n fields of type string, with the names "Field1", Field2", etc
        /// </summary>
        /// <param name="numberOfFields">The number of fields to add</param>
        public virtual void AddFields(int numberOfFields)
        {
            for (int i = 0; i < numberOfFields; i++)
                AddField("Field" + (i + 1).ToString().PadLeft(4, '0'));
        }
    }
}