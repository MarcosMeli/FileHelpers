using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{


    /// <summary>An internal class used to store information about the Record Type.</summary>
    /// <remarks>Is public to provide extensibility of DataStorage from outside the library.</remarks>
    internal sealed partial class RecordInfo 
    {

       private static class RecordInfoFactory
        {
            private static readonly Dictionary<Type, RecordInfo> mRecordInfoCache = new Dictionary<Type, RecordInfo>();

           /// <summary>
           /// Return the record information for the type
           /// </summary>
           /// <param name="type">Type we want settings for</param>
           /// <remarks>Threadsafe</remarks>
           /// <returns>Record Information (settings and functions)</returns>
            public static IRecordInfo Resolve(Type type)
            {
                RecordInfo res;

                // class check cache / lock / check cache  and create if null algorythm
                if (!mRecordInfoCache.TryGetValue(type, out res))
                {
                    lock (type)
                    {   // Double Check inside a lock
                        if (!mRecordInfoCache.TryGetValue(type, out res))
                        {

                            res = new RecordInfo(type);
                            mRecordInfoCache.Add(type, res);
                        }
                    }

                }
                return (IRecordInfo) res.Clone();
            }
        }

        public void RemoveField(string fieldname)
        {
            var index = GetFieldIndex(fieldname);
            Fields[index] = null;
            Fields = Array.FindAll(Fields, x => x != null);
        }
    }
}