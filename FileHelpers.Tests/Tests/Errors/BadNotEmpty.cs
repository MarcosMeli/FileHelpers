using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Tests.CommonTests;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class BadNotEmpty
    {
        [Test]
        public void FieldValidateIsNotEmpty1()
        {
            Assert.Throws<ConvertException>(() => FileTest.Bad.FieldValidateIsNotEmpty1.ReadWithEngine<NotEmptyType>());
        }

        [Test]
        public void FieldValidateIsNotEmpty2()
        {
            Assert.Throws<ConvertException>(() => FileTest.Bad.FieldValidateIsNotEmpty2.ReadWithEngine<NotEmptyType>());
        }

        [Test]
        public void FieldValidateIsNotEmpty3()
        {
            Assert.Throws<ConvertException>(() => FileTest.Bad.FieldValidateIsNotEmpty3.ReadWithEngine<NotEmptyType>());
        }
    }
}