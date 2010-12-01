using System;

namespace FileHelpers.DataLink
{
	/// <summary>
    /// This class implements the <see cref="DataStorage"/>
    /// for plain text files.
    /// </summary>
	public sealed class FileStorage : DataStorage
	{
        /// <summary>
        /// Engine to use for processing
        /// </summary>
		private FileHelperEngine mEngine;

        /// <summary>
        /// Filename being written to
        /// </summary>
		private string mFileName;

		/// <summary>
        /// Create simple FileStorage object to work with the specified type.</summary>
		/// <param name="type">The record class.</param>
		/// <param name="fileName">The target filename.</param>
		public FileStorage(Type type, string fileName):base(type)
		{
			if (type == null)
				throw new BadUsageException("You need to pass a non null Type to the FileStorage.");

			mEngine = new FileHelperEngine(type);
			mErrorManager = mEngine.ErrorManager;

			mFileName = fileName;
		}


		#region "  SelectRecords  "

		/// <summary>
        /// Must Return the records from the DataSource (DB, Excel, etc)
        /// </summary>
		/// <returns>The extracted records.</returns>
		public override object[] ExtractRecords()
		{
			return mEngine.ReadFile(mFileName);
		}

		#endregion

		/// <summary>The engine behind the FileStorage.</summary>
		public FileHelperEngine Engine
		{
			get { return mEngine; }
		}

		/// <summary>The target file name.</summary>
		public string FileName
		{
			get { return mFileName; }
			set { mFileName = value; }
		}

		#region "  InsertRecords  "

		/// <summary>
        /// Insert the records in a DataSource (DB, Excel, etc)
        /// </summary>
		/// <param name="records">The records to insert.</param>
		public override void InsertRecords(object[] records)
		{
			if (mFileName == null || mFileName.Length == 0)
				throw new BadUsageException("You need to set a FileName to the FileStorage Provider.");

			mEngine.WriteFile(mFileName, records);
		}

		#endregion
	}
}