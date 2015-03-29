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
            Assert.DoesNotThrow(() => FileTest.Good.FieldValidate1.ReadWithEngine<ValidateAttributeType>());
        }
    }

    [DelimitedRecord(",")]
    public class ValidateAttributeType
    {
        public int CustomerID;
        public string CompanyName;

        [FieldValidateTest()]
        public string Status;
    }

    public class FieldValidateTestAttribute : FieldValidateAttribute
    {
        private static HashSet<string> valid = new HashSet<string> { "active", "pending", "closed" };

        public FieldValidateTestAttribute()
            : base(message:"The value is empty and must be populated.")
        {
        }

        public override bool Validate(string value)
        {
            return !String.IsNullOrEmpty(value) && valid.Contains(value.ToLower().Trim());
        }  
    }
}
