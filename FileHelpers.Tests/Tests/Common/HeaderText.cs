using System;
using System.Collections;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class HeaderText
    {
       
        [Test]
        public void GetFileHeader()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            var records = engine.ReadFile(FileTest.Good.CustomersVerticalBarDemo.Path);

            Check.That(engine.GetFileHeader())
                .IsEqualTo("CustomerID|CompanyName|ContactName|ContactTitle|Address|City|Country");
        }

        [Test]
        public void CheckItWritesToFile()
        {
            var engine = new FileHelperEngine<CustomersVerticalBar>();
            var records = engine.ReadFile(FileTest.Good.CustomersVerticalBarDemo.Path);
            
            engine.HeaderText = engine.GetFileHeader();
            var result = engine.WriteString(records);

            Check.That(result).StartsWith("CustomerID|CompanyName|ContactName|ContactTitle|Address|City|Country");
            Check.That(result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length)
                .IsEqualTo(records.Length + 1);

        }
    }
}