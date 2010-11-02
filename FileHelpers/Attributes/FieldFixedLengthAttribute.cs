

using System;

namespace FileHelpers
{
	/// <summary>Indicates the length of a FixedLength field.</summary>
	/// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldFixedLengthAttribute : FieldAttribute
	{
		internal int Length;

		/// <summary>Indicates the length of a Fixed Length field.</summary>
		/// <param name="length">The length of the field.</param>
		public FieldFixedLengthAttribute(int length)
		{
			if (length > 0)
				this.Length = length;
			else
				throw new BadUsageException("The FieldFixedLength attribute must be > 0");
		}

	}
}