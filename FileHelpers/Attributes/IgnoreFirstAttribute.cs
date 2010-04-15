

using System;

namespace FileHelpers
{
	/// <summary>Indicates the number of first lines to be discarded.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreFirstAttribute : Attribute
	{
        /// <summary>The number of first lines to be discarded.</summary>
        public int NumberOfLines { get; private set; }



		/// <summary>Indicates that the first line must be discarded.</summary>
		public IgnoreFirstAttribute() : this(1)
		{
		}

		/// <summary>Indicates the number of first lines to be ignored.</summary>
		/// <param name="numberOfLines">The number of first lines to be discarded.</param>
		public IgnoreFirstAttribute(int numberOfLines)
		{
			NumberOfLines = numberOfLines;
		}
	}
}