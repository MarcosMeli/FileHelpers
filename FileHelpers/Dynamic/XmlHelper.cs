using System;
using System.IO;
using System.Xml;

namespace FileHelpers.Dynamic
{
    /// <summary>
    /// XML wrapper to make life a little easier
    /// </summary>
	internal sealed class XmlHelper
        :IDisposable
	{
		/// <summary>
		/// XML writer
		/// </summary>
		internal XmlTextWriter Writer;
        /// <summary>
        /// XML reader
        /// </summary>
		internal XmlTextReader Reader;
		
        /// <summary>
        /// Open file in create more to add XML to it
        /// </summary>
        /// <param name="filename">Filename to create</param>
		public void BeginWriteFile(string filename)
		{
            BeginWriteStream(new StreamWriter(new FileStream(filename, FileMode.Create)));
		}

        /// <summary>
        /// Write XML onto this stream
        /// </summary>
        /// <param name="writer">writer to put XML onto</param>
        public void BeginWriteStream(TextWriter writer)
        {
            Writer = new XmlTextWriter(writer)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4
                };
        }

        /// <summary>
        /// Open a file for reading XML
        /// </summary>
        /// <param name="filename">filename to read</param>
		public void BeginReadFile(string filename)
		{
			Reader = new XmlTextReader(new StreamReader(filename));
		}

        /// <summary>
        /// Write an element with value in one hit
        /// </summary>
        /// <param name="element">Name of XML element</param>
        /// <param name="valueStr">Value of element</param>
		public void WriteElement(string element, string valueStr)
		{
			Writer.WriteStartElement(element);
			Writer.WriteString(valueStr);
			Writer.WriteEndElement();
		}

        /// <summary>
        /// Write an XML element, skipping value that is a default anyway
        /// </summary>
        /// <param name="element">element name</param>
        /// <param name="valueStr">value of the element</param>
        /// <param name="defaultVal">Default (or suppressed) value</param>
		public void WriteElement(string element, string valueStr, string defaultVal)
		{
			if (valueStr != defaultVal)
				WriteElement(element, valueStr);
		}

        /// <summary>
        /// Write an empty element, if the flag is true
        /// </summary>
        /// <param name="element">element name</param>
        /// <param name="mustWrite">do we write it</param>
		public void WriteElement(string element, bool mustWrite)
		{
            if (mustWrite)
			{
				Writer.WriteStartElement(element);
				Writer.WriteEndElement();
			}
		}

        /// <summary>
        /// Write an attribute if the value is not the default
        /// </summary>
        /// <param name="attb">attribute name</param>
        /// <param name="valueStr">value to be written</param>
        /// <param name="defaultVal">default value, if matched suppressed</param>
		public void WriteAttribute(string attb, string valueStr, string defaultVal)
		{
			if (valueStr != defaultVal)
				WriteAttribute(attb, valueStr);
		}

        /// <summary>
        /// Write an attribute onto an element
        /// </summary>
        /// <param name="attb">attribute to write</param>
        /// <param name="valueStr">value of the attribute</param>
		public void WriteAttribute(string attb, string valueStr)
		{
			Writer.WriteStartAttribute(attb, string.Empty);
			Writer.WriteString(valueStr);
			Writer.WriteEndAttribute();
		}

        /// <summary>
        /// Close the writer to flush XML
        /// </summary>
		public void EndWrite()
        {
            if (Writer != null) 
                Writer.Close();
            Writer = null;
        }

        /// <summary>
        /// Close the reader
        /// </summary>
        public void EndRead()
        {
            if (Reader != null) 
                Reader.Close();
            Reader = null;
        }

        /// <summary>
        /// Read the XML to the end of the element (skip attributes)
        /// </summary>
		public void ReadToNextElement()
		{
			while(Reader.Read())
				if (Reader.NodeType == XmlNodeType.Element)
					return;
		}

        public void Dispose()
        {
            EndRead();
            EndWrite();
        }
	}
}
