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
	public sealed class AccessStorage : DatabaseStorage
	{

		private string mAccessFile = string.Empty;
		private string mAccessPassword = string.Empty;

		public AccessStorage(Type recordType, string accessFile):base(recordType)
		{
			AccessFileName = accessFile;
		}

		#region "  Create Connection and Command  "

		/// <summary>Must create an abstract connection object.</summary>
		/// <returns>An Abstract Connection Object.</returns>
		protected sealed override IDbConnection CreateConnection()
		{
            string conString = DataBaseHelper.GetAccessConnection(AccessFileName, AccessFilePassword);
			return new OleDbConnection(conString);
		}

		/// <summary>Must create an abstract command object.</summary>
		/// <returns>An Abstract Command Object.</returns>
		protected sealed override IDbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		#endregion

        /// <summary>The password to the access database.</summary>
        public string AccessFilePassword
        {
        	get{ return mAccessPassword; }
			set{ mAccessPassword = value; }
		}


		/// <summary>The file full path of the Microsoft Access File.</summary>
		public string AccessFileName
		{
			get { return mAccessFile; }
			set { mAccessFile = value; }
		}


	}

}

#endif