using System;
using System.Diagnostics;
using System.Reflection;

namespace FileHelpers
{
    internal class FieldInfoCacheManipulator
    {
        private static PropertyInfo mCacheProperty;

        /// <summary>
        /// Very importat to avoid out of order reflection
        /// The CLR caches previous fields access to speed up reflection but can return the fields in wrong order
        /// Clearing the m_fieldInfoCache of the Cache property resolves the issue
        /// </summary>
        /// <param name="type">Type of Object</param>
        public static void ResetFieldInfoCache(Type type)
        {
            if (mCacheProperty == null) {
                mCacheProperty = type.GetType().GetProperty("Cache",
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);
            }
            Debug.Assert(mCacheProperty != null, "There is no Cache property in the RuntimeType: " + type.GetType().Name);

            if (mCacheProperty != null) {
                var cacheObject = mCacheProperty.GetValue(type, null);

                Debug.Assert(cacheObject != null,
                    "There is no value for the Cache property in the RuntimeType: " + type.Name);
                var cacheField = cacheObject.GetType().GetField("m_fieldInfoCache",
                    BindingFlags.FlattenHierarchy | BindingFlags.Instance |
                    BindingFlags.NonPublic);

                Debug.Assert(cacheField != null,
                    "There is no m_fieldInfoCache field for the RuntimeTypeCache: " + type.Name);
                if (cacheField != null)
                    cacheField.SetValue(cacheObject, null);
            }
        }
    }
}