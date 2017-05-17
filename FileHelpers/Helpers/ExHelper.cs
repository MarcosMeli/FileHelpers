using System;

namespace FileHelpers
{
    /// <summary>
    /// add validation exceptions
    /// </summary>
    internal static class ExHelper
    {
        /// <summary>
        /// Check the string is null or empty and throw an exception
        /// </summary>
        /// <param name="val">value to test</param>
        /// <param name="paramName">name of parameter to check</param>
        public static void CheckNullOrEmpty(string val, string paramName)
        {
            if (string.IsNullOrEmpty(val))
                throw new ArgumentNullException(paramName, "Value can't be null or empty");
        }

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

        /// <summary>
        /// Check an integer value is positive (0 or greater)
        /// </summary>
        /// <param name="val">Integer to test</param>
        public static void PositiveValue(int val)
        {
            if (val < 0)
                throw new ArgumentException("The value must be greater than or equal to 0.");
        }
    }
}