#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.IO;

namespace FileHelpers.DataLink
{
	/// <summary>
	/// This class has the responsability to enable the two directional
	/// transformation.
	/// <list type="bullet">
	/// <item> File &lt;-> DataStorage </item>
	/// </list>
	/// </summary>
	/// <remarks>
	/// <para>Uses an <see cref="DataStorage"/> to accomplish this task.</para>
	/// <para>See in the <a href="class_diagram.html">Class Diagram</a> and in the <a href="example_datalink.html">DataLink Sample</a> for more Info.</para>
	/// </remarks>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="class_diagram.html">Class Diagram</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	/// <seealso href="example_datalink.html">Example of the DataLink</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
	public sealed class FileDataLink
	{
		#region "  Constructor  "

		/// <summary>Create a new instance of the class.</summary>
		/// <param name="provider">The <see cref="DataLink.DataStorage"/> used to performs the transformation.</param>
		public FileDataLink(DataStorage provider)
		{
			mProvider = provider;
			if (mProvider != null)
				mHelperEngine = new FileHelperEngine(mProvider.RecordType);
			else
				throw new ArgumentException("provider can´t be null", "provider");
		}

		#endregion

		#region "  HelperEngine  "

		private FileHelperEngine mHelperEngine;

		/// <summary> The internal <see cref="T:FileHelpers.FileHelperEngine"/> used to the file or stream ops. </summary>
		public FileHelperEngine FileHelperEngine
		{
			get { return mHelperEngine; }
		}

		#endregion

		#region "  DataLinkProvider  "

		DataStorage mProvider;

		/// <summary> The internal <see cref="T:FileHelpers.DataLink.IDataLinkProvider"/> used to the link ops. </summary>
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
		public object[] ExtractToFile(string fileName)
		{
			mLastExtractedRecords = mProvider.ExtractRecords();
			FileHelperEngine.WriteFile(fileName, mLastExtractedRecords);

			return mLastExtractedRecords;
		}

		/// <summary>
		/// Extract records from the data source and insert them to the specified stream using the DataLinkProvider <see cref="DataLink.DataStorage.ExtractRecords"/> method.
		/// </summary>
		/// <param name="writer">The stream where the records be written.</param>
		/// <returns>The extracted records</returns>
		public object[] ExtractToStream(StreamWriter writer)
		{
			mLastExtractedRecords = mProvider.ExtractRecords();
			FileHelperEngine.WriteStream(writer, mLastExtractedRecords);

			return mLastExtractedRecords;
		}

		#endregion

		#region "  InsertFromFile  "

		/// <summary>Extract records from a file and insert them to the data source using the DataLinkProvider <see cref="DataLink.DataStorage.InsertRecords"/> method.</summary>
		/// <param name="fileName">The file with the source records.</param>
		/// <returns>True if the operation is successful. False otherwise.</returns>
		public object[] InsertFromFile(string fileName)
		{
			mLastInsertedRecords = FileHelperEngine.ReadFile(fileName);
			mProvider.InsertRecords(mLastInsertedRecords);

			return mLastInsertedRecords;
		}

		/// <summary>Extract records from a stream and insert them to the data source using the DataLinkProvider <see cref="DataLink.DataStorage.InsertRecords"/> method.</summary>
		/// <param name="reader">The stream with the source records.</param>
		/// <returns>True if the operation is successful. False otherwise.</returns>
		public object[] InsertFromStream(StreamReader reader)
		{
			mLastInsertedRecords = FileHelperEngine.ReadStream(reader);
			mProvider.InsertRecords(mLastInsertedRecords);
			
			return mLastInsertedRecords ;
		}

		#endregion


		/// <summary>The short way to Extract the records from a DataStorage to a file</summary>
		/// <param name="storage">The DataStorage from where get the records</param>
		/// <param name="filename">The file where to write the records to.</param>
		/// <returns>The Extracted records.</returns>
		public static object[] EasyExtractToFile(DataStorage storage, string filename)
		{
			FileDataLink link = new FileDataLink(storage);
			return link.ExtractToFile(filename);
		}

		/// <summary>The short way to Insert Records from a file to a DataStorage</summary>
		/// <param name="storage">The DataStorage where store the records.</param>
		/// <param name="filename">The file with the SourceRecords</param>
		/// <returns>The Inserted records</returns>
		public static object[] EasyInsertFromFile(DataStorage storage, string filename)
		{
			FileDataLink link = new FileDataLink(storage);
			return link.InsertFromFile(filename);
		}

	}
}