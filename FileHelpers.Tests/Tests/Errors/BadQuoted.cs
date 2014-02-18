using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Tests.CommonTests;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class BadQuoted
    {
        [Test]
        public void BadQuoted1()
        {
            Assert.Throws<BadUsageException>(()
                => FileTest.Bad.BadQuoted1.ReadWithEngine<CustomersQuotedType>());
        }

        [Test]
        public void BadQuoted2()
        {
            Assert.Throws<BadUsageException>(()
                => FileTest.Bad.BadQuoted2.ReadWithEngine<CustomersQuotedType>());
        }

        [Test]
        public void BadQuoted3()
        {
            Assert.Throws<BadUsageException>(()
                => FileTest.Bad.BadQuoted3.ReadWithEngine<CustomersQuotedType>());
        }

        [Test]
        public void BadQuoted1Async()
        {
            Assert.Throws<BadUsageException>(()
                => FileTest.Bad.BadQuoted1.ReadWithAsyncEngine<CustomersQuotedType>());
        }

        [Test]
        public void BadQuoted2Async()
        {
            Assert.Throws<BadUsageException>(()
                => FileTest.Bad.BadQuoted2.ReadWithAsyncEngine<CustomersQuotedType>());
        }

        [Test]
        public void BadQuoted3Async()
        {
            Assert.Throws<BadUsageException>(()
                => FileTest.Bad.BadQuoted3.ReadWithAsyncEngine<CustomersQuotedType>());
        }

        [Test]
        public void BadNoQuotes()
        {
            Assert.Throws<BadUsageException>(()
                => FileTest.Bad.BadQuoted1.ReadWithEngine<CustomersNoQuotedType>());
        }
    }
}