#undef GENERICS
//#define GENERICS
//#if NET_2_0


#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;

using FileHelpers.RunTime;

#if ! MINI
using System.Data;
#endif


namespace FileHelpers
{

	public sealed class CsvEngine : FileHelperEngine
	{

		private string mFileName;
		#region "  Constructor  "


		public CsvEngine(string filename, char delimiter, char delimiterHdr, string className): base(GetMappingClass(filename, delimiter, delimiterHdr, className))
		{
			mFileName = filename;
		}

		public CsvEngine(string filename, char delimiter, string className): this(filename, delimiter,  delimiter, className)
		{
		}



		#endregion

		private static Type GetMappingClass(string fileName, char delimiter, char delimiterHdr, string className)
		{
			CsvClassBuilder cb = new CsvClassBuilder(className, delimiter.ToString());
            
			string headers = CommonEngine.RawReadFirstLines(fileName, 1);

			foreach (string header in headers.Split(delimiterHdr))
			{
				cb.AddField(header.Replace(" ", "_"));
			}

			return cb.CreateRecordClass();
		}
	}
}

//#endif