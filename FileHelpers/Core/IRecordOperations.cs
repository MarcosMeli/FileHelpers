using System.Collections;
using System.Data;

namespace FileHelpers
{
    internal interface IRecordOperations
    {
        CreateObjectDelegate CreateRecordHandler { get; }

        T StringToRecord<T>(LineInfo line, object[] values) 
            where T : class;

        string RecordToString(object record);
        string RecordValuesToString(object[] recordValues);

        /// <summary>Returns a record formed with the passed values.</summary>
        /// <param name="values">The source Values.</param>
        /// <returns>A record formed with the passed values.</returns>
        object ValuesToRecord(object[] values);

        /// <summary>Get an object[] of the values in the fields of the passed record.</summary>
        /// <param name="record">The source record.</param>
        /// <returns>An object[] of the values in the fields.</returns>
        object[] RecordToValues(object record);

        DataTable RecordsToDataTable(ICollection records);
        DataTable RecordsToDataTable(ICollection records, int maxRecords);
        DataTable CreateEmptyDataTable();

    }
}