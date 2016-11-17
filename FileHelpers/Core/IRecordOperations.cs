using System.Collections;


namespace FileHelpers
{
    internal interface IRecordOperations
    {
        CreateObjectDelegate CreateRecordHandler { get; }

        object StringToRecord(LineInfo line, object[] values);
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

        

        IRecordOperations Clone(RecordInfo ri);
    }
}