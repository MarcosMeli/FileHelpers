using System;
using System.IO;
using System.Xml;

namespace FileHelpers.RunTime
{
	internal sealed class XmlHelper
	{
		public XmlHelper()
		{}
		
		internal XmlTextWriter mWriter;
		internal XmlTextReader mReader;
		
		public void BeginWriteFile(string filename)
		{
			mWriter = new XmlTextWriter(new StreamWriter(filename));
			mWriter.Formatting = Formatting.Indented;
			mWriter.Indentation = 4;
		}

		public void BeginReadFile(string filename)
		{
			mReader = new XmlTextReader(new StreamReader(filename));
		}

		public void WriteElement(string element, string valueStr)
		{
			mWriter.WriteStartElement(element);
			mWriter.WriteString(valueStr);
			mWriter.WriteEndElement();
		}

		public void WriteElement(string element, string valueStr, string defaultVal)
		{
			if (valueStr != defaultVal)
				WriteElement(element, valueStr);
		}

		public void WriteElement(string element, bool mustWrite)
		{
            if (mustWrite)
			{
				mWriter.WriteStartElement(element);
				mWriter.WriteEndElement();
			}
		}

		public void WriteAttribute(string attb, string valueStr, string defaultVal)
		{
			if (valueStr != defaultVal)
				WriteAttribute(attb, valueStr);
		}

		public void WriteAttribute(string attb, string valueStr)
		{
			mWriter.WriteStartAttribute(attb, string.Empty);
			mWriter.WriteString(valueStr);
			mWriter.WriteEndAttribute();
		}

		public void EndWrite()
		{
			mWriter.Close();
			mWriter = null;
		}
		
		public void ReadToNextElement()
		{
			while(mReader.Read())
				if (mReader.NodeType == XmlNodeType.Element)
					return;
		}

		
	}
}
