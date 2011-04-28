using System;

namespace FileHelpers
{
	/// <summary>Fields inherited from base classes will be ignored.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreInheritedClassAttribute : Attribute
	{
		/// <summary>Fields inherited from base classes will be ignored.</summary>
		public IgnoreInheritedClassAttribute()
		{}
	}
}
