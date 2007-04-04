#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers.DataLink
{
	/// <summary>
	/// This class has the responsability to enable the two directional
	/// transformation.
	/// <list type="bullet">
	/// <item> Excel &lt;-> DataStorage</item>
	/// </list>
	/// <para><b>WARNING you need to have installed Microsoft Excel 2000 or newer to use this feature.</b></para>
	/// <para><b>To use this class you need to reference the FileHelpers.ExcelStorage.dll file.</b></para>
	/// </summary>
	/// <remarks>
	/// <para>Uses an <see cref="DataStorage"/> to accomplish this task.</para>
	/// </remarks>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="class_diagram.html">Class Diagram</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	/// <seealso href="example_datalink.html">Example of the DataLink</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
	public sealed class ExcelDataLink
	{
		#region "  Constructor  "

		/// <summary>Create a new instance of the class.</summary>
		/// <param name="provider">The <see cref="DataLink.DataStorage"/> used to performs the transformation.</param>
		public ExcelDataLink(DataStorage provider)
		{
			mProvider = provider;
			if (mProvider != null)
				mExcelStorage = new ExcelStorage(provider.RecordType);
			else
				throw new ArgumentException("provider can´t be null", "provider");
		}

		#endregion

		#region "  ExcelStorage  "

		private ExcelStorage mExcelStorage;

		/// <summary> The internal <see cref="T:FileHelpers.FileHelperEngine"/> used to the file or stream ops. </summary>
		public ExcelStorage ExcelStorage
		{
			get { return mExcelStorage; }
		}

		#endregion

		#region "  DataLinkProvider  "

		DataStorage mProvider;

		/// <summary> The internal <see cref="T:FileHelpers.DataLink.DataStorage"/> used to the link ops. </summary>
		public DataStorage DataStorage
		{
			get { return mProvider; }
		}

		#endregion

		#region "  Last Records "

		private object[] mLastExtractedRecords;

		/// <summary>
		/// An array of the last records extracted from the data source to a file.
		/// </summary>
		public object[] LastExtractedRecords
		{
			get { return mLastExtractedRecords; }
		}

		private object[] mLastInsertedRecords;

		/// <summary>
		/// An array of the last records inserted in the data source that comes from a file.
		/// </summary>
		public object[] LastInsertedRecords
		{
			get { return mLastInsertedRecords; }
		}

		#endregion

		#region "  ExtractTo File/Stream   "

		/// <summary>
		/// Extract records from the data source and insert them to the specified file using the DataLinkProvider <see cref="DataLink.DataStorage.ExtractRecords"/> method.
		/// </summary>
		/// <param name="fileName">The files where the records be written.</param>
		/// <returns>True if the operation is successful. False otherwise.</returns>
		public bool ExtractToExcel(string fileName)
		{
			mLastExtractedRecords = mProvider.ExtractRecords();
			mExcelStorage.InsertRecords(mLastExtractedRecords);
			return true;
		}

		#endregion

		#region "  InsertFromFile  "

		/// <summary>Extract records from a file and insert them to the data source using the DataLinkProvider <see cref="DataLink.DataStorage.InsertRecords"/> method.</summary>
		/// <param name="excelFileName">The file with the source records.</param>
		/// <returns>True if the operation is successful. False otherwise.</returns>
		public bool InsertFromExcel(string excelFileName)
		{
			mLastInsertedRecords = mExcelStorage.ExtractRecords();
			mProvider.InsertRecords(mLastInsertedRecords);
			return true;
		}

		#endregion
	}
}