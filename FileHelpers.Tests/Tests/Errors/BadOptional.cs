using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class BadOptional
    {
        [Test]
        public void DelimitedBad1()
        {
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine<OptionalBad1>());
        }

        [Test]
        public void DelimitedBad2()
        {
            var engine = new FileHelperEngine<OptionalBad2>();
            Assert.Throws<NullValueNotFoundException>(()
                => TestCommon.ReadTest<OptionalBad2>(engine, "Bad", "OptionalBad1.txt"));
        }


        [DelimitedRecord("|")]
        private class OptionalBad1
        {
            public string CustomerID;
            public string CompanyName;

            [FieldOptional()]
            public string ContactName;

            public string ContactTitle;
        }

        [DelimitedRecord("|")]
        private class OptionalBad2
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;

            [FieldOptional()]
            public int ContactTitle;
        }
    }
}