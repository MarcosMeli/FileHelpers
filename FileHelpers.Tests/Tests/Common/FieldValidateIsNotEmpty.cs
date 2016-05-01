using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class FieldValidateIsNotEmpty
    {
        [Test]
        public void FieldValidateIsNotEmpty1()
        {
            Assert.DoesNotThrow(() => FileTest.Good.FieldValidateIsNotEmpty1.ReadWithEngine<IsNotEmptyType>());
        }
    }

    [DelimitedRecord(",")]
    public class IsNotEmptyType 
    {
        [FieldValidateIsNotEmpty]
        public int CustomerID;

        [FieldValidateIsNotEmpty]
        public string CompanyName;

        public string ContactName;

        [FieldNullValue(true)]
        public bool IsActive;

        [FieldValidateIsNotEmpty]
        public DateTime? CreatedDate;
    }
}
