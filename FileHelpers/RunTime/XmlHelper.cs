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

		public void EndWrite()
		{
			mWriter.Close();
			mWriter = null;
		}

		
	}
}
