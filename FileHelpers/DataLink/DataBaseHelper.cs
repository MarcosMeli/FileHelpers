#if ! MINI

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    ///Create a database connector
    /// </summary>
    /// <remarks>
    /// Access connection May not work on Windows 7 because Jet is not installed by default
    /// </remarks>
    internal static class DataBaseHelper
    {
        #region "  AccessConnection  "

        /// <summary>
        /// generic access connection string
        /// </summary>
        private const string AccessConnStr =
            @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=<PASSWORD>;Data Source=""<BASE>"";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";

        /// <summary>
        /// Connect to a database with no password required
        /// </summary>
        /// <param name="db">Database we want to connect to</param>
        /// <returns>connection string</returns>
        public static string GetAccessConnection(string db)
        {
            return GetAccessConnection(db, "");
        }

        /// <summary>
        /// Connect to an access database with a password
        /// </summary>
        /// <param name="db">database we want to connect to</param>
        /// <param name="password">password for database</param>
        /// <returns>connection string</returns>
        public static string GetAccessConnection(string db, string password)
        {
            string res = AccessConnStr;
            res = res.Replace("<BASE>", db);

            res = res.Replace("<PASSWORD>", password ?? string.Empty);
            return res;
        }

        #endregion

        #region "  SqlConnectionString  "

        private static string AppName = "FileHelpers"; //<- For display in SqlServer

        /// <summary>
        /// connect to SQL server without userid and password
        /// </summary>
        /// <param name="server">SQL server connection</param>
        /// <param name="dbName">database name to connect to</param>
        /// <returns>SQL server connection string</returns>
        public static string SqlConnectionString(string server, string dbName)
        {
            return SqlConnectionString(server, dbName, "", "");
        }

        /// <summary>
        /// connect to SQL server with userid and password
        /// </summary>
        /// <param name="server">SQL server connection</param>
        /// <param name="dbName">database name to connect to</param>
        /// <param name="pass">Database password</param>
        /// <param name="user">databsae user name</param>
        /// <returns>SQL server connection string</returns>
        public static string SqlConnectionString(string server, string dbName, string user, string pass)
        {
            StringBuilder sCadena = new StringBuilder(300);
            if (user.Length == 0 &&
                pass.Length == 0) {
                sCadena =
                    new StringBuilder("data source=<SERVER>;persist security info=True;" +
                                      "initial catalog=<BASE>;integrated security=SSPI;" +
                                      "packet size=4096;Connection Timeout=10;Application Name=" + AppName);
            }
            else {
                sCadena =
                    new StringBuilder("data source=<SERVER>;persist security info=True;" +
                                      "initial catalog=<BASE>;User Id=\"<USER>\";Password=\"<PASS>\";" +
                                      "packet size=4096;Connection Timeout=10;Application Name=" + AppName);
                sCadena.Replace("<USER>", user);
                sCadena.Replace("<PASS>", pass);
            }
            sCadena.Replace("<SERVER>", server);
            sCadena.Replace("<BASE>", dbName);
            return sCadena.ToString();
        }

        #endregion
    }
}

#endif