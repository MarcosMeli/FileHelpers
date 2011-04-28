using System;
using System.ComponentModel;
using System.Text;
using System.Globalization;

namespace FileHelpers
{

    /// <summary>
    /// extensions to help with types
    /// </summary>
	internal static class TypeHelper
	{
        /// <summary>
        /// Is this type any sort of numeric
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumericType(Type type)
        {
            return type == typeof(Int16) ||
                    type == typeof(Int32) ||
                    type == typeof(Int64) ||
                    type == typeof(UInt16) ||
                    type == typeof(UInt32) ||
                    type == typeof(UInt64) ||
                    type == typeof(byte) ||
                    type == typeof(sbyte) ||
                    type == typeof(decimal) ||
                    type == typeof(float) ||
                    type == typeof(double) ||
                    type == typeof(Int16?) ||
                    type == typeof(Int32?) ||
                    type == typeof(Int64?) ||
                    type == typeof(UInt16?) ||
                    type == typeof(UInt32?) ||
                    type == typeof(UInt64?) ||
                    type == typeof(byte?) ||
                    type == typeof(sbyte?) ||
                    type == typeof(decimal?) ||
                    type == typeof(float?) ||
                    type == typeof(double?);
        }
    }
}