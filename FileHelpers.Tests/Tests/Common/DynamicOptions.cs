using System.Linq;
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

        [DelimitedRecord("|")]
        public class CustomersVerticalBar2
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;

            public string ContactTitle;
            public string Address;
            public string City;
            public string Country;

            public string ToRemove;

        }

        [DelimitedRecord("|")]
        public class CustomersVerticalBarAutoProperty
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }

            public string ToRemove { get; set; }
            
            public string ContactTitle { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }

        }

        [Test]
        public void RemoveField()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();

            Check.That(engine.Options.FieldCount).IsEqualTo(8);
            engine.Options.RemoveField("ToRemove");
            Check.That(engine.Options.FieldCount).IsEqualTo(7);

            var records = (CustomersVerticalBar[]) FileTest.Good.CustomersVerticalBar.ReadWithEngine(engine);
            Check.That(records.Length).IsEqualTo(91);
        }

        [Test]
        public void RemoveAutoPropertyMultipleFields()
        {
            var engine = new FileHelperEngine<CustomersVerticalBarAutoProperty>();

            Check.That(engine.Options.FieldCount).IsEqualTo(8);
            engine.Options.RemoveField("ToRemove");
            engine.Options.RemoveField("ContactTitle");
            Assert.That(engine.Options.Fields.Cast<FieldBase>().Select(f => f.FieldName), Has.No.Member("ContactTitle"));
        }

        [Test]
        public void RemoveFieldLast()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar2>();

            Check.That(engine.Options.FieldCount).IsEqualTo(8);
            engine.Options.RemoveField("ToRemove");
            Check.That(engine.Options.FieldCount).IsEqualTo(7);

            var records = (CustomersVerticalBar2[]) FileTest.Good.CustomersVerticalBar.ReadWithEngine(engine);
            Check.That(records.Length).IsEqualTo(91);
        }

        [Test]
        public void SetAsOptionalField()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            engine.Options.RemoveField("ToRemove");
            engine.Options.Fields[engine.Options.FieldCount - 1].IsOptional = true;

            var records = (CustomersVerticalBar[]) FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);

            Check.That(records.Length).IsEqualTo(91);
        }

        [Test]
        public void SetAsOptionalFieldLast()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar2>();
            engine.Options.RemoveField("ToRemove");
            engine.Options.Fields[engine.Options.FieldCount - 1].IsOptional = true;

            var records = (CustomersVerticalBar2[]) FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);

            Check.That(records.Length).IsEqualTo(91);
        }

        [Test]
        public void SetTrimMode()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            engine.Options.RemoveField("ToRemove");
            engine.Options.Fields[engine.Options.FieldCount - 1].IsOptional = true;
            engine.Options.Fields[1].TrimMode = TrimMode.Both;

            var records = (CustomersVerticalBar[]) FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);

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

            var records = (CustomersVerticalBar[]) FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);
            Check.That(records[2].CompanyName).IsEqualTo(records[2].CompanyName.Trim());

        }

        [Test]
        public void SetTrimCharLast()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar2>();
            engine.Options.RemoveField("ToRemove");
            engine.Options.Fields[engine.Options.FieldCount - 1].IsOptional = true;

            engine.Options.Fields[1].TrimMode = TrimMode.Both;
            engine.Options.Fields[1].TrimChars = new char[] { '-' };

            var records = (CustomersVerticalBar2[]) FileTest.Good.CustomersVerticalBarOptions.ReadWithEngine(engine);
            Check.That(records[2].CompanyName).IsEqualTo(records[2].CompanyName.Trim());

        }
    }
}