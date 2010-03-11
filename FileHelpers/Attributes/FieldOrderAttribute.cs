

using System;

namespace FileHelpers
{
	/// <summary>
    /// Indicates the relative order of the current field.
    /// Note: If you use this property for one field you must to use it for all.
    /// </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldOrderAttribute : Attribute
	{
	    internal int Order { get; set;}

        /// <summary>
        /// Indicates the relative order of the current field.
        /// Note:If you use this property for one field you must to use it for all.
        /// </summary>
		/// <param name="order">Indicates the relative order of the current field</param>
		public FieldOrderAttribute(int order) 
		{
            Order = order;
		}

	}
}