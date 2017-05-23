using System;
using System.Collections.Generic;
using System.Linq;

namespace FileHelpers
{
    /// <summary>An internal class used to store information about the Record Type.</summary>
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
                lock (type) {
                    RecordInfo res;
                    lock (mRecordInfoCache) {
                        if (mRecordInfoCache.TryGetValue(type, out res))
                            return res.Clone();
                    }

                    // class check cache / lock / check cache  and create if null algorythm
                    res = new RecordInfo(type);
                    lock (mRecordInfoCache) {
                        if (!mRecordInfoCache.ContainsKey(type))
                            mRecordInfoCache.Add(type, res);
                    }

                    return res.Clone();
                }
            }
        }

        public void RemoveField(string fieldname)
        {
            var index = Fields.ToList().IndexOf(Fields.Single(f => f.FieldFriendlyName == fieldname));
            Fields[index] = null;
            Fields = Array.FindAll(Fields, x => x != null);

            AdjustParentIndex();
        }

        private void AdjustParentIndex()
        {
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i].ParentIndex = i;
            }
        }
    }
}