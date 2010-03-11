namespace FileHelpers.Detection
{
    /// <summary>
    /// Provides a suggestion to the <see cref="SmartFormatDetector"/> 
    /// about the records in the file
    /// </summary>
    public enum FormatHint
    {
        /// <summary>No Info about the file format</summary>
        Unknown = 0,
        /// <summary>The file is likely to be of fixed length records</summary>
        FixedLength,
        /// <summary>The file is likely to be of delimited records</summary>
        Delimited,
        /// <summary>The file is likely to be of records delimited by a tab character</summary>
        DelimitedByTab,
        /// <summary>The file is likely to be of records delimited by a semicolon character</summary>
        DelimitedByComma,
        /// <summary>The file is likely to be of records delimited by a comma character</summary>
        DelimitedBySemicolon
    }
}