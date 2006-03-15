#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

#if ! MINI
using System;
using System.Data;
using System.Data.SqlClient;

namespace FileHelpers.DataLink
{
	/// <summary>This is a base class that implements the <see cref="DataStorage"/> for Microsoft SqlServer.</summary>
	public abstract class SqlServerStorage : DataBaseStorage
	{
		#region "  Create Connection and Command  "

		/// <summary>Must create an abstract connection object.</summary>
		/// <returns>An Abstract Connection Object.</returns>
		protected sealed override IDbConnection CreateConnection()
		{
			string conString = DataBaseHelper.SqlConnectionString(ServerName, DataBaseName, UserName, UserPass);
			return new SqlConnection(conString);
		}

		/// <summary>Must create an abstract command object.</summary>
		/// <returns>An Abstract Command Object.</returns>
		protected sealed override IDbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		#endregion

		#region "  MustOverride  "

		/// <summary> The server name or IP. </summary>
		public abstract string ServerName { get; }

		/// <summary> The name of the database. </summary> 
		public abstract string DataBaseName { get; }

		/// <summary> The user name used to logon to the SqlServer. </summary>
		/// <remarks> Leave empty for WIndows Authentication. </remarks>
		public virtual string UserName
		{
			get { return String.Empty; }
		}

		/// <summary> The user pass used to logon to the SqlServer. </summary>
		/// <remarks> Leave empty for WIndows Authentication. </remarks>
		public virtual string UserPass
		{
			get { return String.Empty; }
		}

		#endregion

		internal override bool ExecuteInBatch
		{
			get { return true; }
		}

	}
}

#endif