

using System;

namespace FileHelpers
{
	/// <summary>Indicates the value to assign to the field in the case of a NULL value.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldNullValueAttribute : Attribute
	{
		internal object NullValue;
//		internal bool NullValueOnWrite = false;

		
		/// <summary>
        /// Indicates directly the null value.
        /// </summary>
        /// <param name="nullValue">The value to assign the case of a NULL value.</param>
		public FieldNullValueAttribute(object nullValue)
		{
			NullValue = nullValue;
//			NullValueOnWrite = useOnWrite;
		}

//		/// <summary>Indicates directly the null value.</summary>
//		/// <param name="nullValue">The value to assign in the "NULL" case.</param>
//		public FieldNullValueAttribute(object nullValue): this(nullValue, false)
//		{}

//		/// <summary>Indicates a type and a string to be converted to that type.</summary>
//		/// <param name="type">The type of the null value.</param>
//		/// <param name="nullValue">The string to be converted to the specified type.</param>
//		/// <param name="useOnWrite">Indicates that if the field has that value when the library writes, then the engine use an empty string.</param>
//		public FieldNullValueAttribute(Type type, string nullValue, bool useOnWrite):this(Convert.ChangeType(nullValue, type, null), useOnWrite)
//		{}

		/// <summary>Indicates a type and a string to be converted to that type.</summary>
		/// <param name="type">The type of the null value.</param>
		/// <param name="nullValue">The string to be converted to the specified type.</param>
		public FieldNullValueAttribute(Type type, string nullValue):this(Convert.ChangeType(nullValue, type, null))
		{}

	}
}