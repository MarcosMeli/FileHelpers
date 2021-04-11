using System;

namespace FileHelpers.Fields
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
            return type == typeof (short) ||
                   type == typeof (int) ||
                   type == typeof (long) ||
                   type == typeof (ushort) ||
                   type == typeof (uint) ||
                   type == typeof (ulong) ||
                   type == typeof (byte) ||
                   type == typeof (sbyte) ||
                   type == typeof (decimal) ||
                   type == typeof (float) ||
                   type == typeof (double) ||
                   type == typeof (short?) ||
                   type == typeof (int?) ||
                   type == typeof (long?) ||
                   type == typeof (ushort?) ||
                   type == typeof (uint?) ||
                   type == typeof (ulong?) ||
                   type == typeof (byte?) ||
                   type == typeof (sbyte?) ||
                   type == typeof (decimal?) ||
                   type == typeof (float?) ||
                   type == typeof (double?);
        }
    }
}