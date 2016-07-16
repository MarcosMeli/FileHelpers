using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FileHelpers.Tests.Tests.Common
{
    [TestFixture]
    public class FieldValidate
    {
        [Test]
        public void FieldValidate1()
        {
            Assert.DoesNotThrow(() => FileTest.Good.FieldValidate1.ReadWithEngine<ValidateAttributeWithNullsType>());
        }

        [Test]
        public void FieldValidate2()
        {
            Assert.DoesNotThrow(() => FileTest.Good.FieldValidate2.ReadWithEngine<ValidateAttributeWithoutNullsType>());
        }
    }

    [DelimitedRecord(",")]
    public class ValidateAttributeWithNullsType
    {
        public int CustomerID;
        public string CompanyName;

        [FieldValidateTest(true)]
        public string Status;
    }


    [DelimitedRecord(",")]
    public class ValidateAttributeWithoutNullsType
    {
        public int CustomerID;
        public string CompanyName;

        [FieldValidateTest(false)]
        public string Status;
    }

    public class FieldValidateTestAttribute : FieldValidateAttribute
    {
        private static HashSet<string> valid = new HashSet<string> { "active", "pending", "closed" };

        public FieldValidateTestAttribute(bool ValidateNullValue)
        {
            this.Message = "The value is empty and must be populated.";
            this.ValidateNullValue = ValidateNullValue;
        }

        public override bool Validate(string value)
        {
            return !String.IsNullOrEmpty(value) && valid.Contains(value.ToLower().Trim());
        }
    }
}
