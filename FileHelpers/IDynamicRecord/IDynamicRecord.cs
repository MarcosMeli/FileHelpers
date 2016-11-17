using System;


namespace FileHelpers
{
    public interface IDynamicRecord
        :IDataRecord
    {
        object GetRecordValue(int i);
        object[] GetRecordValues();
        void SetRecordValue(int i, object value);

        void SetValue(int i, object value);
        new object this[int i]
        {
            get;
            set;
        }

        new object this[string name]
        {
            get;
            set;
        }

    }


    internal interface IDynamicRecordResolver
    {

        string GetDataTypeName(int i);

        Type GetFieldType(int i);
        int GetFieldCount();

        string GetName(int i);

        int GetOrdinal(string name);
    }

    internal abstract class DynamicRecordBase
        : IDynamicRecord
    {
        internal DynamicRecordBase(IDynamicRecordResolver resolver)
        {
            Resolver = resolver;
        }

        
        private IDynamicRecordResolver Resolver;

        #region IDataRecord Members

        public int FieldCount
        {
            get { return Resolver.GetFieldCount(); }
        }

        public bool GetBoolean(int i)
        {
            return (bool) GetValue(i);
        }

        public byte GetByte(int i)
        {
            return (byte)GetValue(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            return (char) GetValue(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            return Resolver.GetDataTypeName(i);
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)GetValue(i);
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)GetValue(i);
        }

        public double GetDouble(int i)
        {
            return (double)GetValue(i);
        }

        public Type GetFieldType(int i)
        {
            return Resolver.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            return (byte)GetValue(i);
        }

        public Guid GetGuid(int i)
        {
            return (Guid)GetValue(i);
        }

        public short GetInt16(int i)
        {
            return (Int16)GetValue(i);
        }

        public int GetInt32(int i)
        {
            return (Int32)GetValue(i);
        }

        public long GetInt64(int i)
        {
            return (Int64)GetValue(i);
        }

        public string GetName(int i)
        {
            return Resolver.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return Resolver.GetOrdinal(name);
        }

        public string GetString(int i)
        {
            return (string) GetValue(i);
        }

        public object GetValue(int i)
        {
            return this.GetRecordValue(i);
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { return GetValue(GetOrdinal(name)); }
            set { SetValue(GetOrdinal(name), value); }
        }

        public object this[int i]
        {
            get { return GetValue(i); }
            set { SetValue(i, value); }
        }

        #endregion


        public void SetValue(int i, object value)
        {
            throw new NotImplementedException();
        }



        #region IDynamicRecord Members

        public abstract object GetRecordValue(int i);

        public abstract object[] GetRecordValues();

        public abstract void SetRecordValue(int i, object value);

        #endregion
    }

    internal class DynamicRecordArray
    : DynamicRecordBase
    {
        private object[] mValues;

        public DynamicRecordArray(IDynamicRecordResolver resolver)
            : this(resolver, new object[resolver.GetFieldCount()])
        { }

        public DynamicRecordArray(IDynamicRecordResolver resolver, object[] values)
            :base(resolver)
        {
            mValues = values;
        }

        public override object GetRecordValue(int i)
        {
            return mValues[i];
        }

        public override object[] GetRecordValues()
        {
            return mValues;
        }

        public override void SetRecordValue(int i, object value)
        {
            mValues[i] = value;
        }
    }
    

}