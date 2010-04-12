#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
#endregion

//This software is provided free of charge from from Kellerman Software.
//It may be used in any project, including commercial for sale projects.

#region License
//Microsoft Public License (Ms-PL)

//This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

//1. Definitions

//The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.

//A "contribution" is the original software, or any additions or changes to the software.

//A "contributor" is any person that distributes its contribution under this license.

//"Licensed patents" are a contributor's patent claims that read directly on its contribution.

//2. Grant of Rights

//(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

//(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

//3. Conditions and Limitations

//(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.

//(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

//(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

//(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

//(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
#endregion

namespace KellermanSoftware.CompareNetObjects
{
    /// <summary>
    /// Class that allows comparison of two objects of the same type to each other.  Supports classes, lists, arrays, dictionaries, child comparison and more.
    /// </summary>
    public class CompareObjects
    {
        #region Class Variables
        private List<String> _differences = new List<String>();
        private List<object> _parents = new List<object>();
        private List<string> _elementsToIgnore = new List<string>();
        private bool _comparePrivateProperties = false;
        private bool _comparePrivateFields = false;
        private bool _compareChildren = true;
        private bool _compareReadOnly = true;
        private bool _compareFields = true;
        private bool _compareProperties = true;
        private int _maxDifferences = 1;
        #endregion

        #region Properties

        /// <summary>
        /// Ignore classes, properties, or fields by name during the comparison.
        /// Case sensitive.
        /// </summary>
        public List<string> ElementsToIgnore
        {
            get { return _elementsToIgnore; }
            set { _elementsToIgnore = value; }
        }

        /// <summary>
        /// If true, private properties will be compared. The default is false.
        /// </summary>
        public bool ComparePrivateProperties
        {
            get { return _comparePrivateProperties; } 
            set { _comparePrivateProperties = value; }
        }

        /// <summary>
        /// If true, private fields will be compared. The default is false.
        /// </summary>
        public bool ComparePrivateFields
        {
            get { return _comparePrivateFields; }
            set { _comparePrivateFields = value; }
        }

        /// <summary>
        /// If true, child objects will be compared. The default is true. 
        /// If false, and a list or array is compared list items will be compared but not their children.
        /// </summary>
        public bool CompareChildren
        {
            get { return _compareChildren; } 
            set { _compareChildren = value; }
        }

        /// <summary>
        /// If true, compare read only properties (only the getter is implemented).
        /// The default is true.
        /// </summary>
        public bool CompareReadOnly
        {
            get { return _compareReadOnly; }
            set { _compareReadOnly = value; }
        }

        /// <summary>
        /// If true, compare fields of a class (see also CompareProperties).
        /// The default is true.
        /// </summary>
        public bool CompareFields
        {
            get { return _compareFields; }
            set { _compareFields = value; }
        }

        /// <summary>
        /// If true, compare properties of a class (see also CompareFields).
        /// The default is true.
        /// </summary>
        public bool CompareProperties
        {
            get { return _compareProperties; }
            set { _compareProperties = value; }
        }

        /// <summary>
        /// The maximum number of differences to detect
        /// </summary>
        /// <remarks>
        /// Default is 1 for performance reasons.
        /// </remarks>
        public int MaxDifferences
        {
            get { return _maxDifferences; }
            set { _maxDifferences = value; }
        }

        /// <summary>
        /// The differences found during the compare
        /// </summary>
        public List<String> Differences
        {
            get { return _differences; }
            set { _differences = value; }
        }

