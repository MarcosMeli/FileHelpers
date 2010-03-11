

using System;

namespace FileHelpers
{
	/// <summary>
    /// Indicates the target field has a new line before this value 
    /// i.e. indicates that the records have multiple lines, 
    /// and this field is in the beginning of a line.
    /// </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldInNewLineAttribute: Attribute
	{
        /// <summary>
        /// Indicates the target field has a new line before this value 
        /// i.e. indicates that the records have multiple lines, 
        /// and this field is in the beginning of a line.
        /// </summary>
		public FieldInNewLineAttribute()
		{}
	}
}