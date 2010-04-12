//using MbUnit.Framework;
using NUnit.Framework;
using System.Diagnostics;

namespace System
{
    public static class FluentTestingExtensions
    {

        // String Related

        /// <summary>
        /// Asserts that the string contains the <paramref name="containedString"/> value. (Case Insensitive)
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="containedString">The string to search inside the testTarget</param>
        /// <returns></returns>
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
        /// <param name="actual"></param>
        /// <param name="containedString">The string to search inside the testTarget</param>
        /// <returns></returns>
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
        /// <param name="actual"></param>
        /// <returns></returns>
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
        /// <param name="actual"></param>
        /// <returns></returns>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertIsNotNull<T>(this T actual)
        {
            Assert.IsNotNull(actual);
            return new FluentAnd<T>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertCondition<T>(this T actual, Predicate<T> predicate)
        {
            Assert.IsTrue(predicate.Invoke(actual));
            return new FluentAnd<T>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertConditionIsFalse<T>(this T actual, Predicate<T> predicate)
        {
            Assert.IsFalse(predicate.Invoke(actual));
            return new FluentAnd<T>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<bool> AssertIsTrue(this bool actual)
        {
            Assert.IsTrue(actual);
            return new FluentAnd<bool>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<bool> AssertIsTrue(this bool actual, string message)
        {
            Assert.IsTrue(actual, message);
            return new FluentAnd<bool>(actual);
        }


        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<bool> AssertIsFalse(this bool actual)
        {
            Assert.IsFalse(actual);
            return new FluentAnd<bool>(actual);
        }


        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertEqualTo<T>(this T actual, T comparisonObject)
        {
            Assert.AreEqual(comparisonObject, actual);
            return new FluentAnd<T>(actual);
        }

        public static FluentAnd<byte> AssertEqualTo(this byte actual, byte comparisonObject)
        {
            Assert.AreEqual(comparisonObject, actual);
            return new FluentAnd<byte>(actual);
        }

        public static FluentAnd<short> AssertEqualTo(this short actual, short comparisonObject)
        {
            Assert.AreEqual(comparisonObject, actual);
            return new FluentAnd<short>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertEqualTo<T>(this T actual, T comparisonObject, string msg)
        {
            Assert.AreEqual(comparisonObject, actual, msg);
            return new FluentAnd<T>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertDifferentTo<T>(this T actual, T comparisonObject)
        {
            Assert.AreNotEqual(comparisonObject, actual);
            return new FluentAnd<T>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertSameObjectAs<T>(this T actual, Object comparisonObject)
        {
            Assert.AreEqual(actual, comparisonObject);
            return new FluentAnd<T>(actual);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static FluentAnd<T> AssertDifferentObjectAs<T>(this T actual, Object comparisonObject)
        {
            Assert.AreNotEqual(actual, comparisonObject);
            return new FluentAnd<T>(actual);
        }

        public sealed class FluentAnd<T>
        {
            public T And { get; private set; }

            public FluentAnd(T target)
            {
                this.And = target;
            }
        }
    }

}