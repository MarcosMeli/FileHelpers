using System;
using System.Collections.Generic;
using System.Reflection;

namespace FileHelpers
{
    internal static class Container
    {
        #region TypeResolutionException

        [Serializable]
        public class TypeResolutionException : Exception
        {
            public TypeResolutionException()
            {
            }

            public TypeResolutionException(string message)
                : base(message)
            {
            }
        }

        #endregion

        [AttributeUsage(AttributeTargets.Class)]
        public class SingletonAttribute : Attribute { }

        #region Mapping

        private class Mapping
        {
            public readonly Type Contract;
            public readonly Type Implementation;
            public readonly ConstructorInfo Constructor;
            public readonly bool IsSingleton;
            public object SingletonInstance;

            public Mapping(Type contract, Type implementation)
            {
                Contract = contract;
                Implementation = implementation;
                Constructor = GetLongestConstructor();
                IsSingleton = implementation.IsDefined(typeof (SingletonAttribute), false);
            }

            private ConstructorInfo GetLongestConstructor()
            {
                var constructors =
                    Implementation.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Array.Sort(constructors,
                           (a, b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length));
                return constructors[0];
            }
        }

        #endregion

        private const string AssemblyPrefix = "FileHelpers";

        private static readonly IDictionary<Type, Mapping> mCache = new Dictionary<Type, Mapping>();

        static Container()
        {
            List<Type> types = GetAllTypesFromRelevantAssemblies();

            foreach (var contract in GetAllContractTypes(types))
            {
                Type implementation = FindImplementingType(contract, types);
                if (implementation != null)
                    mCache[contract] = new Mapping(contract, implementation);
            }
        }

        private static List<Type> GetAllTypesFromRelevantAssemblies()
        {
            var types = new List<Type>();
            var assemblies = Array.FindAll(AppDomain.CurrentDomain.GetAssemblies(),
                                           assembly => assembly.GetName().Name.StartsWith(AssemblyPrefix));
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetTypes());
            }
            return types;
        }

        private static List<Type> GetAllContractTypes(List<Type> types)
        {
            return types.FindAll(type => type.IsInterface && type.Name.StartsWith("I"));
        }

        private static Type FindImplementingType(Type contract, List<Type> types)
        {
            var contractName = contract.Name.Substring(1);
            return types.Find(t => !t.IsAbstract && contract.IsAssignableFrom(t) && t.Name.EndsWith(contractName));
        }

        private static readonly object[] mEmpty = new object[0];

        public static TContract Resolve<TContract>()
        {
            return (TContract) Resolve(typeof (TContract), mEmpty, 0);
        }

        public static TContract Resolve<TContract>(params object[] args)
        {
            return (TContract) Resolve(typeof (TContract), args, 0);
        }

        private static object Resolve(Type contract, object[] args, int index)
        {
            Mapping mapping;
            if (!mCache.TryGetValue(contract, out mapping))
                throw new TypeResolutionException("Could not find implementation for contract: " + contract);

            if (mapping.IsSingleton)
            {
                if (mapping.SingletonInstance == null)
                {
                    mapping.SingletonInstance = CreateInstance(mapping, args, index);
                }
                return mapping.SingletonInstance;
            }

            return CreateInstance(mapping, args, index);
        }

        private static object CreateInstance(Mapping mapping, object[] args, int index)
        {
            var constructorParameters = mapping.Constructor.GetParameters();

            var parameterValues = new List<object>(constructorParameters.Length);

            foreach (var parameterInfo in constructorParameters)
            {
                var parameterType = parameterInfo.ParameterType;
                if (index < args.Length && parameterType.IsAssignableFrom(args[index].GetType()))
                    parameterValues.Add(args[index++]);
                else
                    parameterValues.Add(Resolve(parameterType, args, index));
            }

            if (index < args.Length)
                throw new TypeResolutionException("Too many constructor args for contract: " + mapping.Contract);

            try
            {
                return mapping.Constructor.Invoke(parameterValues.ToArray());
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}