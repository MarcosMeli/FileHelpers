using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace FileHelpers
{
    internal class FieldInfoCacheManipulator
    {
        public static void ResetFieldInfoCache(Type type)
        {

            var cacheProperty = type.GetType().GetProperty("Cache",
                                               BindingFlags.DeclaredOnly |
                                               BindingFlags.Instance |
                                               BindingFlags.NonPublic);

            Debug.Assert(cacheProperty != null, "There is no Cache property in the RuntimeType: " + type.GetType().Name );

            if (cacheProperty != null)
            {
                var cacheObject = cacheProperty.GetValue(type, null);

                Debug.Assert(cacheObject != null, "There is no value for the Cache property in the RuntimeType: " + type.Name);
                var cacheField = cacheObject.GetType().GetField("m_fieldInfoCache",
                                                            BindingFlags.FlattenHierarchy | BindingFlags.Instance |
                                                            BindingFlags.NonPublic);

                Debug.Assert(cacheField != null, "There is no m_fieldInfoCache field for the RuntimeTypeCache: " + type.Name);
                if (cacheField != null)
                    cacheField.SetValue(cacheObject, null);

            }

        }
    }
}