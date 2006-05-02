#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Globalization;

namespace FileHelpers
{
	/// <summary>Class that provides static methods that returns a default <see cref="ConverterBase">Converter</see> to the basic types.</summary>
	/// <remarks>Used by the <see cref="FileHelpers.FieldConverterAttribute"/>.</remarks>
	internal sealed class ConvertHelpers
	{
		#region "  Constructors  "

		// Not allow direct creation
		private ConvertHelpers()
		{
		}

		static CultureInfo ci;

		static ConvertHelpers()
		{
			ci = new CultureInfo(CultureInfo.CurrentCulture.LCID);
			ci.NumberFormat.NumberDecimalSeparator = ".";
			ci.NumberFormat.NumberGroupSeparator = ",";
		}

		#endregion

		#region "  CreateCulture  "

		static CultureInfo CreateCulture(Char decimalSep)
		{
			CultureInfo ci = new CultureInfo(CultureInfo.CurrentCulture.LCID);

			if (decimalSep == '.')
			{
				ci.NumberFormat.NumberDecimalSeparator = ".";
				ci.NumberFormat.NumberGroupSeparator = ",";
			}
			else
			{
				ci.NumberFormat.NumberDecimalSeparator = decimalSep.ToString();
				ci.NumberFormat.NumberGroupSeparator = ".";
			}

			return ci;
		}

		#endregion

		#region "  GetDefaultConverter  "

		internal static ConverterBase GetDefaultConverter(string fieldName, Type fieldType)
		{
			// Try to assign a default Converter
			if (fieldType == typeof (Int16))
				return ToInt16();
			else if (fieldType == typeof (Int32))
				return ToInt32();
			else if (fieldType == typeof (Int64))
				return ToInt64();
			else if (fieldType == typeof (SByte))
				return ToSByte();
			else if (fieldType == typeof (Decimal))
				return ToDecimal();
			else if (fieldType == typeof (Double))
				return ToDouble();
			else if (fieldType == typeof (Single))
				return ToSingle();
			else if (fieldType == typeof (DateTime))
				return ToDateTime();
			else if (fieldType == typeof (Boolean))
				return ToBoolean();
			else if (fieldType == typeof (UInt16) ||
				fieldType == typeof (UInt32) ||
				fieldType == typeof (UInt64) ||
				fieldType == typeof (Byte) ||
				fieldType == typeof (string))
				return null;
#if ! MINI
			else if (fieldType.IsEnum)
				return new EnumConverter(fieldType);
#endif			

			throw new BadUsageException("The field: '" + fieldName + "' is of a non system type (" + fieldType.Name + ") then need a CustomConverter (see the docs for more info).");
		}

		#endregion

		#region "  Int16, Int32, Int64 Converters  "

		#region "  Convert Handlers  "

		/// <summary>
		/// Returns a Byte <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToSByte()
		{
			return new SByteConverter();
		}

		/// <summary>
		/// Returns a Int16 <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToInt16()
		{
			return new Int16Converter();
		}

		/// <summary>
		/// Returns a Int32 <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToInt32()
		{
			return new Int32Converter();
		}

		/// <summary>
		/// Returns a Int64 <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToInt64()
		{
			return new Int64Converter();
		}

		#endregion 

		#region "  Convert Classes  "

		internal class SByteConverter : ConverterBase
		{
			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = sbyte.Parse(StringHelper.RemoveBlanks(from));
				}
				catch
				{
					throw new ConvertException(from, typeof (byte));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ChangeType(from, typeof (byte), null).ToString();
			}
		}

