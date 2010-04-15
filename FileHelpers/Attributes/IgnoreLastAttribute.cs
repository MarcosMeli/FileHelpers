

using System;

namespace FileHelpers
{
	/// <summary>Indicates the number of lines to be discarded at the end.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreLastAttribute : Attribute
	{
        /// <summary> The number of lines to be discarded at end. </summary>
	    public int NumberOfLines { get; private set; }

	    /// <summary>Indicates that the last line must be discarded.</summary>
		public IgnoreLastAttribute() : this(1)
		{
		}

		/// <summary>Indicates the number of last lines to be ignored at the end.</summary>
		/// <param name="numberOfLines">The number of lines to be discarded at end.</param>
		public IgnoreLastAttribute(int numberOfLines)
		{
			NumberOfLines = numberOfLines;
		}

	 
	}
}