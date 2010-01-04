#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 
	// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.
#endregion

#if ! MINI
using System;
using System.Data;
using System.Data.SqlClient;

namespace FileHelpers.DataLink
{
	/// <summary>
	/// Base class for an SqlServer DataStorage.
	/// </summary>
	public abstract class SqlDataLinkProvider : DataBaseDataLinkProvider
	{
		#region "  Create Connection and Command  "

		/// <summary>Must create an abstract connection object.</summary>
		/// <returns>An Abstract Connection Object.</returns>
		protected sealed override IDbConnection CreateConnection()
		{
			// You must return here a Oracle connection
			return new OracleConnection("");
		}

		/// <summary>Must create an abstract command object.</summary>
		/// <returns>An Abstract Command Object.</returns>
		protected sealed override IDbCommand CreateCommand()
		{
			// You must return here a Oracle Command
			return new OracleCommand();
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
			get {return true;}
		}

	}
}

#endif
