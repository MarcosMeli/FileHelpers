using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace FileHelpers.Options
{
    /// <summary>
    /// This class allows you to set some options of the records at runtime.
    /// With these options the library is now more flexible than ever.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public abstract class RecordOptions
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IRecordInfo mRecordInfo;


        /// <summary>
        /// This class allows you to set some options of the records at runtime.
        /// With these options the library is now more flexible than ever.
        /// </summary>
        /// <param name="info">Record information</param>
        internal RecordOptions(IRecordInfo info)
        {
            mRecordInfo = info;
            mRecordConditionInfo = new RecordConditionInfo(info);
            mIgnoreCommentInfo = new IgnoreCommentInfo(info);
        }


        public FieldBaseCollection Fields
        {
            get { return new FieldBaseCollection(mRecordInfo.Fields); }
        }

        public void RemoveField(string fieldname)
        {
            mRecordInfo.RemoveField(fieldname);
            //for (int i = 0; i < Fields.Count; i++)
            //{
            //    var field = Fields[i];
            //    field.ParentIndex = i;
            //}
        }

        /// <summary>
        /// The number of fields of the record type.
        /// </summary>
        public int FieldCount
        {
            get { return mRecordInfo.FieldCount; }
        }

        // <summary>The number of fields of the record type.</summary>
        //[System.Runtime.CompilerServices.IndexerName("FieldNames")]
        //public string this[int index]
        //{
        //    get
        //    {
        //        return mRecordInfo.mFields[index].mFieldInfo.Name;
        //    }
        //}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] mFieldNames;

        /// <summary>
        /// Returns an string array with the fields names.
        /// Note : Do NOT change the values of the array, clone it first if needed
        /// </summary>
        /// <returns>An string array with the fields names.</returns>
        public string[] FieldsNames
        {
            get
            {
                if (mFieldNames == null) {
                    mFieldNames = new string[mRecordInfo.FieldCount];
                    for (int i = 0; i < mFieldNames.Length; i++)
                        mFieldNames[i] = mRecordInfo.Fields[i].FieldFriendlyName;
                }

                return mFieldNames;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Type[] mFieldTypes;

        /// <summary>
        /// Returns a Type[] array with the fields types.
        /// Note : Do NOT change the values of the array, clone it first if
        /// needed
        /// </summary>
        /// <returns>An Type[] array with the fields types.</returns>
        public Type[] FieldsTypes
        {
            get
            {
                if (mFieldTypes == null) {
                    mFieldTypes = new Type[mRecordInfo.FieldCount];
                    for (int i = 0; i < mFieldTypes.Length; i++)
                        mFieldTypes[i] = mRecordInfo.Fields[i].FieldInfo.FieldType;
                }

                return mFieldTypes;
            }
        }

//        /// <summary>Returns the type of the field at the specified index</summary>
//        /// <returns>The type of the field.</returns>
//        /// <param name="index">The index of the field</param>
//        public Type GetFieldType(int index)
//        {
//            return mRecordInfo.mFields[index].mFieldInfo.FieldType;
//        }


        /// <summary>
        /// Indicates the number of first lines to be discarded.
        /// </summary>
        public int IgnoreFirstLines
        {
            get { return mRecordInfo.IgnoreFirst; }
            set
            {
                ExHelper.PositiveValue(value);
                mRecordInfo.IgnoreFirst = value;
            }
        }

        /// <summary>
        /// Indicates the number of lines at the end of file to be discarded.
        /// </summary>
        public int IgnoreLastLines
        {
            get { return mRecordInfo.IgnoreLast; }
            set
            {
                ExHelper.PositiveValue(value);
                mRecordInfo.IgnoreLast = value;
            }
        }

        /// <summary>
        /// Indicates that the engine must ignore the empty lines while
        /// reading.
        /// </summary>
        public bool IgnoreEmptyLines
        {
            get { return mRecordInfo.IgnoreEmptyLines; }
            set { mRecordInfo.IgnoreEmptyLines = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RecordConditionInfo mRecordConditionInfo;

        /// <summary>
        /// Used to tell the engine which records must be included or excluded
        /// while reading.
        /// </summary>
        public RecordConditionInfo RecordCondition
        {
            get { return mRecordConditionInfo; }
        }


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IgnoreCommentInfo mIgnoreCommentInfo;

        /// <summary>
        /// Indicates that the engine must ignore the lines with this comment
        /// marker.
        /// </summary>
        public IgnoreCommentInfo IgnoreCommentedLines
        {
            get { return mIgnoreCommentInfo; }
        }

        /// <summary>
        /// Used to tell the engine which records must be included or excluded
        /// while reading.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public sealed class RecordConditionInfo
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly IRecordInfo mRecordInfo;

            /// <summary>
            /// Used to tell the engine which records must be included or
            /// excluded while reading.
            /// </summary>
            /// <param name="ri">Record information</param>
            internal RecordConditionInfo(IRecordInfo ri)
            {
                mRecordInfo = ri;
            }

            /// <summary>
            /// The condition used to include or exclude records.
            /// </summary>
            public RecordCondition Condition
            {
                get { return mRecordInfo.RecordCondition; }
                set { mRecordInfo.RecordCondition = value; }
            }

            /// <summary>
            /// The selector used by the <see cref="RecordCondition"/>.
            /// </summary>
            public string Selector
            {
                get { return mRecordInfo.RecordConditionSelector; }
                set { mRecordInfo.RecordConditionSelector = value; }
            }
        }

        /// <summary>
        /// Indicates that the engine must ignore the lines with this comment
        /// marker.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public sealed class IgnoreCommentInfo
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly IRecordInfo mRecordInfo;

            /// <summary>
            /// Indicates that the engine must ignore the lines with this
            /// comment marker.
            /// </summary>
            /// <param name="ri">Record information</param>
            internal IgnoreCommentInfo(IRecordInfo ri)
            {
                mRecordInfo = ri;
            }

            /// <summary>
            /// <para>Indicates that the engine must ignore the lines with this
            /// comment marker.</para>
            /// <para>An empty string or null indicates that the engine doesn't
            /// look for comments</para>
            /// </summary>
            public string CommentMarker
            {
                get { return mRecordInfo.CommentMarker; }
                set
                {
                    if (value != null)
                        value = value.Trim();
                    mRecordInfo.CommentMarker = value;
                }
            }

            /// <summary>
            /// Indicates if the comment can have spaces or tabs at left (true
            /// by default)
            /// </summary>
            public bool InAnyPlace
            {
                get { return mRecordInfo.CommentAnyPlace; }
                set { mRecordInfo.CommentAnyPlace = value; }
            }
        }


        /// <summary>
        /// Allows the creating of a record string of the given record. Is
        /// useful when your want to log errors to a plan text file or database
        /// </summary>
        /// <param name="record">
        /// The record that will be transformed to string
        /// </param>
        /// <returns>The string representation of the current record</returns>
        public string RecordToString(object record)
        {
            return mRecordInfo.Operations.RecordToString(record);
        }

        /// <summary>
        /// Allows to get an object[] with the values of the fields in the <param name="record"></param>
        /// </summary>
        /// <param name="record">The record that will be transformed to object[]</param>
        /// <returns>The object[] with the values of the fields in the current record</returns>
        public object[] RecordToValues(object record)
        {
            return mRecordInfo.Operations.RecordToValues(record);
        }
    }

    public sealed class FieldBaseCollection
        : List<FieldBase>
    {
        internal FieldBaseCollection(FieldBase[] fields)
            : base(fields) {}

        /// <summary>
        /// Finds a field by name.
        /// </summary>
        /// <param name="FieldName">The name of the field to find.</param>
        /// <returns>The FieldBase item if found - otherwise, null.</returns>
        public FieldBase this[string FieldName]
        {
            get { return this.Find(x => x.FieldName == FieldName); }
        }
    }
}