namespace FileHelpers
{
    /// <summary>
    /// Indicates the Conversion used in the <see cref="T:FileHelpers.FieldConverterAttribute"/>.
    /// </summary>
    public enum ConverterKind
    {
        /// <summary>Null Converter.</summary>
        None = 0,

        /// <summary>
        /// <para>Convert from or to <b>Date</b> values.</para>
        /// <para>Params: arg1 is the <b>string</b> with the date format.</para>
        /// </summary>
        Date,

        /// <summary>
        /// <para>Convert from or to <b>Boolean</b> values.</para>
        /// <para>Params: arg1 is the <b>TRUE</b> string</para>
        /// <para>Params: arg2 is the <b>FALSE</b> string</para>
        /// </summary>
        Boolean,

        /// <summary>
        /// <para>Convert from or to <b>Byte</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        Byte,

        /// <summary>
        /// <para>Convert from or to <b>Int16 or short</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        Int16,

        /// <summary>
        /// <para>Convert from or to <b>Int32 or int</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        Int32,

        /// <summary>
        /// <para>Convert from or to <b>Int64 or long</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        Int64,

        /// <summary>
        /// <para>Convert from or to <b>Decimal</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        Decimal,

        /// <summary>
        /// <para>Convert from or to <b>Double</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        Double,
        //Added by Shreyas Narasimhan (17 March 2010)
        /// <summary>
        /// <para>Convert from or to <b>Double</b> values. Understands Percent '%' symbol 
        /// and if present returns number /100 only while reading</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        PercentDouble,

        /// <summary>
        /// <para>Convert from or to <b>Single</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        Single,

        /// <summary>
        /// <para>Convert from or to <b>Byte</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        SByte,

        /// <summary>
        /// <para>Convert from or to <b>UInt16 or unsigned short</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        UInt16,

        /// <summary>
        /// <para>Convert from or to <b>UInt32 or unsigned int</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        UInt32,

        /// <summary>
        /// <para>Convert from or to <b>UInt64 or unsigned long</b> values.</para>
        /// <para>Params: arg1 is either a <b>decimal separator</b>, by default '.', or a culture name (eg. "en-US", "fr-FR")</para>
        /// </summary>
        UInt64,

        /// <summary>
        /// <para>Convert from or to <b>Date</b> values using more than one valid format.</para>
        /// <para>Params: arg1 is a <b>string</b> with the main date format. This format is the unique used for write.</para>
        /// <para>Params: arg2 is a <b>string</b> with another valid read format.</para>
        /// <para>Params: arg3 is a <b>string</b> with another valid read format.</para>
        /// </summary>
        DateMultiFormat,

        // Added by Alexander Obolonkov 2007.11.08
        /// <summary>
        /// <para>Convert from or to <b>Char</b> values.</para>
        /// <para>Params: arg1 is a <b>string</b> with "" for default behavior, "x" for make the char ToLower or "X" for make it ToUpper.</para>
        /// </summary>
        Char,
        // Added by Alexander Obolonkov 2007.11.08
        /// <summary>
        /// <para>Convert from/to <b>Guid</b> values.</para>
        /// <para>Params: arg1 is a <b>string</b> with one of the Guid.ToString() formats: "N", "D", "B", or "P"</para>
        /// <para>   "N" ->  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx </para>
        /// <para>   "D" ->  xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx </para>
        /// <para>   "B" ->  {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx} </para>
        /// <para>   "P" ->  (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx) </para>
        /// </summary>
        Guid,
        // Added by Jimmy Mathai 2018.08.15
        /// <summary>
        /// <para>Add fixed <b>Padding</b> to Any types</para>
        /// <para>Params: arg1 is an <b>integer</b>. This is the total length for writing.</para>
        /// <para>Params: arg2 is a <b>Align</b> weather to padd left or right. Default is left</para>
        /// <para>Params: arg3 is a <b>Char</b>. The character to padd with. Default is space.</para>
        /// </summary>
        Padding
    }
}