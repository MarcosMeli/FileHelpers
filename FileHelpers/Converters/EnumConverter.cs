using System;

namespace FileHelpers.Converters
{
    public sealed class EnumConverter : ConverterBase
    {
        private readonly Type mEnumType;
        private readonly EnumFormat mFormat;
        /// <summary>
        /// whether to output enum as string or as integer
        /// </summary>
        public enum EnumFormat
        {
            /// <summary>
            /// as string
            /// </summary>
            String,

            /// <summary>
            /// as integer
            /// </summary>
            Number,
        }

        public EnumConverter(Type sourceEnum, EnumFormat format = EnumFormat.String)
        {
            if (sourceEnum.IsEnum == false)
                throw new BadUsageException("The Input sourceType must be an Enum but is of type " + sourceEnum.Name);
            mEnumType = sourceEnum;
            mFormat = format;
        }


        public EnumConverter(Type sourceEnum, string format) : this(sourceEnum, GetEnumFormat(format))
        {
        }

        public override object StringToField(string from)
        {
            try
            {
                return Enum.Parse(mEnumType, from.Trim(), true);
            }
            catch (ArgumentException)
            {
                throw new ConvertException(from, mEnumType, "The value " + from + " is not present in the Enum.");
            }
        }

        public override string FieldToString(object from)
        {
            if (from == null)
                return string.Empty;

            switch (mFormat)
            {
                case EnumFormat.String:
                    return from.ToString();
                default:
                    {
                        int data = (int)from;
                        return data.ToString();
                    }
            }
        }

        private static EnumFormat GetEnumFormat(string format)
        {
            switch (format.Trim().ToLower())
            {
                case "n":
                    return EnumFormat.Number;
                case "s":
                    return EnumFormat.String;
                default:
                    throw new BadUsageException("The format parameter must be either \"s\" (converts enum to string) or \"n\" (converts enum to number).");

            }
        }
    }
}