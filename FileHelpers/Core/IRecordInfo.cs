using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FileHelpers
{
    internal interface IRecordInfo
        : ICloneable
    {
        bool IsDelimited { get; }
        int FieldCount { get; }
        FieldBase[] Fields { get; }
        int IgnoreFirst { get; set; }
        int IgnoreLast { get; set; }
        bool NotifyRead { get; }
        bool NotifyWrite { get; }
        int SizeHint { get; }
        Type RecordType { get; }
        bool IgnoreEmptyLines { get; set; }
        bool IgnoreEmptySpaces { get; }
        string CommentMarker { get; set; }
        bool CommentAnyPlace { get; set; }
        RecordCondition RecordCondition { get; set; }
        Regex RecordConditionRegEx { get; }
        string RecordConditionSelector { get; set; }


        int GetFieldIndex(string fieldName);
        FieldInfo GetFieldInfo(string name);

        RecordOperations Operations { get; }
    }
}