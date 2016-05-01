using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FileHelpers
{
    /// <summary>
    /// Create an object and assign
    /// </summary>
    /// <param name="values">Values that are assigned to instance</param>
    /// <returns>Instance of object initialised</returns>
    internal delegate object CreateAndAssignDelegate(object[] values);

    /// <summary>
    /// Assign values to a known object
    /// </summary>
    /// <param name="record">Record to assign</param>
    /// <param name="values">Values to assign to the record</param>
    internal delegate void AssignDelegate(object record, object[] values);

    /// <summary>
    /// Extract the values from the object (function created based on FieldInfo
    /// </summary>
    /// <param name="record">instance to extract from</param>
    /// <returns>Values from the instance</returns>
    internal delegate object[] ObjectToValuesDelegate(object record);

    /// <summary>
    /// Create an object (function created based upon a type)
    /// </summary>
    /// <returns>New object</returns>
    internal delegate object CreateObjectDelegate();

    /// <summary>
    /// Creates a series of functions that directly access
    /// the classes rather than creating all the access methods
    /// every time we access data.
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// Create a delegate based on the field information that
        /// converts an object into an array of fields
        /// </summary>
        /// <param name="recordType">Object type to be processed</param>
        /// <param name="fields">Fields we are extracting</param>
        /// <returns>Delegate to convert object to fields</returns>
        public static ObjectToValuesDelegate ObjectToValuesMethod(Type recordType, FieldInfo[] fields)
        {
            var dm = new DynamicMethod("FileHelpersDynamic_GetAllValues",
                MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard,
                typeof (object[]),
                new[] {typeof (object)},
                recordType,
                true);

            ILGenerator generator = dm.GetILGenerator();

            generator.DeclareLocal(typeof (object[]));
            generator.DeclareLocal(recordType);

            generator.Emit(OpCodes.Ldc_I4, fields.Length);
            generator.Emit(OpCodes.Newarr, typeof (object));
            generator.Emit(OpCodes.Stloc_0);

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, recordType);
            generator.Emit(OpCodes.Stloc_1);

            for (int i = 0; i < fields.Length; i++) {
                FieldInfo field = fields[i];

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldloc_1);

                generator.Emit(OpCodes.Ldfld, field);

                if (field.FieldType.IsValueType)
                    generator.Emit(OpCodes.Box, field.FieldType);

                generator.Emit(OpCodes.Stelem_Ref);
            }

            // return the value
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            return (ObjectToValuesDelegate) dm.CreateDelegate(typeof (ObjectToValuesDelegate));
        }

        /// <summary>
        /// Get the default constructor for the class
        /// </summary>
        /// <param name="recordType">class we are working on</param>
        /// <returns>Constructor information</returns>
        public static ConstructorInfo GetDefaultConstructor(Type recordType)
        {
            return recordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                Type.EmptyTypes,
                new ParameterModifier[] {});
        }

        /// <summary>
        /// Create a dynamic function that assigns fields to
        /// the record
        /// </summary>
        /// <param name="recordType">class we are basing assignment on</param>
        /// <param name="fields">LIst of fields to assign</param>
        /// <returns>Function to create object and perform assignment</returns>
        public static CreateAndAssignDelegate CreateAndAssignValuesMethod(Type recordType, FieldInfo[] fields)
        {
            var dm = new DynamicMethod("FileHelpersDynamicCreateAndAssign",
                MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard,
                typeof (object),
                new[] {typeof (object[])},
                recordType,
                true);

            ILGenerator generator = dm.GetILGenerator();

            generator.DeclareLocal(recordType);
            generator.Emit(OpCodes.Newobj, GetDefaultConstructor(recordType));
            generator.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < fields.Length; i++) {
                FieldInfo field = fields[i];

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldelem_Ref);

                if (field.FieldType.IsValueType)
                    generator.Emit(OpCodes.Unbox_Any, field.FieldType);
                else
                    generator.Emit(OpCodes.Castclass, field.FieldType);

                generator.Emit(OpCodes.Stfld, field);
            }

            // return the value
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            return (CreateAndAssignDelegate) dm.CreateDelegate(typeof (CreateAndAssignDelegate));
        }

        /// <summary>
        /// Assign fields to record type
        /// </summary>
        /// <param name="recordType">Class to base function on</param>
        /// <param name="fields">List of fields to assign</param>
        /// <returns>function that assigns only</returns>
        public static AssignDelegate AssignValuesMethod(Type recordType, FieldInfo[] fields)
        {
            var dm = new DynamicMethod("FileHelpersDynamic_Assign",
                MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard,
                null,
                new[] {typeof (object), typeof (object[])},
                recordType,
                true);

            var generator = dm.GetILGenerator();

            generator.DeclareLocal(recordType);
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, recordType);
            generator.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < fields.Length; i++) {
                var field = fields[i];

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldelem_Ref);

                if (field.FieldType.IsValueType)
                    generator.Emit(OpCodes.Unbox_Any, field.FieldType);
                else
                    generator.Emit(OpCodes.Castclass, field.FieldType);

                generator.Emit(OpCodes.Stfld, field);
            }

            generator.Emit(OpCodes.Ret);

            return (AssignDelegate) dm.CreateDelegate(typeof (AssignDelegate));
        }

        /// <summary>
        /// Create a function that will create an instance of the class
        /// </summary>
        /// <param name="recordType">class we want to create an instance of</param>
        /// <returns>Function to create an instance</returns>
        public static CreateObjectDelegate CreateFastConstructor(Type recordType)
        {
            var dm = new DynamicMethod("FileHelpersDynamicCreateRecordFast",
                MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard,
                typeof (object),
				new Type[] {},
                recordType,
                true);

            ILGenerator generator = dm.GetILGenerator();

            generator.Emit(OpCodes.Newobj, GetDefaultConstructor(recordType));
            generator.Emit(OpCodes.Ret);

            return (CreateObjectDelegate) dm.CreateDelegate(typeof (CreateObjectDelegate));
        }

        /// <summary>
        /// Create a function to get the field name from the object
        /// </summary>
        /// <param name="fi">Field information</param>
        /// <returns>Function to directly get field</returns>
        public static GetFieldValueCallback CreateGetFieldMethod(FieldInfo fi)
        {
            var dm = new DynamicMethod("FileHelpersDynamic_GetValue" + fi.Name,
                MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard,
                typeof (object),
                new[] {typeof (object)},
                fi.DeclaringType,
                true);

            ILGenerator generator = dm.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, fi.DeclaringType);
            generator.Emit(OpCodes.Ldfld, fi);
            generator.Emit(OpCodes.Ret);

            return (GetFieldValueCallback) dm.CreateDelegate(typeof (GetFieldValueCallback));
        }

        /// <summary>
        /// Get all the FieldInfo details from the class and optionally it's parents
        /// </summary>
        /// <param name="currentType">Type we are interrogating</param>
        /// <returns>IEnumerable list of fields in type</returns>
        public static IEnumerable<FieldInfo> RecursiveGetFields(Type currentType)
        {
            // If we are inherited and we have not stopped recursion, get parent types
            if (currentType.BaseType != null &&
                !currentType.IsDefined(typeof (IgnoreInheritedClassAttribute), false)) {
                foreach (var item in RecursiveGetFields(currentType.BaseType))
                    yield return item;
            }

            if (currentType == typeof (object))
                yield break;

            FieldInfoCacheManipulator.ResetFieldInfoCache(currentType);


            foreach (var fi in currentType.GetFields(BindingFlags.Public |
                                                     BindingFlags.NonPublic |
                                                     BindingFlags.Instance |
                                                     BindingFlags.DeclaredOnly)) {
                if (!(typeof (Delegate)).IsAssignableFrom(fi.FieldType))
                    yield return fi;
            }
        }
    }
}