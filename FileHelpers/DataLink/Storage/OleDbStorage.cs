#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

#if ! MINI

using System;
using System.Data;
using System.Data.OleDb;

namespace FileHelpers.DataLink
{

	/// <summary>This is a base class that implements the <see cref="DataStorage"/> for Microsoft Access Files.</summary>
	public sealed class OleDbStorage : DatabaseStorage
	{

		#region "  Constructors  "

		/// <summary>
		/// Create a new OleDbStorage based in the record type and in the connection string.
		/// </summary>
		/// <param name="recordType">The Type of the records.</param>
		/// <param name="oleDbConnString">The conection string used to create the OleDbConnection.</param>
		public OleDbStorage(Type recordType, string oleDbConnString):base(recordType)
		{
			mConnectionString = oleDbConnString;
		}

		#endregion

		#region "  Create Connection and Command  "

		/// <summary>Must create an abstract connection object.</summary>
		/// <returns>An Abstract Connection Object.</returns>
		protected sealed override IDbConnection CreateConnection()
		{
			if (mConnectionString == null || mConnectionString == string.Empty)
				throw new BadUsageException("The OleDb Connection string can´t be null or empty.");
			return new OleDbConnection(mConnectionString);
		}

		/// <summary>Must create an abstract command object.</summary>
		/// <returns>An Abstract Command Object.</returns>
		protected sealed override IDbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		#endregion

		#region "  ConnectionString  "

		private string mConnectionString = string.Empty;

		/// <summary>The conection string used to create the OleDbConnection.</summary>
		public string ConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		#endregion

	}
}

#endif