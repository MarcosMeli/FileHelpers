using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates the <see cref="TrimMode"/> used after reading to truncate the field. </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldTrimAttribute : Attribute
    {
        /// <summary>A string of chars used to trim.</summary>
        public Char[] TrimChars { get; private set; }

        /// <summary>The TrimMode used after read.</summary>
        public TrimMode TrimMode { get; private set; }


        private static readonly char[] WhitespaceChars = new char[] {
            '\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005',
            '\u2006', '\u2007', '\u2008',
            '\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
        };

        #region "  Constructors  "

        /// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. By default trims the blank spaces and tabs.</summary>
        /// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
        public FieldTrimAttribute(TrimMode mode)
            : this(mode, WhitespaceChars) {}

        /// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
        /// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
        /// <param name="chars">A list of chars used to trim.</param>
        public FieldTrimAttribute(TrimMode mode, params char[] chars)
        {
            TrimMode = mode;
            Array.Sort(chars);
            TrimChars = chars;
        }

        /// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
        /// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
        /// <param name="trimChars">A string of chars used to trim.</param>
        public FieldTrimAttribute(TrimMode mode, string trimChars)
            : this(mode, trimChars.ToCharArray()) {}

        #endregion
    }
}