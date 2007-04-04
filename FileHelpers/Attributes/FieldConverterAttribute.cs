#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Reflection;
using System.Text;

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
            Kind = converter;
            
			Type convType;

			switch (converter)
			{
				case ConverterKind.Date:
					convType = typeof (ConvertHelpers.DateTimeConverter);
					break;

				case ConverterKind.Byte:
					convType = typeof (ConvertHelpers.ByteConverter);
					break;

                case ConverterKind.SByte:
                    convType = typeof(ConvertHelpers.SByteConverter);
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

                case ConverterKind.UInt16:
                    convType = typeof(ConvertHelpers.UInt16Converter);
                    break;
                case ConverterKind.UInt32:
                    convType = typeof(ConvertHelpers.UInt32Converter);
                    break;
                case ConverterKind.UInt64:
                    convType = typeof(ConvertHelpers.UInt64Converter);
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

		/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write ops. </summary>
		/// <param name="customConverter">The Type of your custom converter.</param>
		/// <param name="arg1">The first param pased directly to the Converter Constructor.</param>
		public FieldConverterAttribute(Type customConverter, string arg1) : this(customConverter, new string[] {arg1})
		{
		}

		/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write ops. </summary>
		/// <param name="customConverter">The Type of your custom converter.</param>
		/// <param name="arg1">The first param pased directly to the Converter Constructor.</param>
		/// <param name="arg2">The second param pased directly to the Converter Constructor.</param>
		public FieldConverterAttribute(Type customConverter, string arg1, string arg2)
			: this(customConverter, new string[] {arg1, arg2})
		{
		}

		/// <summary>Indicates the <see cref="ConverterKind"/> used for read/write ops. </summary>
		/// <param name="customConverter">The Type of your custom converter.</param>
		/// <param name="arg1">The first param pased directly to the Converter Constructor.</param>
		/// <param name="arg2">The second param pased directly to the Converter Constructor.</param>
		/// <param name="arg3">The third param pased directly to the Converter Constructor.</param>
		public FieldConverterAttribute(Type customConverter, string arg1, string arg2, string arg3)
			: this(customConverter, new string[] {arg1, arg2, arg3})
		{
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

		internal ConverterBase Converter;
        internal ConverterKind Kind;

		#endregion

		#region "  CreateConverter  "

		private void CreateConverter(Type convType, object[] args)
		{
			if (typeof (ConverterBase).IsAssignableFrom(convType))
			{
				ConstructorInfo constructor;
				constructor = convType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, ArgsToTypes(args), null);

				if (constructor == null)
				{
					if (args.Length == 0)
						throw new BadUsageException("Empty constructor for converter: " + convType.Name + " was not found. You must add a constructor without args (can be public or private)");
					else
						throw new BadUsageException("Constructor for converter: " + convType.Name + " with these arguments: (" + ArgsDesc(args) + ") was not found. You must add a constructor with this signature (can be public or private)");
				}

				try
				{
					Converter = (ConverterBase) constructor.Invoke(args);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}

			}
#if ! MINI
			else if (convType.IsEnum)
			{
				Converter = new EnumConverter(convType);
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

		private string ArgsDesc(object[] args)
		{
			
			string res = DisplayType(args[0]);

			for(int i = 1; i < args.Length; i++)
				res += ", " + DisplayType(args[i]);

			return res;
		}

		private string DisplayType(object o)
		{
			if (o == null)
				return "Object";
			else
				return o.GetType().Name;
		}

		#endregion


        internal void ValidateTypes(FieldInfo fi)
        {
            bool valid = false;

            Type fieldType = fi.FieldType;

#if NET_2_0

            if (fieldType.IsValueType &&
                  fieldType.IsGenericType &&
                    fieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                fieldType = fieldType.GetGenericArguments()[0];
            }

#endif

            switch (Kind)
            {
                case ConverterKind.None:
                    valid = true;
                    break;

                case ConverterKind.Date:
                    valid = typeof(DateTime) == fieldType;
                    break;

                case ConverterKind.Byte:
                case ConverterKind.SByte:
                case ConverterKind.Int16:
                case ConverterKind.Int32:
                case ConverterKind.Int64:
                case ConverterKind.UInt16:
                case ConverterKind.UInt32:
                case ConverterKind.UInt64:
                case ConverterKind.Decimal:
                case ConverterKind.Double:
                case ConverterKind.Single:
                case ConverterKind.Boolean:
                    valid = Kind.ToString() == fieldType.UnderlyingSystemType.Name;
                    break;

            }

            if (valid == false)
               throw new BadUsageException(
                   "The Converter of the field: '" + fi.Name + "' is wrong. The field is of Type: " + fieldType.Name + " and the converter is for type: " + Kind.ToString());
        }
    }
}