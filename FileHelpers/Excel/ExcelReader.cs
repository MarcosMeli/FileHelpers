#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace FileHelpers.DataLink
{
	public class ExcelReader: ExcelHelper
	{
		#region "  Constructors  "

		public ExcelReader()
		{}

		public ExcelReader(int startRow, int startCol): base(startRow, startCol)
		{}

		#endregion


		private bool mReadAllAsText = false;

		public bool ReadAllAsText
		{
			get { return mReadAllAsText; }
			set { mReadAllAsText = value; }
		}

		private void ValidatePropertiesForExtract()
        {

            if (this.StartRow <= 0 )
                throw new BadUsageException("The StartRow Property is Invalid. Must be Greater or Equal Than 1.");

//            if (this.StartRow > mDtExcel.Rows.Count)
//                throw new BadUsageException("The StartRow Property is Invalid. Must be Less or Equal to Worksheet row's count.");

            if (this.StartColumn <= 0)
                throw new BadUsageException("The StartColumn Property is Invalid. Must be Greater or Equal Than 1.");

//            if (this.StartColumn > mDtExcel.Columns.Count)
//                throw new BadUsageException("The StartColumn Property is Invalid. Must be Less or Equal To Worksheet Column's count.");

        }


		public DataTable ExtractDataTable(string file)
		{
			return ExtractDataTable(file, StartRow, StartColumn);
		}

		public DataTable ExtractDataTable(string file, int row, int col)
		{

			ValidatePropertiesForExtract();

			OleDbConnection connExcel;
			//private OleDbDataAdapter mDaExcel;
				
			connExcel= new OleDbConnection(CreateConnectionString(file));
			connExcel.Open();
			DataTable res = new DataTable();

			string sheetName = GetFirstSheet(connExcel);

			string sheet = sheetName + (sheetName.EndsWith("$") ? "" : "$") ;
			string command = String.Format("SELECT * FROM [{0}]", sheet);

			OleDbCommand cm = new OleDbCommand(command, connExcel);
			OleDbDataAdapter da = new OleDbDataAdapter(cm);
			da.Fill(res);

			connExcel.Close();
			return res;

		}

		protected override string ExtraProps()
		{
			if (mReadAllAsText)
				return " IMEX=1;";

			return string.Empty;
		}



	}
}