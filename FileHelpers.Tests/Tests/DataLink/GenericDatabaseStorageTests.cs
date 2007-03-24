using System;
using System.IO;
using System.Data.SqlClient;
using System.Data.OracleClient;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileHelpers;
using FileHelpers.DataLink;

namespace FileHelpers.Tests
{
    [TestClass]
    public class GenericDatabaseStorageTests
    {
        public GenericDatabaseStorageTests( ) { }

        [TestMethod]
        public void CurrencySqlExtract( )
        {
            GenericDatabaseStorage<SqlConnection, SqlCommand> storage = new GenericDatabaseStorage<SqlConnection, SqlCommand>( typeof( TestRecord ), "Data Source=FRENZY;Initial Catalog=Pruebas;User Id=sa;Password=Toshiba;" );

            storage.SelectSql = "SELECT * FROM Currency";
            storage.FillRecordCallback = new FillRecordHandler( FillRecordOrder );

            TestRecord[ ] res = storage.ExtractRecords( ) as TestRecord[ ];

            Assert.AreEqual( 105, res.Length );

            Assert.AreEqual( "AED", res[ 0 ].CurrencyCode );
            Assert.AreEqual( "AFA", res[ 1 ].CurrencyCode );
            Assert.AreEqual( "ALL", res[ 2 ].CurrencyCode );
        }

        [TestMethod]
        public void CurrencySqlExtractToFile( )
        {
            GenericDatabaseStorage<SqlConnection, SqlCommand> storage = new GenericDatabaseStorage<SqlConnection, SqlCommand>( typeof( TestRecord ), "Data Source=FRENZY;Initial Catalog=Pruebas;User Id=sa;Password=Toshiba;" );

            storage.SelectSql = "SELECT * FROM Currency";
            storage.FillRecordCallback = new FillRecordHandler( FillRecordOrder );

            FileDataLink.EasyExtractToFile( storage, "tempord.txt" );

            FileDataLink link = new FileDataLink( storage );
            link.ExtractToFile( "tempord.txt" );

            TestRecord[ ] res = CommonEngine.ReadFile( typeof( TestRecord ), "tempord.txt" ) as TestRecord[ ];

            if ( File.Exists( "tempord.txt" ) )
                File.Delete( "tempord.txt" );

            Assert.AreEqual( 105, res.Length );

            Assert.AreEqual( "AED", res[ 0 ].CurrencyCode );
            Assert.AreEqual( "AFA", res[ 1 ].CurrencyCode );
            Assert.AreEqual( "ALL", res[ 2 ].CurrencyCode );
        }

        [TestMethod]
        public void CurrencyOracleExtract( )
        {
            GenericDatabaseStorage<OracleConnection, OracleCommand> storage = new GenericDatabaseStorage<OracleConnection, OracleCommand>( typeof( TestRecord ), "User Id=SHELL;Password=shell;Data Source=ora9dev" );

            storage.SelectSql = "SELECT * FROM CURRENCY";
            storage.FillRecordCallback = new FillRecordHandler( FillRecordOrder );

            TestRecord[ ] res = storage.ExtractRecords( ) as TestRecord[ ];

            Assert.AreEqual( 3, res.Length );

            Assert.AreEqual( "AED", res[ 0 ].CurrencyCode );
            Assert.AreEqual( "AFA", res[ 1 ].CurrencyCode );
            Assert.AreEqual( "ALL", res[ 2 ].CurrencyCode );
        }

        [TestMethod]
        public void CurrencyOracleExtractToFile( )
        {
            GenericDatabaseStorage<OracleConnection, OracleCommand> storage = new GenericDatabaseStorage<OracleConnection, OracleCommand>( typeof( TestRecord ), "User Id=SHELL;Password=shell;Data Source=ora9dev" );

            storage.SelectSql = "SELECT * FROM CURRENCY";
            storage.FillRecordCallback = new FillRecordHandler( FillRecordOrder );

            FileDataLink.EasyExtractToFile( storage, "tempord.txt" );

            FileDataLink link = new FileDataLink( storage );
            link.ExtractToFile( "tempord.txt" );

            TestRecord[ ] res = CommonEngine.ReadFile( typeof( TestRecord ), "tempord.txt" ) as TestRecord[ ];

            if ( File.Exists( "tempord.txt" ) )
                File.Delete( "tempord.txt" );

            Assert.AreEqual( 3, res.Length );

            Assert.AreEqual( "AED", res[ 0 ].CurrencyCode );
            Assert.AreEqual( "AFA", res[ 1 ].CurrencyCode );
            Assert.AreEqual( "ALL", res[ 2 ].CurrencyCode );
        }
        
        public void FillRecordOrder( object rec, object[ ] fields )
        {
            TestRecord record = ( TestRecord )rec;

            record.CurrencyCode = ( string )fields[ 0 ];
            record.Name = ( string )fields[ 1 ];
        }
    }

    [DelimitedRecord( "|" )]
    public class TestRecord
    {
        public string CurrencyCode;
        public string Name;
    }
}