using System;
using System.Collections.Generic;
using System.Reflection;

namespace EmbeddedIoC
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

        private const string assemblyPrefix = "FileHelpers";

        private static readonly IDictionary<Type, ConstructorInfo> constructorCache =
            new Dictionary<Type, ConstructorInfo>();

        static Container()
        {
            List<Type> types = GetAllTypesFromRelevantAssemblies();

            foreach (var contract in GetAllContractTypes(types))
            {
                Type implementation = FindImplementingType(contract, types);
                if (implementation != null)
                    constructorCache[contract] = GetLongestConstructor(implementation);
            }
        }

        private static List<Type> GetAllTypesFromRelevantAssemblies()
        {
            var types = new List<Type>();
            var assemblies = Array.FindAll(AppDomain.CurrentDomain.GetAssemblies(),
                                           assembly => assembly.GetName().Name.StartsWith(assemblyPrefix));
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

        private static ConstructorInfo GetLongestConstructor(Type implementation)
        {
            var constructors = implementation.GetConstructors();
            Array.Sort(constructors,
                       (a, b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length));
            return constructors[0];
        }

        public static TContract Resolve<TContract>()
        {
            return (TContract) Resolve(typeof (TContract));
        }

        private static object Resolve(Type contract)
        {
            ConstructorInfo implementation;
            if (!constructorCache.TryGetValue(contract, out implementation))
                throw new TypeResolutionException("Could not find implementation for contract: " + contract);

            var constructorParameters = implementation.GetParameters();

            var parameterValues = new List<object>(constructorParameters.Length);

            foreach (var parameterInfo in constructorParameters)
                parameterValues.Add(Resolve(parameterInfo.ParameterType));

            return implementation.Invoke(parameterValues.ToArray());
        }
    }
}