namespace FileHelpers
{
    /// <summary>The condition used to include or exclude each record.</summary>
    public enum RecordCondition
    {
        /// <summary>No Condition, Include it always.</summary>
        None = 0,

        /// <summary>Include the record if it contains the selector string.</summary>
        IncludeIfContains,

        /// <summary>Include the record if it begins with selector string.</summary>
        IncludeIfBegins,

        /// <summary>Include the record if it ends with selector string.</summary>
        IncludeIfEnds,

        /// <summary>Include the record if it begins and ends with selector string.</summary>
        IncludeIfEnclosed,
        /// <summary>Include the record if it matches the regular expression passed as selector.</summary>
        IncludeIfMatchRegex,

        /// <summary>Exclude the record if it contains the selector string.</summary>
        ExcludeIfContains,

        /// <summary>Exclude the record if it begins with selector string.</summary>
        ExcludeIfBegins,

        /// <summary>Exclude the record if it ends with selector string.</summary>
        ExcludeIfEnds,

        /// <summary>Exclude the record if it begins and ends with selector string.</summary>
        ExcludeIfEnclosed,

        /// <summary>Exclude the record if it matches the regular expression passed as selector.</summary>
        ExcludeIfMatchRegex

    }
}