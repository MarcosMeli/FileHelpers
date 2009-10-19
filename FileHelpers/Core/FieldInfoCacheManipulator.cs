using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Singleton = EmbeddedIoC.Container.SingletonAttribute;

namespace FileHelpers
{
    [Singleton]
    internal class FieldInfoCacheManipulator : IFieldInfoCacheManipulator
    {
        private struct FieldInfoCache
        {
            private readonly FieldInfo CacheField;
            private readonly object CacheObject;

            public FieldInfoCache(Type type)
            {
                var CacheProperty = type.GetType().GetProperty("Cache",
                                                               BindingFlags.DeclaredOnly | BindingFlags.Instance |
                                                               BindingFlags.NonPublic);

                if (CacheProperty != null)
                {
                    CacheObject = CacheProperty.GetValue(type, null);

                    CacheField = CacheObject.GetType().GetField("m_fieldInfoCache",
                                                                BindingFlags.FlattenHierarchy | BindingFlags.Instance |
                                                                BindingFlags.NonPublic);
                }
                else
                {
                    CacheObject = null;
                    CacheField = null;
                }
            }

            public void Reset()
            {
                lock (CacheObject)
                {
                    CacheField.SetValue(CacheObject, null);
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