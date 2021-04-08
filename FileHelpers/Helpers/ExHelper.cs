using System;

namespace FileHelpers.Helpers
{
    /// <summary>
    /// add validation exceptions
    /// </summary>
    internal static class ExHelper
    {

        /// <summary>
        /// Check that parameter is not null or empty and throw an exception
        /// </summary>
        /// <param name="param">value to check</param>
        /// <param name="paramName">parameter name</param>
        public static void CheckNullParam(string param, string paramName)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException(paramName, paramName + " can't be neither null nor empty");
        }

        /// <summary>
        /// Check that parameter is not null and throw an exception
        /// </summary>
        /// <param name="param">value to check</param>
        /// <param name="paramName">parameter name</param>
        public static void CheckNullParam(object param, string paramName)
        {
            if (param == null)
                throw new ArgumentNullException(paramName, paramName + " can't be null");
        }

        /// <summary>
        /// check that parameter 1 is different from parameter 2
        /// </summary>
        /// <param name="param1">value 1 to test</param>
        /// <param name="param1Name">name of value 1</param>
        /// <param name="param2">value 2 to test</param>
        /// <param name="param2Name">name of vlaue 2</param>
        public static void CheckDifferentsParams(object param1, string param1Name, object param2, string param2Name)
        {
            if (param1 == param2) {
                throw new ArgumentException(param1Name + " can't be the same as " + param2Name,
                    param1Name + " and " + param2Name);
            }
        }
    }
}