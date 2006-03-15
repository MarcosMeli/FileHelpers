#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers
{
	/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldConverterAttribute : Attribute
	{
		#region "  Constructors  "

		/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write ops. </summary>
		/// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
		public FieldConverterAttribute(ConverterKind converter) : this(converter, new string[] {})
		{
		}

		/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write ops. </summary>
		/// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
		/// <param name="arg1">The first param pased directly to the Converter Constructor.</param>
		public FieldConverterAttribute(ConverterKind converter, string arg1) : this(converter, new string[] {arg1})
		{
		}

		/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write ops. </summary>
		/// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
		/// <param name="arg1">The first param pased directly to the Converter Constructor.</param>
		/// <param name="arg2">The second param pased directly to the Converter Constructor.</param>
		public FieldConverterAttribute(ConverterKind converter, string arg1, string arg2)
			: this(converter, new string[] {arg1, arg2})
		{
		}

		/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write ops. </summary>
		/// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
		/// <param name="arg1">The first param pased directly to the Converter Constructor.</param>
		/// <param name="arg2">The second param pased directly to the Converter Constructor.</param>
		/// <param name="arg3">The third param pased directly to the Converter Constructor.</param>
		public FieldConverterAttribute(ConverterKind converter, string arg1, string arg2, string arg3)
			: this(converter, new string[] {arg1, arg2, arg3})
		{
		}

		private FieldConverterAttribute(ConverterKind converter, params string[] args)
		{
			Type convType;

			switch (converter)
			{
				case ConverterKind.Date:
					convType = typeof (ConvertHelpers.DateTimeConverter);
					break;

				case ConverterKind.Byte:
					convType = typeof (ConvertHelpers.SByteConverter);
					break;

				case ConverterKind.Int16:
					convType = typeof (ConvertHelpers.Int16Converter);
					break;
				case ConverterKind.Int32:
					convType = typeof (ConvertHelpers.Int32Converter);
					break;
				case ConverterKind.Int64:
					convType = typeof (ConvertHelpers.Int64Converter);
					break;
				case ConverterKind.Decimal:
					convType = typeof (ConvertHelpers.DecimalConverter);
					break;
				case ConverterKind.Double:
					convType = typeof (ConvertHelpers.DoubleConverter);
					break;
				case ConverterKind.Single:
					convType = typeof (ConvertHelpers.SingleConverter);
					break;
				case ConverterKind.Boolean:
					convType = typeof (ConvertHelpers.BooleanConverter);
					break;
				default:
					throw new BadUsageException("Converter '" + converter.ToString() + "' not found, you must specify a valid converter.");

			}
			//mType = type;

			CreateConverter(convType, args);
		}

		/// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
		/// <param name="customConverter">The Type of your custom converter.</param>
		/// <param name="args">A list of params pased directly to your converter constructor.</param>
		public FieldConverterAttribute(Type customConverter, params object[] args)
		{
			CreateConverter(customConverter, args);
		}

		/// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
		/// <param name="customConverter">The Type of your custom converter.</param>
		public FieldConverterAttribute(Type customConverter)
		{
			CreateConverter(customConverter, new object[] {});
		}

		#endregion

		#region "  Converter  "

		ConverterBase mConverter;

		/// <summary> The <see cref="ConverterBase"/> used to performs the string to records and record to string operations. </summary>
		public ConverterBase Converter
		{
			get { return mConverter; }
		}

		#endregion

		#region "  CreateConverter  "

		private void CreateConverter(Type convType, object[] args)
		{
			if (typeof (ConverterBase).IsAssignableFrom(convType))
			{
				ConstructorInfo constructor;
				constructor = convType.GetConstructor(ArgsToTypes(args));

				if (constructor == null)
					throw new BadUsageException("Constructor with " + args.Length.ToString() + " arguments of type " + convType.Name + " not found !!");

				try
				{
					mConverter = (ConverterBase) constructor.Invoke(args);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}

			}
#if ! MINI
			else if (convType.IsEnum)
			{
				mConverter = new EnumConverter(convType);
			}
#endif			
			else
				throw new BadUsageException("The custom converter must inherit from ConverterBase");
		}

		#endregion

		#region "  ArgsToTypes  "

		private Type[] ArgsToTypes(object[] args)
		{
			if (args == null)
				throw new BadUsageException("The args to the constructor can be null, if you not want to pass the ConverterKind.");

			Type[] res = new Type[args.Length];

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == null)
					res[i] = typeof (object);
				else
					res[i] = args[i].GetType();
			}

			return res;

		}

		#endregion
	}
}