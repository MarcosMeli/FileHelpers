//using MbUnit.Framework;
using NUnit.Framework;
using System.Diagnostics;

namespace System
{
    /// <summary>
    /// Fluent Asserts that allow chaining of "And" between tests
    /// </summary>
    public static class FluentTestingExtensions
    {

        // String Related

        /// <summary>
        /// Asserts that the string contains the <paramref name="containedString"/> value. (Case Insensitive)
        /// </summary>
        /// <param name="actual">String to test</param>
        /// <param name="containedString">The string to search inside the testTarget</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<string> AssertContains(this string actual, string containedString)
        {
            Assert.IsTrue(actual.Contains(containedString));
            return new FluentAnd<string>(actual);
        }

        /// <summary>
        /// Checks that the string contains the <paramref name="containedString"/> value. (Case Sensitive)
        /// </summary>
        /// <param name="actual">String to test</param>
        /// <param name="containedString">The string to search inside the testTarget</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<string> AssertContainsCaseSensitive(this string actual, string containedString)
        {
            Assert.IsTrue(actual.Contains(containedString));
            return new FluentAnd<string>(actual);
        }

        /// <summary>
        /// Checks that the string is string.Empty (if null it fails)
        /// </summary>
        /// <param name="actual">String to test</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<string> AssertIsEmpty(this string actual)
        {
            if (actual != string.Empty)
                Assert.Fail("Expecting empty string but was: " + actual);

            return new FluentAnd<string>(actual);
        }

        /// <summary>
        /// Checks that the string is null or string.Empty
        /// </summary>
        /// <param name="actual">String to test</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<string> AssertIsNullOrEmpty(this string actual)
        {
            Assert.IsTrue(string.IsNullOrEmpty(actual));
            return new FluentAnd<string>(actual);
        }

        /// <summary>
        /// Checks that value is null
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertIsNull<T>(this T actual)
        {
            Assert.IsNull(actual);
            return new FluentAnd<T>(actual);
        }

        /// <summary>
        /// Checks that value is not null
        /// </summary>
        /// <typeparam name="T">Type we are checking for Null</typeparam>
        /// <param name="actual">Value to test</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertIsNotNull<T>(this T actual)
        {
            Assert.IsNotNull(actual);
            return new FluentAnd<T>(actual);
        }

        /// <summary>
        /// Test a value with a predicate
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <param name="predicate">Predicate we are testing</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertCondition<T>(this T actual, Predicate<T> predicate)
        {
            Assert.IsTrue(predicate.Invoke(actual));
            return new FluentAnd<T>(actual);
        }

        /// <summary>
        /// Test a predicate is false
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <param name="predicate">Predicate we are testing</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertConditionIsFalse<T>(this T actual, Predicate<T> predicate)
        {
            Assert.IsFalse(predicate.Invoke(actual));
            return new FluentAnd<T>(actual);
        }
        /// <summary>
        /// Assert the boolean is true
        /// </summary>
        /// <param name="actual">Value to test</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<bool> AssertIsTrue(this bool actual)
        {
            Assert.IsTrue(actual);
            return new FluentAnd<bool>(actual);
        }

        /// <summary>
        /// Assert boolean is true and display message on failure
        /// </summary>
        /// <param name="actual">Value to test</param>
        /// <param name="message">Message to display on error</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<bool> AssertIsTrue(this bool actual, string message)
        {
            Assert.IsTrue(actual, message);
            return new FluentAnd<bool>(actual);
        }

        /// <summary>
        /// Assert value is false
        /// </summary>
        /// <param name="actual">Value to test</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<bool> AssertIsFalse(this bool actual)
        {
            Assert.IsFalse(actual);
            return new FluentAnd<bool>(actual);
        }

        /// <summary>
        /// Assert two values are true
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <param name="comparisonObject">What we are comparing to</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertEqualTo<T>(this T actual, T comparisonObject)
        {
            Assert.AreEqual(comparisonObject, actual);
            return new FluentAnd<T>(actual);
        }

        /// <summary>
        /// Assert two bytes are equal
        /// </summary>
        /// <param name="actual">Value to test</param>
        /// <param name="comparisonObject">What we are comparing to</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<byte> AssertEqualTo(this byte actual, byte comparisonObject)
        {
            Assert.AreEqual(comparisonObject, actual);
            return new FluentAnd<byte>(actual);
        }

        /// <summary>
        /// Assert two shorts are equal
        /// </summary>
        /// <param name="actual">Value to test</param>
        /// <param name="comparisonObject">What we are comparing to</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<short> AssertEqualTo(this short actual, short comparisonObject)
        {
            Assert.AreEqual(comparisonObject, actual);
            return new FluentAnd<short>(actual);
        }

        /// <summary>
        /// Assert two values are equal, display message on failure
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <param name="comparisonObject">What we are comparing to</param>
        /// <param name="msg">Message to display on error</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertEqualTo<T>(this T actual, T comparisonObject, string msg)
        {
            Assert.AreEqual(comparisonObject, actual, msg);
            return new FluentAnd<T>(actual);
        }

        /// <summary>
        /// Assert two values are different
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <param name="comparisonObject">What we are comparing to</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertDifferentTo<T>(this T actual, T comparisonObject)
        {
            Assert.AreNotEqual(comparisonObject, actual);
            return new FluentAnd<T>(actual);
        }
        /// <summary>
        /// Assert two values are the same (Equal)
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <param name="comparisonObject">What we are comparing to</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertSameObjectAs<T>(this T actual, Object comparisonObject)
        {
            Assert.AreEqual(actual, comparisonObject);
            return new FluentAnd<T>(actual);
        }

        /// <summary>
        /// Assert that the new objects are not the same object
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        /// <param name="actual">Value to test</param>
        /// <param name="comparisonObject">What we are comparing to</param>
        /// <returns>And condition</returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertDifferentObjectAs<T>(this T actual, Object comparisonObject)
        {
            Assert.AreNotEqual(actual, comparisonObject);
            return new FluentAnd<T>(actual);
        }

        /// <summary>
        /// And condition allows chaining of tests
        /// </summary>
        /// <typeparam name="T">type we are passing eg string</typeparam>
        public sealed class FluentAnd<T>
        {
            /// <summary>
            /// Value we are passing through, shows as And in test.
            /// </summary>
            public T And { get; private set; }

            /// <summary>
            /// Create a Fluent And condition
            /// </summary>
            /// <param name="target">Value to pass through</param>
            [DebuggerStepThrough]
            [DebuggerHidden]
            public FluentAnd(T target)
            {
                this.And = target;
            }
        }
    }
}