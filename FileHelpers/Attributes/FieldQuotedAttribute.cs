using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Indicates that the field must be read and written as a Quoted String. 
    /// By default uses "" (double quotes)
    /// </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldQuotedAttribute : Attribute
    {
        /// <summary>The char used to quote the string.</summary>
        public char QuoteChar { get; private set; }

        internal QuoteMode mQuoteMode = QuoteMode.AlwaysQuoted;

        /// <summary>Indicates if the handling of optionals in the quoted field.</summary>
        public QuoteMode QuoteMode
        {
            get { return mQuoteMode; }
            internal set { mQuoteMode = value; }
        }

        internal MultilineMode mQuoteMultiline = MultilineMode.AllowForBoth;

        /// <summary>Indicates if the field can span multiple lines.</summary>
        public MultilineMode QuoteMultiline
        {
            get { return mQuoteMultiline; }
            internal set { mQuoteMultiline = value; }
        }

        /// <summary>
        /// Indicates that the field must be read and written as a Quoted String with double quotes.
        /// </summary>
        public FieldQuotedAttribute()
            : this('\"') {}

        /// <summary>
        /// Indicates that the field must be read and written as a Quoted String
        /// with the specified char.
        /// </summary>
        /// <param name="quoteChar">The char used to quote the string.</param>
        public FieldQuotedAttribute(char quoteChar)
            : this(quoteChar, QuoteMode.OptionalForRead, MultilineMode.AllowForBoth) {}

        /// <summary>
        /// Indicates that the field must be read and written as a "Quoted String"
        /// (that can be optional depending of the mode).
        /// </summary>
        /// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
        public FieldQuotedAttribute(QuoteMode mode)
            : this('\"', mode) {}

        /// <summary>Indicates that the field must be read and written as a Quoted String (that can be optional).</summary>
        /// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
        /// <param name="multiline">Indicates if the field can span multiple lines.</param>
        public FieldQuotedAttribute(QuoteMode mode, MultilineMode multiline)
            : this('"', mode, multiline) {}

        /// <summary>Indicates that the field must be read and written as a Quoted String (that can be optional).</summary>
        /// <param name="quoteChar">The char used to quote the string.</param>
        /// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
        public FieldQuotedAttribute(char quoteChar, QuoteMode mode)
            : this(quoteChar, mode, MultilineMode.AllowForBoth) {}

        /// <summary>Indicates that the field must be read and written as a Quoted String (that can be optional).</summary>
        /// <param name="quoteChar">The char used to quote the string.</param>
        /// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
        /// <param name="multiline">Indicates if the field can span multiple lines.</param>
        public FieldQuotedAttribute(char quoteChar, QuoteMode mode, MultilineMode multiline)
        {
            if (quoteChar == '\0')
                throw new BadUsageException("You can't use the null char (\\0) as quoted.");

            QuoteChar = quoteChar;
            QuoteMode = mode;
            QuoteMultiline = multiline;
        }

        /// <summary>Indicates that the field must be read and written like a Quoted String with double quotes.</summary>
        /// <param name="multiline">Indicates if the field can span multiple lines.</param>
        public FieldQuotedAttribute(MultilineMode multiline)
            : this('\"', QuoteMode.OptionalForRead, multiline) {}
    }
}