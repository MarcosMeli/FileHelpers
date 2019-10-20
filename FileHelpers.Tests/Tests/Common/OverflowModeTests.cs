using System;
using System.Text;
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

        [Test]
        public void Discard()
        {
            var engine = new FixedFileEngine<DiscardCustomer>();
            var customer = new DiscardCustomer {mCustomerID = "0123456789ABC"};
            var res = engine.WriteString(new[] {customer});

            Check.That(res).IsEqualTo($"0123456789{Environment.NewLine}");
        }

        [Test]
        public void Error()
        {
            var engine = new FixedFileEngine<ErrorCustomer>();
            var customer = new ErrorCustomer { mCustomerID = "0123456789ABC" };
            Assert.Throws<ConvertException>(() => engine.WriteString(new[] {customer}));
        }
    }
}