using System;

namespace FileHelpers
{
	/// <summary>
    /// Indicates that the target field might be on the source file.
    /// If it is not present then the value will be null (TODO: Check null)
    /// This attribute is used for read.
    /// </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldOptionalAttribute : Attribute
	{
	}
}