using System;
using FileHelpers.Enums;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class OverflowModeTests
    {
        [FixedLengthRecord]
        public class DiscardCustomer
        {
            [FieldFixedLength(10, OverflowMode = OverflowMode.DiscardEnd)]
            public string mCustomerID;
        }

        [FixedLengthRecord]
        public class ErrorCustomer
        {
            [FieldFixedLength(10, OverflowMode = OverflowMode.Error)]
            public string mCustomerID;
        }

        [TestCase("0123456789ABC", "0123456789")]
        [TestCase("0123456789A", "0123456789")]
        [TestCase("0123456789", "0123456789")]
        [TestCase("012345678", "012345678 ")]
        public void Discard(string originalValue, string expectedValue)
        {
            var engine = new FixedFileEngine<DiscardCustomer>();
            var customer = new DiscardCustomer {mCustomerID = originalValue};
            var res = engine.WriteString(new[] {customer});

            Check.That(res).IsEqualTo($"{expectedValue}{Environment.NewLine}");
        }

        [TestCase("0123456789ABC")]
        [TestCase("0123456789A")]
        public void ErrorWithTooLongValue(string value)
        {
            var engine = new FixedFileEngine<ErrorCustomer>();
            var customer = new ErrorCustomer { mCustomerID = value };
            Assert.Throws<ConvertException>(() => engine.WriteString(new[] {customer}));
        }

        [TestCase("0123456789", "0123456789")]
        [TestCase("012345678", "012345678 ")]
        public void ErrorWithShortEnoughValue(string originalValue, string expectedValue)
        {
            var engine = new FixedFileEngine<ErrorCustomer>();
            var customer = new ErrorCustomer { mCustomerID = originalValue };
            var res = engine.WriteString(new[] {customer});
            Check.That(res).IsEqualTo($"{expectedValue}{Environment.NewLine}");
        }
    }
}