#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers
{
    ///<summary>Represents a record/field length indicator strategy</summary>
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