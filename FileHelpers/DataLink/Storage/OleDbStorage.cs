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

		private string mConnectionString = string.Empty;

		public string ConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		public OleDbStorage(Type recordType, string oleDbConnString):base(recordType)
		{
			ConnectionString = oleDbConnString;
		}

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


	}

}

#endif