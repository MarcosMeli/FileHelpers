

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers
{
	/// <summary>Indicates the convertion used in the <see cref="T:FileHelpers.FieldConverterAttribute"/>.</summary>
	public enum ConverterKind
	{

		/// <summary>Null Converter.</summary>
		None = 0,
		/// <summary>
		/// <para>Convert from/to <b>Date</b> values.</para>
		/// <para>Params: arg1 is the <b>string</b> with the date format.</para>
		/// </summary>
		Date,
		/// <summary>Convert from/to <b>Boolean</b> values.</summary>
		Boolean,

        /// <summary>
        /// <para>Convert from/to <b>Byte</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Byte,
        /// <summary>
        /// <para>Convert from/to <b>Int16 or short</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Int16,
        /// <summary>
        /// <para>Convert from/to <b>Int32 or int</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Int32,
        /// <summary>
        /// <para>Convert from/to <b>Int64 or long</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Int64,
        /// <summary>
        /// <para>Convert from/to <b>Decimal</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Decimal,
        /// <summary>
        /// <para>Convert from/to <b>Double</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Double,
        /// <summary>
        /// <para>Convert from/to <b>Single</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Single,
        /// <summary>
        /// <para>Convert from/to <b>Byte</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        SByte,
        /// <summary>
        /// <para>Convert from/to <b>UInt16 or unsigned short</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        UInt16,
        /// <summary>
        /// <para>Convert from/to <b>UInt32 or unsigned int</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        UInt32,
        /// <summary>
        /// <para>Convert from/to <b>UInt64 or unsigned long</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        UInt64
	}
}