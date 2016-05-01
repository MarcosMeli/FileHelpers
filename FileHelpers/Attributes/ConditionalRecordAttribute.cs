using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Allow to declaratively set what records must be included or excluded while reading.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">Complete attributes list</a> for more information and examples of each one.</remarks>

    /// <example>
    /// [DelimitedRecord(",")] 
    /// [ConditionalRecord(RecordCondition.ExcludeIfBegins, "//")] 
    /// public class ConditionalType1 
    /// { 
    /// 
    /// // Using Regular Expressions example
    /// [DelimitedRecord(",")]
    /// [ConditionalRecord(RecordCondition.IncludeIfMatchRegex, ".*abc??")]
    /// public class ConditionalType3
    /// { 
    /// </example>
    [AttributeUsage(AttributeTargets.Class)]
	//[Obsolete("This attribute will be removed in next version, is better to use INotifyRead and provide code for filtering")]
    public sealed class ConditionalRecordAttribute : Attribute
    {
        /// <summary> The condition used to include or exclude each record </summary>
        public RecordCondition Condition { get; private set; }

        /// <summary> The selector (match string) for the condition. </summary>
        /// <remarks>The string will have a condition, included, excluded start with etc</remarks>
        public string ConditionSelector { get; private set; }

        /// <summary>Allow to declaratively show what records must be included or excluded</summary>
        /// <param name="condition">The condition used to include or exclude each record <see cref="RecordCondition"/>conditions</param>
        /// <param name="conditionSelector">The selector (match string) for the condition.</param>
        public ConditionalRecordAttribute(RecordCondition condition, string conditionSelector)
        {
            Condition = condition;
            ConditionSelector = conditionSelector;
            ExHelper.CheckNullOrEmpty(conditionSelector, "conditionSelector");
        }
    }
}