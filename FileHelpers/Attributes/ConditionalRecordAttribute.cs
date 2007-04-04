#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{

	/// <summary>Allow to declarative set what records must be included or excluded when reading.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConditionalRecordAttribute : Attribute
	{
		internal RecordCondition mCondition;
		internal string mConditionSelector;

		/// <summary>Allow to declarative show what records must be included or excluded</summary>
		/// <param name="condition">The condition used to include or exclude each record</param>
		/// <param name="selector">The selector for the condition.</param>
		public ConditionalRecordAttribute(RecordCondition condition, string selector)
		{
			if (selector == null ||  selector.Length == 0)
				throw new BadUsageException("The selector arg for the ConditionalRecordAttribute can't be null or empty.");
			
			mCondition = condition;
			mConditionSelector = selector;
		}

	}

}
