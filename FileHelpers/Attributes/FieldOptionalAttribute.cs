

using System;

namespace FileHelpers
{
	/// <summary>Indicates that the target field is included only under some circunstances.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldOptionalAttribute : Attribute
	{
		#region "  Constructors  "

		/// <summary>Indicates that the target field is included only under some circunstances.</summary>
		public FieldOptionalAttribute()
		{
		}

		#endregion
	}
}