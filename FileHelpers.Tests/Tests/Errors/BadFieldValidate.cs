using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FileHelpers.Tests.Tests.Common
{
    [TestFixture]
    public class BadFieldValidate
    {
        [Test]
        public void BadFieldValidate1()
        {
            Assert.Throws<ConvertException>(() => FileTest.Bad.FieldValidate1.ReadWithEngine<ValidateAttributeWithNullsType>());
        }

        [Test]
        public void BadFieldValidate2()
        {
            Assert.Throws<ConvertException>(() => FileTest.Bad.FieldValidate2.ReadWithEngine<ValidateAttributeWithNullsType>());
        }
    }
}
