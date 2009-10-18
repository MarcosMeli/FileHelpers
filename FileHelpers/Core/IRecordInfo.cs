using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace FileHelpers
{
    internal delegate object CreateNewObject();

    internal interface IRecordInfo
    {
        bool IsDelimited { get; }
        CreateNewObject CreateRecordObject { get; }
        int FieldCount { get; }
        FieldBase[] Fields { get; }
        int IgnoreFirst { get; set; }
        int IgnoreLast { get; set; }
        bool NotifyRead { get; }
        bool NotifyWrite { get; }
        Type RecordType { get; }
        bool IgnoreEmptyLines { get; set; }
        bool IgnoreEmptySpaces { get; }
        string CommentMarker { get; set; }
        bool CommentAnyPlace { get; set; }
        RecordCondition RecordCondition { get; set; }
        string RecordConditionSelector { get; set; }
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

        DataTable RecordsToDataTable(ICollection records);
        DataTable RecordsToDataTable(ICollection records, int maxRecords);
        DataTable CreateEmptyDataTable();
        int GetFieldIndex(string fieldName);
        FieldInfo GetFieldInfo(string name);
    }
}