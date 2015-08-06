using System;
using System.Collections;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class DynamicOptions
    {

        [DelimitedRecord("|")]
        public class CustomersVerticalBar
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;

            public string ToRemove;
            
            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;
            
        }

        [Test]
        public void RemoveField()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();

            Check.That(engine.Options.FieldCount).IsEqualTo(8);
            engine.Options.RemoveField("ToRemove");
            Check.That(engine.Options.FieldCount).IsEqualTo(7);

            var records = FileTest.Good.CustomersVerticalBar.ReadWithEngine(engine);
            Check.That(records.Length).IsEqualTo(91);
        }


        [Test]
        public void SetAsOptionalField()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            engine.Options.RemoveField("ToRemove");
            engine.Options.Fields[engine.Options.FieldCount - 1].IsOptional = true;

            var records = FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);

            Check.That(records.Length).IsEqualTo(91);
        }

        [Test]
        public void SetTrimMode()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            engine.Options.RemoveField("ToRemove");
            engine.Options.Fields[engine.Options.FieldCount - 1].IsOptional = true;
            engine.Options.Fields[1].TrimMode = TrimMode.Both;

            var records = FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);

            Check.That(records[0].CompanyName).IsEqualTo(records[0].CompanyName.Trim());
            Check.That(records[1].CompanyName).IsEqualTo(records[1].CompanyName.Trim());
            

        }

        [Test]
        public void SetTrimChar()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            engine.Options.RemoveField("ToRemove");
            engine.Options.Fields[engine.Options.FieldCount - 1].IsOptional = true;

            engine.Options.Fields[1].TrimMode = TrimMode.Both;
            engine.Options.Fields[1].TrimChars = new char[] { '-' };

            var records = FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);
            Check.That(records[2].CompanyName).IsEqualTo(records[2].CompanyName.Trim());

        }
    }
}