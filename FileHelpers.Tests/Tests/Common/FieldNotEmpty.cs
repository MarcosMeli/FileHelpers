using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class FieldNotEmpty
    {
        [Test]
        public void FieldNotEmpty1()
        {
            Assert.DoesNotThrow(() => FileTest.Good.FieldNotEmpty1.ReadWithEngine<NotEmptyType>());
        }
    }

    [DelimitedRecord(",")]
    public class NotEmptyType {
        [FieldNotEmpty()]
        public int CustomerID;

        [FieldNotEmpty()]
        public string CompanyName;

        public string ContactName;

        [FieldNullValue(true)]
        public bool IsActive;

        [FieldNotEmpty()]
        public DateTime? CreatedDate;
    }
}
