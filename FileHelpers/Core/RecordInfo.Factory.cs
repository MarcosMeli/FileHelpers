using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FileHelpers
{


    /// <summary>An internal class used to store information about the Record Type.</summary>
    /// <remarks>Is public to provide extensibility of DataSorage from outside the library.</remarks>
    internal sealed partial class RecordInfo 
    {

       private static class RecordInfoFactory
        {
            private static readonly Dictionary<Type, RecordInfo> mRecordInfoCache = new Dictionary<Type, RecordInfo>();

            public static IRecordInfo Resolve(Type type)
            {
                RecordInfo res;

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


   }
}