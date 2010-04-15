

using System;

namespace FileHelpers
{

    /// <summary>Allow to declaratively set what records must be included or excluded while reading.</summary>
	/// <remarks>See the <a href="attributes.html">Complete attributes list</a> for more information and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes list</seealso>
	/// <seealso href="quick_start.html">Quick start guide</seealso>
	/// <seealso href="examples.html">Examples of use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConditionalRecordAttribute : Attribute
	{
        /// <summary> The condition used to include or exclude each record </summary>
        public RecordCondition Condition { get; private set; }
        /// <summary> The selector for the condition. </summary>
        public string ConditionSelector { get; private set; }

        /// <summary>Allow to declaratively show what records must be included or excluded</summary>
		/// <param name="condition">The condition used to include or exclude each record</param>
        /// <param name="conditionSelector">The selector for the condition.</param>
		public ConditionalRecordAttribute(RecordCondition condition, string conditionSelector)
		{
            Condition = condition;
            ConditionSelector = conditionSelector;
            ExHelper.CheckNullOrEmpty(conditionSelector, "conditionSelector");

		}

	}

}
