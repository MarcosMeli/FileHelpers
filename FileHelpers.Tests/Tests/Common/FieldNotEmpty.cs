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
        public void FieldValidateIsNotEmpty1()
        {
            Assert.DoesNotThrow(() => FileTest.Good.FieldValidateIsNotEmpty1.ReadWithEngine<NotEmptyType>());
        }
    }

    [DelimitedRecord(",")]
    public class NotEmptyType {
        [FieldValidateIsNotEmpty()]
        public int CustomerID;

        [FieldValidateIsNotEmpty()]
        public string CompanyName;

        public string ContactName;

        [FieldNullValue(true)]
        public bool IsActive;

        [FieldValidateIsNotEmpty()]
        public DateTime? CreatedDate;
    }
}
