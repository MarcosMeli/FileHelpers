using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            var recordParam = Expression.Parameter(typeof(object));
            var record = Expression.Convert(recordParam, recordType);

            var fieldAccessors = fields
                .Select(x => Expression.Field(record, x))
                .Select(x => Expression.Convert(x, typeof(object)))
                .Cast<Expression>()
                .ToArray();

            var newArray = Expression.NewArrayInit(typeof(object), fieldAccessors);

            return Expression.Lambda<ObjectToValuesDelegate>(newArray, recordParam).Compile();
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
            var valuesParam = Expression.Parameter(typeof(object[]));

            var bindings = new MemberBinding[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];

                var arrayItem = Expression.ArrayAccess(valuesParam, Expression.Constant(i));
                var castItem = Expression.Convert(arrayItem, field.FieldType);

                bindings[i] = Expression.Bind(field, castItem);
            }

            var newExpression = Expression.New(GetDefaultConstructor(recordType));
            var binder = Expression.MemberInit(newExpression, bindings);

            return Expression.Lambda<CreateAndAssignDelegate>(binder, valuesParam).Compile();
        }

        /// <summary>
        /// Assign fields to record type
        /// </summary>
        /// <param name="recordType">Class to base function on</param>
        /// <param name="fields">List of fields to assign</param>
        /// <returns>function that assigns only</returns>
        public static AssignDelegate AssignValuesMethod(Type recordType, FieldInfo[] fields)
        {
            var recordParam = Expression.Parameter(typeof(object));
            var valuesParam = Expression.Parameter(typeof(object[]));

            var localRecord = Expression.Variable(recordType);
            var recordAssign = Expression.Assign(localRecord, Expression.Convert(recordParam, recordType));

            var fieldSetters = new Expression[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];

                var valueGetter = Expression.ArrayAccess(valuesParam, Expression.Constant(i));
                var castValue = Expression.Convert(valueGetter, field.FieldType);

                var fieldSetter = Expression.Field(localRecord, field);

                fieldSetters[i] = Expression.Assign(fieldSetter, castValue);
            }

            var body = Expression.Block(
                new[] { localRecord },
                recordAssign,
                Expression.Block(fieldSetters));

            return Expression.Lambda<AssignDelegate>(body, recordParam, valuesParam).Compile();
        }

        /// <summary>
        /// Create a function that will create an instance of the class
        /// </summary>
        /// <param name="recordType">class we want to create an instance of</param>
        /// <returns>Function to create an instance</returns>
        public static CreateObjectDelegate CreateFastConstructor(Type recordType)
        {
            var expression = Expression.New(GetDefaultConstructor(recordType));

            return Expression.Lambda<CreateObjectDelegate>(expression, null).Compile();
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