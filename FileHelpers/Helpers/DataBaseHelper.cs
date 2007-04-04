#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

#if ! MINI

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FileHelpers
{
	internal class DataBaseHelper
	{
		#region "  AccessConnection  "

		private const string AccessConnStr = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=<PASSWORD>;Data Source=""<BASE>"";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";

		public static string GetAccessConnection(string db)
		{
			return GetAccessConnection(db, "");
		}

		public static string GetAccessConnection(string db, string password)
		{
            string res = AccessConnStr;
            res = res.Replace("<BASE>", db);

            res = res.Replace("<PASSWORD>", password == null ? string.Empty : password);
            return res;
		}

		#endregion

		#region "  SqlConnectionString  "

		private static string AppName = "FileHelpers"; //<- For display in SqlServer
		

		static public string SqlConnectionString(string server, string dbName)
		{
			return SqlConnectionString(server, dbName, "", "");
		}

		static public string SqlConnectionString(string server, string dbName, string user, string pass)
		{
			StringBuilder sCadena = new StringBuilder(300);
			if (user.Length == 0 && pass.Length == 0)
			{
				sCadena = new StringBuilder("data source=<SERVER>;persist security info=True;" + "initial catalog=<BASE>;integrated security=SSPI;" + "packet size=4096;Connection Timeout=10;Application Name=" + AppName);
			}
			else
			{
				sCadena = new StringBuilder("data source=<SERVER>;persist security info=True;" + "initial catalog=<BASE>;User Id=\"<USER>\";Password=\"<PASS>\";" + "packet size=4096;Connection Timeout=10;Application Name=" + AppName);
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