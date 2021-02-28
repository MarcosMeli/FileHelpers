#if !NETSTANDARD
using System;
using System.Data;

namespace FileHelpers.DataLink
{
    /// <summary>
    /// This is a base class that implements the storage for <b>any</b> DB with
    /// ADO.NET support.
    /// </summary>
    /// <typeparam name="ConnectionClass">The ADO.NET connection class</typeparam>
    [Obsolete("Datalink feature is outdated and will be rewritten, see https://www.filehelpers.net/mustread/")]
    public sealed class GenericDatabaseStorage<ConnectionClass> : DatabaseStorage
        where ConnectionClass : IDbConnection, new()
    {
        #region Constructors

        /// <summary>
        /// Creates an object that implements the storage for <b>any</b> DB
        /// with ADO.NET support.
        /// </summary>        
        /// <param name="recordType">The record type to use.</param>
        /// <param name="connectionString">The connection string to </param>
        public GenericDatabaseStorage(Type recordType, string connectionString)
            : base(recordType)
        {
            ConnectionString = connectionString;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Do we get data in batches or single records
        /// </summary>
        protected override bool ExecuteInBatch
        {
            get { return true; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Connect to a data source using the defined connection string
        /// </summary>
        /// <returns>Database connection object</returns>
        protected override sealed IDbConnection CreateConnection()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                //throw new FileHelpersException( "The connection cannot open because connection string is null or empty." );
                throw new Exception("The connection cannot open because connection string is null or empty.");

            ConnectionClass connection = new ConnectionClass();
            connection.ConnectionString = ConnectionString;

            return connection;
        }

        #endregion
    }
}
#endif
