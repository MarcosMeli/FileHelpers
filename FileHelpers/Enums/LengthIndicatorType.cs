namespace FileHelpers
{
    ///<summary>Represents a record or field length indicator strategy</summary>
    public enum LengthIndicatorType
    {
        ///<summary>Least Significant Byte first (Little-Endian) integer</summary>
        LSB,
        ///<summary>Most Significant Byte first (Big-Endian) integer</summary>
        MSB,
        ///<summary>ASCII text numeric</summary>
        ASCII
    }
}