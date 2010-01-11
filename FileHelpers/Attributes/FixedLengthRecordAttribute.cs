using System;

namespace FileHelpers
{
	/// <summary>Indicates that this class represents a fixed length record.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FixedLengthRecordAttribute : TypedRecordAttribute
	{
        /// <summary>Indicates the behavior when variable length records are found.</summary>
	    public FixedMode FixedMode { get; private set; }

	    /// <summary>Indicates that this class represents a fixed length record. By default requieres that the records has the length equals to the sum of each field length.</summary>
		public FixedLengthRecordAttribute()
            :this(FixedMode.ExactLength)
		{}

		/// <summary>Indicates that this class represents a fixed length record with the specified variable record behavior.</summary>
		/// <param name="fixedMode">The <see cref="FileHelpers.FixedMode"/> used for variable length records. By Default is FixedMode.ExactLenght</param>
		public FixedLengthRecordAttribute(FixedMode fixedMode)
		{
		    FixedMode = fixedMode;
		}
	}
}