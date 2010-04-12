using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FileHelpers
{
    internal delegate object ValuesToObjectDelegate(object[] values);
    internal delegate object[] ObjectToValuesDelegate(object record);
    internal delegate object CreateObjectDelegate();

    internal static class ReflectionHelper
    {
        public static ObjectToValuesDelegate ObjectToValuesMethod(Type recordType, FieldInfo[] fields)
        {
            var dm = new DynamicMethod("_GetAllValues_FH_RT_",
                                       MethodAttributes.Static | MethodAttributes.Public,
                                       CallingConventions.Standard,
                                       typeof(object[]),
                                       new[] { typeof(object) },
                                       recordType,
                                       true);

            ILGenerator generator = dm.GetILGenerator();

            generator.DeclareLocal(typeof(object[]));
            generator.DeclareLocal(recordType);

            generator.Emit(OpCodes.Ldc_I4, fields.Length);
            generator.Emit(OpCodes.Newarr, typeof(object));
            generator.Emit(OpCodes.Stloc_0);

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, recordType);
            generator.Emit(OpCodes.Stloc_1);

            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldloc_1);

                generator.Emit(OpCodes.Ldfld, field);

                if (field.FieldType.IsValueType)
                {
                    generator.Emit(OpCodes.Box, field.FieldType);
                }

                generator.Emit(OpCodes.Stelem_Ref);
            }

            // return the value
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            return (ObjectToValuesDelegate)dm.CreateDelegate(typeof(ObjectToValuesDelegate));
        }

        public static ConstructorInfo GetDefaultConstructor(Type recordType)
        {
            return recordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                                        null, Type.EmptyTypes,
                                        new ParameterModifier[] { });
        }

        public static ValuesToObjectDelegate ValuesToObjectMethod(Type recordType, FieldInfo[] fields)
        {
            var dm = new DynamicMethod("_CreateAndAssing_FH_RT_",
                                       MethodAttributes.Static | MethodAttributes.Public,
                                       CallingConventions.Standard,
                                       typeof(object),
                                       new[] { typeof(object[]) },
                                       recordType,
                                       true);

            ILGenerator generator = dm.GetILGenerator();

            generator.DeclareLocal(recordType);
            generator.Emit(OpCodes.Newobj, GetDefaultConstructor(recordType));
            generator.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldelem_Ref);

                if (field.FieldType.IsValueType)
                {
                    generator.Emit(OpCodes.Unbox_Any, field.FieldType);
                }
                else
                {
                    generator.Emit(OpCodes.Castclass, field.FieldType);
                }

                generator.Emit(OpCodes.Stfld, field);
            }

            // return the value
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            return (ValuesToObjectDelegate)dm.CreateDelegate(typeof(ValuesToObjectDelegate));
        }

        public static CreateObjectDelegate CreateFastConstructor(Type recordType)
        {
            var dm = new DynamicMethod("_CreateRecordFast_FH_RT_",
                                       MethodAttributes.Static | MethodAttributes.Public,
                                       CallingConventions.Standard,
                                       typeof(object),
                                       new[] { typeof(object[]) },
                                       recordType,
                                       true);

            ILGenerator generator = dm.GetILGenerator();

            generator.Emit(OpCodes.Newobj, GetDefaultConstructor(recordType));
            generator.Emit(OpCodes.Ret);

            return (CreateObjectDelegate)dm.CreateDelegate(typeof(CreateObjectDelegate));
        }

        public static GetFieldValueCallback CreateGetFieldMethod(FieldInfo fi)
        {
            var dm = new DynamicMethod("_GetValue" + fi.Name + "_FH_RT_",
                                       MethodAttributes.Static | MethodAttributes.Public,
                                       CallingConventions.Standard,
                                       typeof(object),
                                       new[] { typeof(object) },
                                       fi.DeclaringType,
                                       true);

            ILGenerator generator = dm.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, fi.DeclaringType);
            generator.Emit(OpCodes.Ldfld, fi);
            generator.Emit(OpCodes.Ret);

            return (GetFieldValueCallback)dm.CreateDelegate(typeof(GetFieldValueCallback));
        }


        public static IEnumerable<FieldInfo> RecursiveGetFields(Type currentType)
        {
            if (currentType.BaseType != null && !currentType.IsDefined(typeof(IgnoreInheritedClassAttribute), false))
                foreach (FieldInfo item in RecursiveGetFields(currentType.BaseType)) yield return item;

            if (currentType == typeof(object))
                yield break;

            FieldInfoCacheManipulator.ResetFieldInfoCache(currentType);

            //var temp = currentType.GetMembers(BindingFlags.Public |
            //                                  BindingFlags.NonPublic |
            //                                  BindingFlags.Instance |
            //                                  BindingFlags.GetField |
            //                                  BindingFlags.GetProperty |
            //                                  BindingFlags.DeclaredOnly);

            //Debug.WriteLine(temp.Length);

            //Array.Sort(temp, (x, y) => x.MetadataToken.CompareTo(y.MetadataToken));

            //Debug.WriteLine(temp.Length);

            foreach (FieldInfo fi in currentType.GetFields(BindingFlags.Public |
                                                           BindingFlags.NonPublic |
                                                           BindingFlags.Instance |
                                                           BindingFlags.DeclaredOnly))
            {
                if (!(typeof(Delegate)).IsAssignableFrom(fi.FieldType))
                    yield return fi;
            }
        }

    }
}