

using System;


namespace FileHelpers
{
	/// <summary>
	/// Base class to provide bi-directional
	/// Field - String conversion.
	/// </summary>
	public abstract class ConverterBase
	{

        private static string mDefaultDateTimeFormat = "ddMMyyyy";


        /// <summary>
        /// <para>Allow you to set the default Date Format used for the Converter.</para>
        /// <para>using the same CustomDateTimeFormat that is used in the .NET framework.</para>
        /// <para>By default: "ddMMyyyy"</para>
        /// </summary>
        public static string DefaultDateTimeFormat
        {
            get { return mDefaultDateTimeFormat; }
            set 
            {
                try
                {
                    DateTime.Now.ToString(value);
                }
                catch
                {
                    throw new BadUsageException("The format: '" + value + " is invalid for the DateTime Converter.");
                }

                mDefaultDateTimeFormat= value;
            }
        }


		/// <summary>
		/// Convert a string in the file to a field value.
		/// </summary>
		/// <param name="from">The string to convert.</param>
		/// <returns>The Field value.</returns>
		public abstract object StringToField(string from);

		/// <summary>
		/// Convert a field value to an string to write this to the file.
		/// </summary>
		/// <remarks>The basic implementation just returns  from.ToString();</remarks>
		/// <param name="from">The field values to convert.</param>
		/// <returns>The string representing the field value.</returns>
		public virtual string FieldToString(object from)
		{
			if (from == null)
				return string.Empty;
			else
				return from.ToString();
		}

		/// <summary>
        /// If the class retures false the engines don´t pass null values to the converter. 
        /// If true the engines pass all the values to the converter.
        /// </summary>
		protected internal virtual bool CustomNullHandling
		{
			get { return false; }
		}

		internal Type mDestinationType;

		/// <summary>
		/// Thorws a ConvertException with the passed values
		/// </summary>
		/// <param name="from">The source string.</param>
		/// <param name="errorMsg" >The custom error msg.</param>
        /// <exception cref="ConvertException"></exception>
		protected void ThrowConvertException(string from, string errorMsg)
		{
			throw new ConvertException(from, mDestinationType, errorMsg);
		}

//		internal object mDefaultValue;
//		/// <summary>
//		/// Indicates 
//		/// </summary>
//		protected object DefaultValueFromField
//		{
//			get
//			{
//				return mDefaultValue;
//			}
//		}

	}
}