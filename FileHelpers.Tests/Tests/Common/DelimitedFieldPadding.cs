using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class DelimitedFieldPadding
    {

        [DelimitedRecord(",")]
        private class AlignCenterSample
        {
            public AlignCenterSample()
            {

            }

            public AlignCenterSample(string name, int amount, DateTime date )
            {
                this.Name = name;
                this.Amount = amount;
                this.Date = date;
            }


            [DelimitedFieldPadding(6, AlignMode.Center, '+')]
            public string Name { get; set; }

            [DelimitedFieldPadding(5, AlignMode.Center, '+')]
            [FieldConverter(ConverterKind.Int32)]
            public int Amount { get; set; }

            [DelimitedFieldPadding(12, AlignMode.Center, '+')]
            [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
            public DateTime Date { get; set; }
        }


        [Test]
        public void TestPaddingCenterAlign()
        {

            List<AlignCenterSample> records = new List<AlignCenterSample>();
            records.Add(new AlignCenterSample("AA", 100, new DateTime(2020, 1, 1)));
            

            var engine = new FileHelperEngine<AlignCenterSample>();
            string output = engine.WriteString(records).TrimEnd();

            string expected = "++AA++,+100+,+2020-01-01+";

            Assert.AreEqual(expected, output);
        }


        [DelimitedRecord(",")]
        private class AlignLeftSample
        {
            public AlignLeftSample()
            {

            }

            public AlignLeftSample(string name, int amount, DateTime date)
            {
                this.Name = name;
                this.Amount = amount;
                this.Date = date;
            }


            [DelimitedFieldPadding(6, AlignMode.Left, ' ')]
            public string Name { get; set; }

            [DelimitedFieldPadding(5, AlignMode.Left, ' ')]
            [FieldConverter(ConverterKind.Int32)]
            public int Amount { get; set; }

            [DelimitedFieldPadding(12, AlignMode.Left, '*')]
            [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
            public DateTime Date { get; set; }
        }


        [Test]
        public void TestPaddingLeftAlign()
        {

            var records = new List<AlignLeftSample>();
            records.Add(new AlignLeftSample("AA", 100, new DateTime(2020, 1, 1)));


            var engine = new FileHelperEngine<AlignLeftSample>();
            string output = engine.WriteString(records).TrimEnd();

            string expected = "AA    ,100  ,2020-01-01**";

            Assert.AreEqual(expected, output);
        }


        [DelimitedRecord(",")]
        private class AlignRightSample
        {
            public AlignRightSample()
            {

            }

            public AlignRightSample(string name, int amount, DateTime date)
            {
                this.Name = name;
                this.Amount = amount;
                this.Date = date;
            }


            [DelimitedFieldPadding(6, AlignMode.Right, ' ')]
            public string Name { get; set; }

            [DelimitedFieldPadding(5, AlignMode.Right, '*')]
            [FieldConverter(ConverterKind.Int32)]
            public int Amount { get; set; }

            [DelimitedFieldPadding(12, AlignMode.Right, '+')]
            [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
            public DateTime Date { get; set; }
        }




        [Test]
        public void TestPaddingRightAlign()
        {

            var records = new List<AlignRightSample>();
            records.Add(new AlignRightSample("AA", 100, new DateTime(2020, 1, 1)));


            var engine = new FileHelperEngine<AlignRightSample>();
            string output = engine.WriteString(records).TrimEnd();

            string expected = "    AA,**100,++2020-01-01";

            Assert.AreEqual(expected, output);
        }


        [FixedLengthRecord]
        private class FixedWithWithDelimitedFiledPaddingSample
        {
            public FixedWithWithDelimitedFiledPaddingSample()
            {

            }

            public FixedWithWithDelimitedFiledPaddingSample(string name, string description)
            {
                this.Name = name;
                this.Description = description;
            }

            [DelimitedFieldPadding(10, AlignMode.Left, ' ')]
            [FieldFixedLength(10)]
            public string Name { get; set; }

            [DelimitedFieldPadding(50, AlignMode.Left, ' ')]
            [FieldFixedLength(50)]
            public string Description { get; set; }


        }




       


        [Test]
        public void DelimitedFieldPaddingInvalidOnFixedWidthFile()
        {

            Assert.Throws<BadUsageException>(() =>
            {
                var engine = new FileHelperEngine<FixedWithWithDelimitedFiledPaddingSample>();
            });

        }
    }
}