		internal class Int16Converter : ConverterBase
		{
			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = Int16.Parse(StringHelper.RemoveBlanks(from));
				}
				catch
				{
					throw new ConvertException(from, typeof (Int16));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ChangeType(from, typeof (Int16), null).ToString();
			}
		}

		internal class Int32Converter : ConverterBase
		{
			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = Int32.Parse(StringHelper.RemoveBlanks(from), null);
				}
				catch
				{
					throw new ConvertException(from, typeof (Int32));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ToInt32(from).ToString((IFormatProvider) null);
			}
		}


		internal class Int64Converter : ConverterBase
		{
			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = Int64.Parse(StringHelper.RemoveBlanks(from));
				}
				catch
				{
					throw new ConvertException(from, typeof (Int64));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ToInt64(from).ToString((IFormatProvider) null);
			}
		}

		#endregion

		#endregion

		#region "  Single, Double, DecimalConverters  "

		#region "  Convert Handlers  "

		/// <summary>
		/// Returns a Int16 <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToDecimal()
		{
			return ToDecimal('.');
		}

		public static ConverterBase ToDecimal(Char decimalSeparator)
		{
			return new DecimalConverter(decimalSeparator);
		}

		/// <summary>
		/// Returns a Int32 <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToSingle()
		{
			return ToSingle('.');
		}

		public static ConverterBase ToSingle(Char decimalSeparator)
		{
			return new SingleConverter(decimalSeparator);
		}

		/// <summary>
		/// Returns a Int64 <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToDouble()
		{
			return ToDouble('.');
		}

		public static ConverterBase ToDouble(Char decimalSeparator)
		{
			return new DoubleConverter(decimalSeparator);
		}

		#endregion 

		#region "  Convert Classes  "

		internal class DecimalConverter : ConverterBase
		{
			CultureInfo ci;

			public DecimalConverter(Char decimalSep)
			{
				ci = CreateCulture(decimalSep);
			}

			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = Decimal.Parse(StringHelper.RemoveBlanks(from), ci);
				}
				catch
				{
					throw new ConvertException(from, typeof (Decimal));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ToDecimal(from).ToString(ci);
			}
		}

		internal class SingleConverter : ConverterBase
		{
			CultureInfo ci;

			public SingleConverter(Char decimalSep)
			{
				ci = CreateCulture(decimalSep);
			}

			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = Single.Parse(StringHelper.RemoveBlanks(from), ci);
				}
				catch
				{
					throw new ConvertException(from, typeof (Single));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ToSingle(from).ToString(ci);
			}
		}


		internal class DoubleConverter : ConverterBase
		{
			CultureInfo ci;

			public DoubleConverter(Char decimalSep)
			{
				ci = CreateCulture(decimalSep);
			}

			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = Double.Parse(StringHelper.RemoveBlanks(from), ci);
				}
				catch
				{
					throw new ConvertException(from, typeof (Double));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ToDouble(from).ToString(ci);
			}
		}

		#endregion

		#endregion

		#region "  Date Converters  "

		#region "  Convert Handlers  "

		/// <summary>
		/// <para>Returns a Date <see cref="ConverterBase">Converter</see>.</para>
		/// <para>Uses the default format "ddMMyyyy".</para>
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToDateTime()
		{
			return ToDateTime("ddMMyyyy");
		}

		/// <summary>
		/// <para>Returns a Date <see cref="ConverterBase">Converter</see>.</para>
		/// <para>Uses the specified format.</para>
		/// </summary>
		/// <param name="format">The format used to convert from/to a string.</param>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToDateTime(string format)
		{
			return new DateTimeConverter(format);
		}

		#endregion 

		#region "  Convert Classes  "

		internal class DateTimeConverter : ConverterBase
		{
			string mFormat;

			public DateTimeConverter() : this("ddMMyyyy")
			{
			}

			public DateTimeConverter(string format)
			{
				if (format == null || format == String.Empty)
					throw new BadUsageException("The format of the DateTime Converter can be null or empty.");

				try
				{
					string tmp = DateTime.Now.ToString(format);
				}
				catch
				{
					throw new BadUsageException("The format: '" + format + " is invalid for the DateTime Converter.");
				}

				mFormat = format;
			}

			public override object StringToField(string from)
			{
				if (from == null) from = string.Empty;

				object val;
				try
				{
					val = DateTime.ParseExact(from.Trim(), mFormat, null);
				}
				catch
				{
					string extra = String.Empty;
					if (from.Length > mFormat.Length)
						extra = " There are more chars than in the format string.";
					else if (from.Length < mFormat.Length)
						extra = " There are less chars than in the format string.";
					else
						extra = " Using the format: '" + mFormat + "'";


					throw new ConvertException(from, typeof (DateTime), extra);
				}
				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ToDateTime(from).ToString(mFormat);
			}
		}

		#endregion

		#endregion

		#region "  Boolean Converters  "

		#region "  Convert Handlers  "

		/// <summary>
		/// Returns a Boolean <see cref="ConverterBase">Converter</see>.
		/// </summary>
		/// <returns>The <see cref="ConverterBase"/> that performs the convertion.</returns>
		public static ConverterBase ToBoolean()
		{
			return new BooleanConverter();
		}

		#endregion

		#region "  Convert Classes  "

		internal class BooleanConverter : ConverterBase
		{
			public override object StringToField(string from)
			{
				object val;
				try
				{
					val = Boolean.Parse(from);
				}
				catch
				{
					throw new ConvertException(from, typeof (Boolean));
				}

				return val;
			}

			public override string FieldToString(object from)
			{
				return Convert.ToBoolean(from).ToString();
			}
		}

		#endregion

		#endregion
	}
}