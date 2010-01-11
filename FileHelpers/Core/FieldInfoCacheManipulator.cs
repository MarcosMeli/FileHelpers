using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Singleton = FileHelpers.Container.SingletonAttribute;

namespace FileHelpers
{
    [Singleton]
    internal class FieldInfoCacheManipulator : IFieldInfoCacheManipulator
    {
        private struct FieldInfoCache
        {
            private readonly FieldInfo mCacheField;
            private readonly object mCacheObject;

            public FieldInfoCache(Type type)
            {
                var CacheProperty = type.GetType().GetProperty("Cache",
                                                               BindingFlags.DeclaredOnly | BindingFlags.Instance |
                                                               BindingFlags.NonPublic);

                if (CacheProperty != null)
                {
                    mCacheObject = CacheProperty.GetValue(type, null);

                    mCacheField = mCacheObject.GetType().GetField("m_fieldInfoCache",
                                                                BindingFlags.FlattenHierarchy | BindingFlags.Instance |
                                                                BindingFlags.NonPublic);
                }
                else
                {
                    mCacheObject = null;
                    mCacheField = null;
                }
            }

            public void Reset()
            {
                lock (mCacheObject)
                {
                    mCacheField.SetValue(mCacheObject, null);
                }
            }
        }

        private static readonly Dictionary<Type, FieldInfoCache> cacheCache = new Dictionary<Type, FieldInfoCache>();
        private readonly ReaderWriterLock cacheCacheLock = new ReaderWriterLock();

        public void ResetFieldInfoCache(Type type)
        {
            cacheCacheLock.AcquireReaderLock(Timeout.Infinite);

            FieldInfoCache cache;
            if (!cacheCache.TryGetValue(type, out cache))
            {
                cacheCacheLock.UpgradeToWriterLock(Timeout.Infinite);
                cache = new FieldInfoCache(type);
                cacheCache.Add(type, cache);
            }
            cacheCacheLock.ReleaseLock();
            
            cache.Reset();
        }
    }
}