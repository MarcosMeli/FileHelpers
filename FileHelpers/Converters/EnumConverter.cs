using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    internal sealed class EnumConverter : ConverterBase
    {
        private readonly Type mEnumType;
        private readonly EnumFormat mFormat;
        /// <summary>
        /// whether to output enum as string or as integer
        /// </summary>
        private enum EnumFormat
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

        public EnumConverter(Type sourceEnum)

            : this(sourceEnum, "s")
        { }
           

        public EnumConverter(Type sourceEnum, string format)
        {
            if (sourceEnum.IsEnum == false)
                throw new BadUsageException("The Input sourceType must be an Enum but is of type " + sourceEnum.Name);

            mEnumType = sourceEnum;
            switch (format.Trim())
            {
                case "s":
                case "S":
                    mFormat = EnumFormat.String;
                    break;
                case "n":
                case "N":
                    mFormat = EnumFormat.Number;
                    break;
                default:
                    throw new BadUsageException("The format parameter must be either \"s\" (converts enum to string) or \"n\" (converts enum to number).");

            }
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

            switch(mFormat)
            {
                case EnumFormat.String:
                    return from.ToString();
                default:
                    return Convert.ToInt32(from).ToString();
            }

        }
    }
}