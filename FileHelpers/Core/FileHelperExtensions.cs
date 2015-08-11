using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// Set of Extension methods to be exposed to end users 
    /// of the FileHelper API.
    /// </summary>
    public static class FileHelperExtensions
    {
        /// <summary>
        /// Generic extension method for arrays that returns the array records
        /// in a DataTable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records">The array to transform into a DataTable</param>
        /// <returns>The array records in a DataTable.</returns>
        public static DataTable ToDataTable<T>(this T[] records)
        {
            var ri = RecordInfo.Resolve(typeof(T));

            return ri.Operations.RecordsToDataTable(records);
        }
    }
}


namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// This is needed per http://stackoverflow.com/questions/1522605/using-extension-methods-in-net-2-0
    /// This is needed because of the target framework of the project being .NET 2.0. 
    /// Remove this is the target framework is changed to a higher .NET version.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute { }
}