        /// <summary>
        /// The differences found in a string suitable for a textbox
        /// </summary>
        public string DifferencesString
        {
            get
            {
                StringBuilder sb = new StringBuilder(4096);

                sb.Append("\r\nBegin Differences:\r\n");

                foreach (string item in Differences)
                {
                    sb.AppendFormat("{0}\r\n", item);
                }

                sb.AppendFormat("End Differences (Maximum of {0} differences shown).",MaxDifferences);

                return sb.ToString();
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Compare two objects of the same type to each other.
        /// </summary>
        /// <remarks>
        /// Check the Differences or DifferencesString Properties for the differences.
        /// Default MaxDifferences is 1 for performance
        /// </remarks>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns>True if they are equal</returns>
        public bool Compare(object object1, object object2)
        {
            string defaultBreadCrumb = string.Empty;

            Differences.Clear();
            Compare(object1, object2, defaultBreadCrumb);

            return Differences.Count == 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb">Where we are in the object hiearchy</param>
        private void Compare(object object1, object object2, string breadCrumb)
        {
            //If both null return true
            if (object1 == null && object2 == null)
                return;

            //Check if one of them is null
            if (object1 == null)
            {
                Differences.Add(string.Format("object1{0} == null && object2{0} != null ((null),{1})", breadCrumb,cStr(object2)));
                return;
            }

            if (object2 == null)
            {
                Differences.Add(string.Format("object1{0} != null && object2{0} == null ({1},(null))", breadCrumb, cStr(object1)));
                return;
            }

            Type t1 = object1.GetType();
            Type t2 = object2.GetType();

            //Objects must be the same type
            if (t1 != t2)
            {
                Differences.Add(string.Format("Different Types:  object1{0}.GetType() != object2{0}.GetType()", breadCrumb));
                return;
            }

            if (IsDataset(t1))
            {
                CompareDataset(object1, object2, breadCrumb);
            }
            else if (IsDataTable(t1))
            {
                CompareDataTable(object1, object2, breadCrumb);
            }
            else if (IsDataRow(t1))
            {
                CompareDataRow(object1, object2, breadCrumb);
            }
            else if (IsIList(t1)) //This will do arrays, multi-dimensional arrays and generic lists
            {
                CompareIList(object1, object2, breadCrumb);
            }
            else if (IsIDictionary(t1))
            {
                CompareIDictionary(object1, object2, breadCrumb);
            }
            else if (IsEnum(t1))
            {
                CompareEnum(object1, object2, breadCrumb);
            }
            else if (IsSimpleType(t1))
            {
                CompareSimpleType(object1, object2, breadCrumb);
            }
            else if (IsClass(t1))
            {
                CompareClass(object1, object2, breadCrumb);
            }
            else if (IsTimespan(t1))
            {
                CompareTimespan(object1, object2, breadCrumb);
            }
            else if (IsStruct(t1))
            {
                CompareStruct(object1, object2, breadCrumb);
            }
            else
            {
                throw new NotImplementedException("Cannot compare object of type " + t1.Name);
            }

        }

        private void CompareDataRow(object object1, object object2, string breadCrumb)
        {
            DataRow dataRow1 = object1 as DataRow;
            DataRow dataRow2 = object2 as DataRow;

            if (dataRow1 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object1");

            if (dataRow2 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object2");

            for (int i = 0; i < dataRow1.Table.Columns.Count; i++)
            {
                //If we should ignore it, skip it
                if (ElementsToIgnore.Contains(dataRow1.Table.Columns[i].ColumnName))
                    continue;

                //If we should ignore read only, skip it
                if (!CompareReadOnly && dataRow1.Table.Columns[i].ReadOnly)
                    continue;

                //Both are null
                if (dataRow1.IsNull(i) && dataRow2.IsNull(i))
                    continue; 

                string currentBreadCrumb = AddBreadCrumb(breadCrumb, string.Empty, string.Empty, dataRow1.Table.Columns[i].ColumnName);

                //Check if one of them is null
                if (dataRow1.IsNull(i))
                {
                    Differences.Add(string.Format("object1{0} == null && object2{0} != null ((null),{1})", currentBreadCrumb, cStr(object2)));
                    return;
                }

                if (dataRow2.IsNull(i))
                {
                    Differences.Add(string.Format("object1{0} != null && object2{0} == null ({1},(null))", currentBreadCrumb, cStr(object1)));
                    return;
                }

                Compare(dataRow1[i], dataRow2[i], currentBreadCrumb);

                if (Differences.Count >= MaxDifferences)
                    return;
            }
        }

        private void CompareDataTable(object object1, object object2, string breadCrumb)
        {
            DataTable dataTable1 = object1 as DataTable;
            DataTable dataTable2 = object2 as DataTable;

            if (dataTable1 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object1");

            if (dataTable2 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object2");

            //If we should ignore it, skip it
            if (ElementsToIgnore.Contains(dataTable1.TableName))
                return;

            //There must be the same amount of rows in the datatable
            if (dataTable1.Rows.Count != dataTable2.Rows.Count)
            {
                Differences.Add(string.Format("object1{0}.Rows.Count != object2{0}.Rows.Count ({1},{2})", breadCrumb,
                                              dataTable1.Rows.Count, dataTable2.Rows.Count));

                if (Differences.Count >= MaxDifferences)
                    return;
            }

            //There must be the same amount of columns in the datatable
            if (dataTable1.Columns.Count != dataTable2.Columns.Count)
            {
                Differences.Add(string.Format("object1{0}.Columns.Count != object2{0}.Columns.Count ({1},{2})", breadCrumb,
                                              dataTable1.Columns.Count, dataTable2.Columns.Count));

                if (Differences.Count >= MaxDifferences)
                    return;
            }

            for (int i = 0; i < dataTable1.Rows.Count; i++)
            {
                string currentBreadCrumb = AddBreadCrumb(breadCrumb, "Rows", string.Empty, i);

                CompareDataRow(dataTable1.Rows[i], dataTable2.Rows[i], currentBreadCrumb);

                if (Differences.Count >= MaxDifferences)
                    return;
            }
        }

        private void CompareDataset(object object1, object object2, string breadCrumb)
        {
            DataSet dataSet1 = object1 as DataSet;
            DataSet dataSet2 = object2 as DataSet;

            if (dataSet1 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object1");

            if (dataSet2 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object2");


            //There must be the same amount of tables in the dataset
            if (dataSet1.Tables.Count != dataSet2.Tables.Count)
            {
                Differences.Add(string.Format("object1{0}.Tables.Count != object2{0}.Tables.Count ({1},{2})", breadCrumb,
                                              dataSet1.Tables.Count, dataSet2.Tables.Count));

                if (Differences.Count >= MaxDifferences)
                    return;
            }

            for (int i=0;i<dataSet1.Tables.Count;i++)
            {
                string currentBreadCrumb = AddBreadCrumb(breadCrumb, "Tables", string.Empty, dataSet1.Tables[i].TableName);

                CompareDataTable(dataSet1.Tables[i], dataSet2.Tables[i], currentBreadCrumb);

                if (Differences.Count >= MaxDifferences)
                    return;
            }
        }

        private bool IsTimespan(Type t)
        {
            return t == typeof(TimeSpan);
        }

        private bool IsEnum(Type t)
        {
            return t.IsEnum;
        }

        private bool IsStruct(Type t)
        {
            return t.IsValueType && !IsSimpleType(t);
        }

        private bool IsSimpleType(Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                t = Nullable.GetUnderlyingType(t);
            }

            return t.IsPrimitive
                || t == typeof(DateTime)
                || t == typeof(decimal)
                || t == typeof(string)
                || t == typeof(Guid);

        }

        private bool ValidStructSubType(Type t)
        {
            return IsSimpleType(t)
                || IsEnum(t)
                || IsArray(t)
                || IsClass(t)
                || IsIDictionary(t)
                || IsTimespan(t)
                || IsIList(t);
        }

        private bool IsArray(Type t)
        {
            return t.IsArray;
        }

        private bool IsClass(Type t)
        {
            return t.IsClass;
        }

        private bool IsIDictionary(Type t)
        {
            return t.GetInterface("System.Collections.IDictionary", true) != null;
        }

        private bool IsDataset(Type t)
        {
            return t == typeof(DataSet);
        }

        private bool IsDataRow(Type t)
        {
            return t == typeof(DataRow);
        }

        private bool IsDataTable(Type t)
        {
            return t == typeof(DataTable);
        }

        private bool IsIList(Type t)
        {
            return t.GetInterface("System.Collections.IList", true) != null;
        }

        private bool IsChildType(Type t)
        {
            return !IsSimpleType(t)
                && (IsClass(t)
                    || IsArray(t)
                    || IsIDictionary(t)
                    || IsIList(t)
                    || IsStruct(t));
        }

        /// <summary>
        /// Compare a timespan struct
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void CompareTimespan(object object1, object object2, string breadCrumb)
        {
            if (((TimeSpan)object1).Ticks != ((TimeSpan)object2).Ticks)
            {
                Differences.Add(string.Format("object1{0}.Ticks != object2{0}.Ticks", breadCrumb));
            }
        }

        /// <summary>
        /// Compare an enumeration
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void CompareEnum(object object1, object object2, string breadCrumb)
        {
            if (object1.ToString() != object2.ToString())
            {
                string currentBreadCrumb = AddBreadCrumb(breadCrumb, object1.GetType().Name, string.Empty, -1);
                Differences.Add(string.Format("object1{0} != object2{0} ({1},{2})", currentBreadCrumb, object1, object2));
            }
        }

        /// <summary>
        /// Compare a simple type
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void CompareSimpleType(object object1, object object2, string breadCrumb)
        {
            if (object2 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object2");

            IComparable valOne = object1 as IComparable;

            if (valOne == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object1");

            if (valOne.CompareTo(object2) != 0)
            {
                Differences.Add(string.Format("object1{0} != object2{0} ({1},{2})", breadCrumb, object1, object2));
            }
        }



        /// <summary>
        /// Compare a struct
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void CompareStruct(object object1, object object2, string breadCrumb)
        {
            string currentCrumb;
            Type t1 = object1.GetType();

            //Compare the fields
            FieldInfo[] currentFields = t1.GetFields();

            foreach (FieldInfo item in currentFields)
            {
                //Only compare simple types within structs (Recursion Problems)
                if (!ValidStructSubType(item.FieldType))
                {
                    continue;
                }

                currentCrumb = AddBreadCrumb(breadCrumb, item.Name, string.Empty, -1);

                Compare(item.GetValue(object1), item.GetValue(object2), currentCrumb);

                if (Differences.Count >= MaxDifferences)
                    return;
            }

        }

        /// <summary>
        /// Compare the properties, fields of a class
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void CompareClass(object object1, object object2, string breadCrumb)
        {
            try
            {
                _parents.Add(object1);
                _parents.Add(object2);
                Type t1 = object1.GetType();

                //We ignore the class name
                if (ElementsToIgnore.Contains(t1.Name))
                    return;

                //Compare the properties
                if (CompareProperties)
                    PerformCompareProperties(t1, object1, object2, breadCrumb);

                //Compare the fields
                if (CompareFields)
                    PerformCompareFields(t1, object1, object2, breadCrumb);
            }
            finally
            {
                _parents.Remove(object1);
                _parents.Remove(object2);
            }
        }

        /// <summary>
        /// Compare the fields of a class
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void PerformCompareFields(Type t1,
            object object1,
            object object2,
            string breadCrumb)
        {
            object objectValue1;
            object objectValue2;
            string currentCrumb;

            FieldInfo[] currentFields;
            
            if (ComparePrivateFields)
                currentFields= t1.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            else
                currentFields= t1.GetFields(); //Default is public instance
                        
            foreach (FieldInfo item in currentFields)
            {
                //Skip if this is a shallow compare
                if (!CompareChildren && IsChildType(item.FieldType))
                    continue;

                //If we should ignore it, skip it
                if (ElementsToIgnore.Contains(item.Name))
                    continue;

                objectValue1 = item.GetValue(object1);
                objectValue2 = item.GetValue(object2);

                bool object1IsParent = objectValue1 != null && (objectValue1 == object1 || _parents.Contains(objectValue1));
                bool object2IsParent = objectValue2 != null && (objectValue2 == object2 || _parents.Contains(objectValue2));

                //Skip fields that point to the parent
                if (IsClass(item.FieldType)
                    && (object1IsParent || object2IsParent))
                {
                    continue;
                }

                currentCrumb = AddBreadCrumb(breadCrumb, item.Name, string.Empty, -1);

                Compare(objectValue1, objectValue2, currentCrumb);

                if (Differences.Count >= MaxDifferences)
                    return;
            }
        }


        /// <summary>
        /// Compare the properties of a class
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void PerformCompareProperties(Type t1, 
            object object1,
            object object2,
            string breadCrumb)
        {
            object objectValue1;
            object objectValue2;
            string currentCrumb;

            PropertyInfo[] currentProperties;

            if (ComparePrivateProperties)
                currentProperties = t1.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            else
                currentProperties =  t1.GetProperties(); //Default is public instance            

            foreach (PropertyInfo info in currentProperties)
            {
                //If we can't read it, skip it
                if (info.CanRead == false)
                    continue;

                //Skip if this is a shallow compare
                if (!CompareChildren && IsChildType(info.PropertyType))
                    continue;

                //If we should ignore it, skip it
                if (ElementsToIgnore.Contains(info.Name))
                    continue;

                //If we should ignore read only, skip it
                if (!CompareReadOnly && info.CanWrite == false)
                    continue;

                if (!IsValidIndexer(info,object1,object2,breadCrumb))
                {
                    objectValue1 = info.GetValue(object1, null);
                    objectValue2 = info.GetValue(object2, null);                    
                }
                else
                {
                    CompareIndexer(info,object1, object2,breadCrumb);
                    continue; ;
                }

                bool object1IsParent = objectValue1 != null && (objectValue1 == object1 || _parents.Contains(objectValue1));
                bool object2IsParent = objectValue2 != null && (objectValue2 == object2 || _parents.Contains(objectValue2));

                //Skip properties where both point to the corresponding parent
                if (IsClass(info.PropertyType)
                    && (object1IsParent && object2IsParent))
                {
                    continue;
                }

                currentCrumb = AddBreadCrumb(breadCrumb, info.Name, string.Empty, -1);

                Compare(objectValue1, objectValue2, currentCrumb);

                if (Differences.Count >= MaxDifferences)
                    return;
            }
        }

        private bool IsValidIndexer(PropertyInfo info, object object1, object object2, string breadCrumb)
        {
            ParameterInfo[] indexers = info.GetIndexParameters();

            if (indexers.Length == 0)
            {
                return false;
            }
            else if (indexers.Length > 1)
            {
                throw new Exception("Cannot compare objects with more than one indexer for object " + breadCrumb);
            }
            else if (indexers[0].ParameterType != typeof(Int32))
            {
                throw new Exception("Cannot compare objects with a non integer indexer for object " + breadCrumb);
            }
            else if (info.ReflectedType.GetProperty("Count") == null)
            {
                throw new Exception("Indexer must have a corresponding Count property for object " + breadCrumb);
            }
            else if (info.ReflectedType.GetProperty("Count").PropertyType != typeof(Int32))
            {
                throw new Exception("Indexer must have a corresponding Count property that is an integer for object " + breadCrumb);
            }

            return true;
        }
        private void CompareIndexer(PropertyInfo info, object object1, object object2, string breadCrumb)
        {
            string currentCrumb;
            int indexerCount1 = (int)info.ReflectedType.GetProperty("Count").GetGetMethod().Invoke(object1, new object[] { });
            int indexerCount2 = (int)info.ReflectedType.GetProperty("Count").GetGetMethod().Invoke(object2, new object[] { });

            //Indexers must be the same length
            if (indexerCount1 != indexerCount2)
            {
                currentCrumb = AddBreadCrumb(breadCrumb, info.Name, string.Empty, -1);
                Differences.Add(string.Format("object1{0}.Count != object2{0}.Count ({1},{2})", currentCrumb,
                                              indexerCount1, indexerCount2));

                if (Differences.Count >= MaxDifferences)
                    return;
            }

            // Run on indexer
            for (int i = 0; i < indexerCount1; i++)
            {
                currentCrumb = AddBreadCrumb(breadCrumb, info.Name, string.Empty, i);
                object objectValue1 = info.GetValue(object1, new object[] { i });
                object objectValue2 = info.GetValue(object2, new object[] { i });
                Compare(objectValue1, objectValue2, currentCrumb);

                if (Differences.Count >= MaxDifferences)
                    return;
            }
        }

        /// <summary>
        /// Compare a dictionary
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void CompareIDictionary(object object1, object object2, string breadCrumb)
        {
            IDictionary iDict1 = object1 as IDictionary;
            IDictionary iDict2 = object2 as IDictionary;

            if (iDict1 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object1");

            if (iDict2 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object2");

            try
            {
                _parents.Add(object1);
                _parents.Add(object2);

                //Objects must be the same length
                if (iDict1.Count != iDict2.Count)
                {
                    Differences.Add(string.Format("object1{0}.Count != object2{0}.Count ({1},{2})", breadCrumb,
                                                  iDict1.Count, iDict2.Count));

                    if (Differences.Count >= MaxDifferences)
                        return;
                }

                IDictionaryEnumerator enumerator1 = iDict1.GetEnumerator();
                IDictionaryEnumerator enumerator2 = iDict2.GetEnumerator();

                while (enumerator1.MoveNext() && enumerator2.MoveNext())
                {
                    string currentBreadCrumb = AddBreadCrumb(breadCrumb, "Key", string.Empty, -1);

                    Compare(enumerator1.Key, enumerator2.Key, currentBreadCrumb);

                    if (Differences.Count >= MaxDifferences)
                        return;

                    currentBreadCrumb = AddBreadCrumb(breadCrumb, "Value", string.Empty, -1);

                    Compare(enumerator1.Value, enumerator2.Value, currentBreadCrumb);

                    if (Differences.Count >= MaxDifferences)
                        return;
                }
            }
            finally
            {
                _parents.Remove(object1);
                _parents.Remove(object2);
            }
        }

        /// <summary>
        /// Convert an object to a nicely formatted string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string cStr(object obj)
        {
            try
            {                
                if (obj == null)
                    return "(null)";

                if (obj == DBNull.Value)
                    return "System.DBNull.Value";
                
                return obj.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// Compare an array or something that implements IList
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <param name="breadCrumb"></param>
        private void CompareIList(object object1, object object2, string breadCrumb)
        {
            IList ilist1 = object1 as IList;
            IList ilist2 = object2 as IList;

            if (ilist1 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object1");

            if (ilist2 == null) //This should never happen, null check happens one level up
                throw new ArgumentNullException("object2");

            try
            {
                _parents.Add(object1);
                _parents.Add(object2);

                //Objects must be the same length
                if (ilist1.Count != ilist2.Count)
                {
                    Differences.Add(string.Format("object1{0}.Count != object2{0}.Count ({1},{2})", breadCrumb,
                                                  ilist1.Count, ilist2.Count));

                    if (Differences.Count >= MaxDifferences)
                        return;
                }

                IEnumerator enumerator1 = ilist1.GetEnumerator();
                IEnumerator enumerator2 = ilist2.GetEnumerator();
                int count = 0;

                while (enumerator1.MoveNext() && enumerator2.MoveNext())
                {
                    string currentBreadCrumb = AddBreadCrumb(breadCrumb, string.Empty, string.Empty, count);

                    Compare(enumerator1.Current, enumerator2.Current, currentBreadCrumb);

                    if (Differences.Count >= MaxDifferences)
                        return;

                    count++;
                }
            }
            finally
            {
                _parents.Remove(object1);
                _parents.Remove(object2); 
            }
        }



        /// <summary>
        /// Add a breadcrumb to an existing breadcrumb
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="name"></param>
        /// <param name="extra"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string AddBreadCrumb(string existing, string name, string extra, string index)
        {
            bool useIndex = !String.IsNullOrEmpty(index);
            bool useName= name.Length > 0;
            StringBuilder sb = new StringBuilder();

            sb.Append(existing);

            if (useName)
            {
                sb.AppendFormat(".");
                sb.Append(name);
            }

            sb.Append(extra);

            if (useIndex)
            {
                int result=-1;
                if (Int32.TryParse(index, out result))
                {
                    sb.AppendFormat("[{0}]", index);
                }
                else
                {
                    sb.AppendFormat("[\"{0}\"]", index);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Add a breadcrumb to an existing breadcrumb
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="name"></param>
        /// <param name="extra"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string AddBreadCrumb(string existing, string name, string extra, int index)
        {
            return AddBreadCrumb(existing, name, extra, index >= 0 ? index.ToString() : null);
        }

        #endregion

    }
}
