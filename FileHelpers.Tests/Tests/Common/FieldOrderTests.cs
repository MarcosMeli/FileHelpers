using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class FieldOrderTests
    {
        [Test]
        public void SimpleOrder()
        {
            var engine = new FileHelperEngine<FieldOrderType>();

            Assert.AreEqual(5, engine.Options.FieldCount);
            Assert.AreEqual("Field1", engine.Options.FieldsNames[0]);
            Assert.AreEqual("Field2", engine.Options.FieldsNames[1]);
            Assert.AreEqual("Field3", engine.Options.FieldsNames[2]);
            Assert.AreEqual("Field4", engine.Options.FieldsNames[3]);
            Assert.AreEqual("Field5", engine.Options.FieldsNames[4]);
        }


        [Test]
        public void UsingAttributeToChangeOrder()
        {
            var engine = new FileHelperEngine<FieldOrderTypeSorted>();

            Assert.AreEqual(5, engine.Options.FieldCount);
            Assert.AreEqual("Field2", engine.Options.FieldsNames[0]);
            Assert.AreEqual("Field1", engine.Options.FieldsNames[1]);
            Assert.AreEqual("Field5", engine.Options.FieldsNames[2]);
            Assert.AreEqual("Field4", engine.Options.FieldsNames[3]);
            Assert.AreEqual("Field3", engine.Options.FieldsNames[4]);
        }

        [DelimitedRecord("\t")]
        public class FieldOrderType
        {
            public int Field1;
            public int Field2;
            public string Field3;
            public int Field4;
            public DateTime Field5;
        }


        [DelimitedRecord("\t")]
        public class FieldOrderTypeSorted
        {
            [FieldOrder(-5)]
            public int Field1;

            [FieldOrder(-10)]
            public int Field2;

            [FieldOrder(10)]
            public string Field3;

            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(1)]
            public DateTime Field5;
        }


        [Test]
        public void FieldOrderWithSameNumber1()
        {
            Assert.Throws<BadUsageException>
                (
                    () => new FileHelperEngine<FieldOrderSameNumber1>(),
                    "The field: Field5 has the same FieldOrder that: Field3 you must use different values"
                );
        }

        [Test]
        public void FieldOrderWithSameNumber2()
        {
            Assert.Throws<BadUsageException>
                (
                    () => new FileHelperEngine<FieldOrderSameNumber2>(),
                    "The field: Field2 has the same FieldOrder that: Field1 you must use different values"
                );
        }


        [DelimitedRecord("\t")]
        public class FieldOrderSameNumber1
        {
            [FieldOrder(-5)]
            public int Field1;

            [FieldOrder(-10)]
            public int Field2;

            [FieldOrder(10)]
            public string Field3;

            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(10)]
            public DateTime Field5;
        }

        [DelimitedRecord("\t")]
        public class FieldOrderSameNumber2
        {
            [FieldOrder(5)]
            public int Field1;

            [FieldOrder(5)]
            public int Field2;

            [FieldOrder(10)]
            public string Field3;

            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(1)]
            public DateTime Field5;
        }

        [Test]
        public void PartialFieldOrderAppliedMiddle()
        {
            Assert.Throws<BadUsageException>
                (
                    () => new FileHelperEngine<FieldOrderPartialAppliedMiddle>(),
                    "The field: Field3 must be marked with FielOrder because if you use this attribute in one field you must also use it in all."
                );
        }

        [Test]
        public void PartialFieldOrderAppliedLast()
        {
            Assert.Throws<BadUsageException>
                (
                    () => new FileHelperEngine<FieldOrderPartialAppliedLast>(),
                    "The field: Field5 must be marked with FielOrder because if you use this attribute in one field you must also use it in all."
                );
        }


        [Test]
        public void PartialFieldOrderAppliedFirst()
        {
            Assert.Throws<BadUsageException>
                (
                    () => new FileHelperEngine<FieldOrderPartialAppliedFirst>(),
                    "The field: Field1 must be marked with FielOrder because if you use this attribute in one field you must also use it in all."
                );
        }


        [DelimitedRecord("\t")]
        public class FieldOrderPartialAppliedMiddle
        {
            [FieldOrder(4)]
            public int Field1;

            [FieldOrder(1)]
            public int Field2;

            public string Field3;

            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(2)]
            public DateTime Field5;
        }

        [DelimitedRecord("\t")]
        public class FieldOrderPartialAppliedFirst
        {
            public int Field1;

            [FieldOrder(8)]
            public int Field2;

            [FieldOrder(5)]
            public string Field3;

            [FieldOrder(2)]
            public int Field4;

            [FieldOrder(1)]
            public DateTime Field5;
        }

        [DelimitedRecord("\t")]
        public class FieldOrderPartialAppliedLast
        {
            [FieldOrder(1)]
            public int Field1;

            [FieldOrder(2)]
            public int Field2;

            [FieldOrder(5)]
            public string Field3;

            [FieldOrder(4)]
            public int Field4;

            public DateTime Field5;
        }


        [Test]
        public void FieldOptionalPlusFieldOrderWrong1()
        {
            Assert.Throws<BadUsageException>
                (
                    () => new FileHelperEngine<FieldOptionalPlusFieldOrderTypeWrong1>(),
                    ""
                );
        }


        [Test]
        public void FieldOptionalPlusFieldOrderWrong2()
        {
            Assert.Throws<BadUsageException>
                (
                    () => new FileHelperEngine<FieldOptionalPlusFieldOrderTypeWrong2>(),
                    ""
                );
        }

        [DelimitedRecord("\t")]
        public class FieldOptionalPlusFieldOrderTypeWrong1
        {
            [FieldOrder(-5)]
            public int Field1;

            [FieldOrder(-10)]
            public int Field2;

            [FieldOrder(10)]
            public string Field3;

            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(1)]
            [FieldOptional]
            public DateTime Field5;
        }

        [DelimitedRecord("\t")]
        public class FieldOptionalPlusFieldOrderTypeWrong2
        {
            [FieldOrder(-5)]
            public int Field1;

            [FieldOrder(-10)]
            public int Field2;

            [FieldOrder(10)]
            public string Field3;

            [FieldOptional]
            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(1)]
            public DateTime Field5;
        }

        [Test]
        public void FieldOptionalPlusFieldOrderGood1()
        {
            var engine = new FileHelperEngine<FieldOptionalPlusFieldOrderTypeGood1>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);
        }

        [Test]
        public void FieldOptionalPlusFieldOrderGood2()
        {
            var engine = new FileHelperEngine<FieldOptionalPlusFieldOrderTypeGood2>();

            Check.That(engine.Options.FieldCount).IsEqualTo(5);
        }

        [DelimitedRecord("\t")]
        public class FieldOptionalPlusFieldOrderTypeGood1
        {
            [FieldOrder(-5)]
            public int Field1;

            [FieldOrder(-10)]
            public int Field2;

            [FieldOptional]
            [FieldOrder(10)]
            public string Field3;

            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(1)]
            public DateTime Field5;
        }

        [DelimitedRecord("\t")]
        public class FieldOptionalPlusFieldOrderTypeGood2
        {
            [FieldOrder(-5)]
            public int Field1;

            [FieldOrder(-10)]
            public int Field2;

            [FieldOptional]
            [FieldOrder(10)]
            public string Field3;

            [FieldOptional]
            [FieldOrder(5)]
            public int Field4;

            [FieldOrder(1)]
            public DateTime Field5;
        }
    }
}