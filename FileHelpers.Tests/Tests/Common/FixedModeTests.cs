using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class FixedModeTests
    {
        public class CustomersBase
        {
            [FieldFixedLength(11)]
            public string mCustomerID;

            [FieldFixedLength(38)]
            public string mCompanyName;

            [FieldFixedLength(22)]
            public string mContactName;

            [FieldFixedLength(38)]
            public string mContactTitle;

            [FieldFixedLength(41)]
            public string mAddress;

            [FieldFixedLength(18)]
            public string mCity;

            [FieldFixedLength(15)]
            public string mCountry;
        }

        public class CustomersBaseLast2Optional
        {
            [FieldFixedLength(11)]
            public string mCustomerID;

            [FieldFixedLength(38)]
            public string mCompanyName;

            [FieldFixedLength(22)]
            public string mContactName;

            [FieldFixedLength(38)]
            public string mContactTitle;

            [FieldFixedLength(41)]
            public string mAddress;

            [FieldOptional]
            [FieldFixedLength(18)]
            public string mCity;

            [FieldOptional]
            [FieldFixedLength(15)]
            public string mCountry;
        }

        [FixedLengthRecord(FixedMode.ExactLength)]
        public class CustomerExact
            : CustomersBase {}

        [FixedLengthRecord(FixedMode.AllowLessChars)]
        public class CustomerLess
            : CustomersBase {}

        [FixedLengthRecord(FixedMode.AllowLessChars)]
        public class CustomerLessLast2Optional
            : CustomersBaseLast2Optional {}

        [FixedLengthRecord(FixedMode.AllowMoreChars)]
        public class CustomerMore
            : CustomersBase {}

        [FixedLengthRecord(FixedMode.AllowVariableLength)]
        public class CustomerVariable
            : CustomersBase {}

        [Test]
        public void ExactLength()
        {
            var engine = new FileHelperEngine<CustomerExact>();
            engine.ErrorMode = ErrorMode.IgnoreAndContinue;

            var res = FileTest.Good.CustomersFixedExact
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedLessChars10Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(81);

            res = FileTest.Good.CustomersFixedMoreChars5Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(86);

            res = FileTest.Good.CustomersFixedMoreVariable12Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(79);
        }

        [Test]
        public void LessChars()
        {
            var engine = new FileHelperEngine<CustomerLess>();
            engine.ErrorMode = ErrorMode.IgnoreAndContinue;

            var res = FileTest.Good.CustomersFixedExact
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedLessChars10Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedMoreChars5Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(86);

            res = FileTest.Good.CustomersFixedMoreVariable12Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(85);
        }


        [Test]
        public void MoreChars()
        {
            var engine = new FileHelperEngine<CustomerMore>();
            engine.ErrorMode = ErrorMode.IgnoreAndContinue;

            var res = FileTest.Good.CustomersFixedExact
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedLessChars10Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(81);

            res = FileTest.Good.CustomersFixedMoreChars5Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedMoreVariable12Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(85);
        }


        [Test]
        public void VariableChars()
        {
            var engine = new FileHelperEngine<CustomerVariable>();
            engine.ErrorMode = ErrorMode.IgnoreAndContinue;

            var res = FileTest.Good.CustomersFixedExact
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedLessChars10Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedMoreChars5Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);

            res = FileTest.Good.CustomersFixedMoreVariable12Records
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(91);
        }

        [Test]
        public void WithOutLastField()
        {
            var engine = new FileHelperEngine<CustomerLess>();

            Assert.Throws<BadUsageException>(() =>
                FileTest.Good.CustomersFixedWithoutLastField
                    .ReadWithEngine(engine));
        }

        [Test]
        public void WithOutLastTwoField()
        {
            var engine = new FileHelperEngine<CustomerLess>();

            Assert.Throws<BadUsageException>(() =>
                FileTest.Good.CustomersFixedWithout2Fields
                    .ReadWithEngine(engine));
        }

        [Test]
        public void WithOut1AndHaldFields()
        {
            var engine = new FileHelperEngine<CustomerLess>();

            Assert.Throws<BadUsageException>(() =>
                FileTest.Good.CustomersFixedWithout1AndHalfFields
                    .ReadWithEngine(engine));
        }

        [Test]
        public void WithOutLastFieldWithOptional()
        {
            var engine = new FileHelperEngine<CustomerLessLast2Optional>();

            var res = FileTest.Good.CustomersFixedWithoutLastField
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(2);
        }

        [Test]
        public void WithOutLastTwoFieldWithOptional()
        {
            var engine = new FileHelperEngine<CustomerLessLast2Optional>();

            var res = FileTest.Good.CustomersFixedWithout2Fields
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(2);
        }

        [Test]
        public void WithOut1AndHaldFieldsWithOptional()
        {
            var engine = new FileHelperEngine<CustomerLessLast2Optional>();

            var res = FileTest.Good.CustomersFixedWithout1AndHalfFields
                .ReadWithEngine(engine);

            res.Length.AssertEqualTo(2);
        }
    }
}