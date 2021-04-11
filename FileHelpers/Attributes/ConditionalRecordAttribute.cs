using System;

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
            CheckNullOrEmpty(conditionSelector, "conditionSelector");
        }

        /// <summary>
        /// Check the string is null or empty and throw an exception
        /// </summary>
        /// <param name="val">value to test</param>
        /// <param name="paramName">name of parameter to check</param>
        private static void CheckNullOrEmpty(string val, string paramName)
        {
            if (string.IsNullOrEmpty(val))
                throw new ArgumentNullException(paramName, "Value can't be null or empty");
        }
    }
}