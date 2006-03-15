#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

#if ! MINI

using System.Data;
using System.Data.OleDb;

namespace FileHelpers.DataLink
{
	/// <summary>This is a base class that implements the <see cref="DataStorage"/> for Microsoft Access Files.</summary>
	public abstract class AccessStorage : DataBaseStorage
	{
		#region "  Create Connection and Command  "

		/// <summary>Must create an abstract connection object.</summary>
		/// <returns>An Abstract Connection Object.</returns>
		protected sealed override IDbConnection CreateConnection()
		{
			string conString = DataBaseHelper.GetAccessConnection(MdbFileName);
			return new OleDbConnection(conString);
		}

		/// <summary>Must create an abstract command object.</summary>
		/// <returns>An Abstract Command Object.</returns>
		protected sealed override IDbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		#endregion

		#region "  MustOverride  "

		/// <summary>The complete path to the access database.</summary>
		public abstract string MdbFileName { get; }

		#endregion
	}

}

#endif