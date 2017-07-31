using System.Data;

namespace FileHelpers
{
    /// <summary>
    /// Set of Extension methods to be exposed to end users 
    /// of the FileHelpers API.
    /// </summary>
    public static class ExtensionsFileHelpers
    {
        /// <summary>
        /// Generic extension method for arrays that returns the array records
        /// in a DataTable.
        /// </summary>
        /// <param name="records">The array to transform into a DataTable</param>
        /// <returns>The array records in a DataTable.</returns>
        public static DataTable ToDataTable<T>(this T[] records)
        {
            var ri = RecordInfo.Resolve(typeof(T));
            return ri.Operations.RecordsToDataTable(records);
        }
    }
}
