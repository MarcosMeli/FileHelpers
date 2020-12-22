using System;
using System.Reflection;
using FileHelpers.Converters;

namespace FileHelpers
{
    /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">Complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldConverterAttribute : Attribute
    {
        #region "  Constructors  "

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        public FieldConverterAttribute(ConverterKind converter)
            : this(converter, new string[] {}) {}

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(ConverterKind converter, string arg1)
            : this(converter, new string[] {arg1}) {}

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(ConverterKind converter, string arg1, string arg2)
            : this(converter, new string[] {arg1, arg2}) {}

        /// <summary>Indicates the <see cref="ConverterKind"/> used for read/write operations. </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        /// <param name="arg3">The third param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(ConverterKind converter, string arg1, string arg2, string arg3)
            : this(converter, new string[] {arg1, arg2, arg3}) {}


        /// <summary>
        /// Indicates the <see cref="ConverterKind"/> used for read/write operations. 
        /// </summary>
        /// <param name="converter">The <see cref="ConverterKind"/> used for the transformations.</param>
        /// <param name="args">An array of parameters passed directly to the Converter</param>
        private FieldConverterAttribute(ConverterKind converter, params string[] args)
        {
            Type convType;

            switch (converter) {
                case ConverterKind.Date:
                    convType = typeof (DateTimeConverter);
                    break;

                case ConverterKind.DateMultiFormat:
                    convType = typeof (DateTimeMultiFormatConverter);
                    break;

                case ConverterKind.Byte:
                    convType = typeof (ByteConverter);
                    break;

                case ConverterKind.SByte:
                    convType = typeof (SByteConverter);
                    break;

                case ConverterKind.Int16:
                    convType = typeof (Int16Converter);
                    break;
                case ConverterKind.Int32:
                    convType = typeof (Int32Converter);
                    break;
                case ConverterKind.Int64:
                    convType = typeof (Int64Converter);
                    break;

                case ConverterKind.UInt16:
                    convType = typeof (UInt16Converter);
                    break;
                case ConverterKind.UInt32:
                    convType = typeof (UInt32Converter);
                    break;
                case ConverterKind.UInt64:
                    convType = typeof (UInt64Converter);
                    break;

                case ConverterKind.Decimal:
                    convType = typeof (DecimalConverter);
                    break;
                case ConverterKind.Double:
                    convType = typeof (DoubleConverter);
                    break;
                    // Added by Shreyas Narasimhan 17 March 2010
                case ConverterKind.PercentDouble:
                    convType = typeof (PercentDoubleConverter);
                    break;
                case ConverterKind.Single:
                    convType = typeof (SingleConverter);
                    break;
                case ConverterKind.Boolean:
                    convType = typeof (BooleanConverter);
                    break;
                    // Added by Alexander Obolonkov 2007.11.08
                case ConverterKind.Char:
                    convType = typeof (CharConverter);
                    break;
                    // Added by Alexander Obolonkov 2007.11.08
                case ConverterKind.Guid:
                    convType = typeof (GuidConverter);
                    break;
                default:
                    throw new BadUsageException("Converter '" + converter.ToString() +
                                                "' not found, you must specify a valid converter.");
            }
            //mType = type;

            CreateConverter(convType, args);
        }

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(Type customConverter, string arg1)
            : this(customConverter, new string[] {arg1}) {}

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(Type customConverter, string arg1, string arg2)
            : this(customConverter, new string[] {arg1, arg2}) {}

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="arg1">The first param passed directly to the Converter Constructor.</param>
        /// <param name="arg2">The second param passed directly to the Converter Constructor.</param>
        /// <param name="arg3">The third param passed directly to the Converter Constructor.</param>
        public FieldConverterAttribute(Type customConverter, string arg1, string arg2, string arg3)
            : this(customConverter, new string[] {arg1, arg2, arg3}) {}

        /// <summary>Indicates a custom <see cref="ConverterBase"/> implementation.</summary>
        /// <param name="customConverter">The Type of your custom converter.</param>
        /// <param name="args">A list of params passed directly to your converter constructor.</param>
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


        /// <summary>The final concrete converter used for FieldToString and StringToField operations </summary>
        public ConverterBase Converter { get; private set; }

        #endregion

        #region "  CreateConverter  "

        private void CreateConverter(Type convType, object[] args)
        {
            if (typeof(ConverterBase).IsAssignableFrom(convType)) {
                ConstructorInfo constructor;
                constructor = convType.GetConstructor(
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    ArgsToTypes(args),
                    null);

                if (constructor == null) {
                    if (args.Length == 0) {
                        throw new BadUsageException("Empty constructor for converter: " + convType.Name +
                                                    " was not found. You must add a constructor without args (can be public or private)");
                    }
                    else {
                        throw new BadUsageException("Constructor for converter: " + convType.Name +
                                                    " with these arguments: (" + ArgsDesc(args) +
                                                    ") was not found. You must add a constructor with this signature (can be public or private)");
                    }
                }

                try {
                    Converter = (ConverterBase)constructor.Invoke(args);
                }
                catch (TargetInvocationException ex) {
                    throw ex.InnerException;
                }
            }
            else if (convType.IsEnum)
                if (args.Length == 0)
                    Converter = new EnumConverter(convType);
                else
                    Converter = new EnumConverter(convType, args[0] as string);

            else
                throw new BadUsageException("The custom converter must inherit from ConverterBase");
        }

        #endregion

        #region "  ArgsToTypes  "

        private static Type[] ArgsToTypes(object[] args)
        {
            if (args == null) {
                throw new BadUsageException(
                    "The args to the constructor can be null if you do not want to pass anything into them.");
            }

            var res = new Type[args.Length];

            for (int i = 0; i < args.Length; i++) {
                if (args[i] == null)
                    res[i] = typeof (object);
                else
                    res[i] = args[i].GetType();
            }

            return res;
        }

        private static string ArgsDesc(object[] args)
        {
            string res = DisplayType(args[0]);

            for (int i = 1; i < args.Length; i++)
                res += ", " + DisplayType(args[i]);

            return res;
        }

        private static string DisplayType(object o)
        {
            if (o == null)
                return "Object";
            else
                return o.GetType().Name;
        }

        #endregion
    }
}