namespace FileHelpers
{
    /// <summary>Indicates the align of the field when the <see cref="T:FileHelpers.FileHelperEngine"/> <b>writes</b> the record.</summary>
    public enum AlignMode
    {
        /// <summary>Aligns the field to the left.</summary>
        Left,

        /// <summary>Aligns the field to the center.</summary>
        Center,

        /// <summary>Aligns the field to the right.</summary>
        Right
    }

    /// <summary>
    /// Indicates padding mode when field writes the record.
    /// </summary>
    public enum PaddingMode
    {
        /// <summary>Padding field to the left.</summary>
        Left,

        /// <summary>Padding field to the right.</summary>
        Right
    }
}