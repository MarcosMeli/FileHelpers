

using System;
using System.ComponentModel;
using System.Text;
using System.Globalization;

namespace FileHelpers
{

	internal static class TypeHelper
	{
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