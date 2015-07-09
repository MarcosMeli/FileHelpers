using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates that this class represents a fixed length record.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class FixedLengthRecordAttribute : TypedRecordAttribute
    {
        /// <summary>Indicates the behavior when variable length records are found.</summary>
        public FixedMode FixedMode { get; private set; }

        /// <summary>Indicates that this class represents a fixed length
        /// record. By default fixed length files require the records to have
        /// equal length.
        /// (ie the record length equals the sum of each field length.
        /// </summary>
        public FixedLengthRecordAttribute()
            : this(FixedMode.ExactLength) {}

        /// <summary>
        /// Indicates that this class represents a fixed length record with the
        /// specified variable length record behavior.
        /// </summary>
        /// <param name="fixedMode">The <see cref="FileHelpers.FixedMode"/> used for variable length records. By Default is FixedMode.ExactLength</param>
        public FixedLengthRecordAttribute(FixedMode fixedMode)
        {
            FixedMode = fixedMode;
        }
    }
}