using System;
using System.Data;

namespace FileHelpers.DataLink
{
#if NET_20

    public sealed class GenericDatabaseStorage<T, U> : DatabaseStorage
        where T : IDbConnection, new( )
        where U : IDbCommand, new( )
    {
        #region Constructors

        public GenericDatabaseStorage( Type recordType, string connectionString ) : base( recordType )
        {
            _connectionString = connectionString;
        }

        #endregion

        #region Properties

        private string _connectionString = String.Empty;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        protected override bool ExecuteInBatch
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Private Methods

        protected sealed override IDbConnection CreateConnection( )
        {
            if ( String.IsNullOrEmpty( _connectionString ) )
                //throw new FileHelperException( "The connection cannot open because connection string is null or empty." );
                throw new Exception( "The connection cannot open because connection string is null or empty." );

            T connection = new T( );
            connection.ConnectionString = _connectionString;

            return connection;
        }

        protected override IDbCommand CreateCommand( )
        {
            return new U( );
        }

        #endregion
    }
#endif
}