

using System;

namespace FileHelpers
{
	/// <summary>Indicates that the target field is completely ignored by the Engine 
    ///     i.e If a field is marked with this attribute , the Engine will not look for this 
    ///     Field in the File.
    ///  </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldIgnoredAttribute : FieldAttribute
	{
		/// <summary>
        /// Indicates that the target field is ignored by the Engine
        /// AND IS NOT IN THE FILE.
        /// </summary>
		public FieldIgnoredAttribute()
		{}
	}
}