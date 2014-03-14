using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class ArrayFields
    {
        [Test]
        public void ArrayFields1()
        {
            var res = FileTest.Good.ArrayFields
                .ReadWithEngine<ArrayType1>();

            SimpleComparer(res);
        }

        [Test]
        public void ArrayFields2()
        {
            var res = FileTest.Good.ArrayFields
                .ReadWithEngine<ArrayType3>();

            SimpleComparer2(res);
        }

        [Test]
        public void ArrayFieldsString()
        {
            var res = FileTest.Good.ArrayFields
                .ReadWithEngine<ArrayTypeStrings>();

            SimpleComparerStrings(res);
        }


        private static void SimpleComparer2(ArrayType3[] res)
        {
            Assert.AreEqual(3, res.Length);

            Assert.AreEqual("58745", res[0].CustomerID);
            Assert.AreEqual("13", res[0].BuyedArts[0]);
            Assert.AreEqual("+8", res[0].BuyedArts[1]);
            Assert.AreEqual("+3", res[0].BuyedArts[2]);
            Assert.AreEqual("-7", res[0].BuyedArts[3]);
            Assert.AreEqual(20, res[0].BuyedArts.Length);

            Assert.AreEqual("31245", res[1].CustomerID);
            Assert.AreEqual("6", res[1].BuyedArts[0]);
            Assert.AreEqual(17, res[1].BuyedArts.Length);

            Assert.AreEqual(" 1245", res[2].CustomerID);
            Assert.AreEqual(0, res[2].BuyedArts.Length);
        }

        private static void SimpleComparer(ArrayType1[] res)
        {
            Assert.AreEqual(3, res.Length);

            Assert.AreEqual(58745, res[0].CustomerID);
            Assert.AreEqual(13, res[0].BuyedArts[0]);
            Assert.AreEqual(8, res[0].BuyedArts[1]);
            Assert.AreEqual(3, res[0].BuyedArts[2]);
            Assert.AreEqual(-7, res[0].BuyedArts[3]);
            Assert.AreEqual(20, res[0].BuyedArts.Length);

            Assert.AreEqual(31245, res[1].CustomerID);
            Assert.AreEqual(6, res[1].BuyedArts[0]);
            Assert.AreEqual(17, res[1].BuyedArts.Length);

            Assert.AreEqual(1245, res[2].CustomerID);
            Assert.AreEqual(0, res[2].BuyedArts.Length);
        }

        [Test]
        public void ArrayFieldsDelimited()
        {
            var res = FileTest.Good.ArrayFieldsDelimited
                .ReadWithEngine<ArrayTypeDelimited>();

            Assert.AreEqual(10, res.Length);
        }

        private static void SimpleComparerStrings(ArrayTypeStrings[] res)
        {
            Assert.AreEqual(3, res.Length);

            Assert.AreEqual(58745, res[0].CustomerID);
            Assert.AreEqual("13", res[0].BuyedArts[0]);
            Assert.AreEqual("+8", res[0].BuyedArts[1]);
            Assert.AreEqual("+3", res[0].BuyedArts[2]);
            Assert.AreEqual("-7", res[0].BuyedArts[3]);
            Assert.AreEqual(20, res[0].BuyedArts.Length);

            Assert.AreEqual(31245, res[1].CustomerID);
            Assert.AreEqual("6", res[1].BuyedArts[0]);
            Assert.AreEqual(17, res[1].BuyedArts.Length);

            Assert.AreEqual(1245, res[2].CustomerID);
            Assert.AreEqual(0, res[2].BuyedArts.Length);
        }

        [Test]
        public void ArrayFieldsRW()
        {
            var engine = new FileHelperEngine<ArrayType1>();
            var res = engine.ReadFile(FileTest.Good.ArrayFields.Path);
            SimpleComparer(res);

            res = engine.ReadString(engine.WriteString(res));

            SimpleComparer(res);
        }


        /// <summary>
        /// TODO:  Implement layout engine to handle this ArrayFieldsComplex test case.
        /// Test a class containing an array of other objects
        /// Objects are delimitted and finite so in theory should be parsable.
        /// </summary>
        [Test]
        [Ignore("Class containing an array in turn containing many fields is not yet supported")]
        public void ArrayFieldsComplex()
        {
            var engine = new FileHelperEngine<ArrayComplexType>();
            var res = engine.ReadString("");
        }

        [Test]
        public void ArrayFieldsBad01()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<ArrayTypeBad1>());
        }

        [Test]
        public void ArrayFieldsBad02()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<ArrayTypeBad2>());
        }

        [Test]
        public void ArrayFieldsBad03()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<ArrayTypeBad3>());
        }

        [Test]
        public void ArrayFieldsBad04()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<ArrayTypeBad4>());
        }

        [Test]
        public void ArrayFieldsBad05()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<ArrayTypeBad5>());
        }

        [Test]
        public void ArrayFieldsBad06()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<ArrayTypeBad6>());
        }

        [Test]
        public void ArrayFieldsBad10()
        {
            var engine = new FileHelperEngine<ArrayType2>();
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            var res = engine.ReadFile(FileTest.Good.ArrayFields2.Path);

            Assert.AreEqual(0, res.Length);
            Assert.AreEqual(2, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(
                "Line: 1 Column: 33 Field: BuyedArts. The array has only 4 values, less than the minimum length of 5",
                engine.ErrorManager.Errors[0].ExceptionInfo.Message);
            Assert.AreEqual(
                "Line: 2 Column: 40 Field: BuyedArts. The array has more values than the maximum length of 5",
                engine.ErrorManager.Errors[1].ExceptionInfo.Message);
        }

        [Test]
        public void ArrayWriteMinErrorNull()
        {
            Assert.Throws<InvalidOperationException>(
                () => {
                    var engine = new DelimitedFileEngine<ArrayModel2To4>();
                    var res =
                        engine.WriteString(new[] {
                            new ArrayModel2To4() {
                                Id = 1,
                                Name = "name1",
                                Weighting = null
                            }
                        });
                });
        }

        [Test]
        public void ArrayWriteMinError0()
        {
            Assert.Throws<InvalidOperationException>(
                () => {
                    var engine = new DelimitedFileEngine<ArrayModel2To4>();
                    var res =
                        engine.WriteString(new[] {
                            new ArrayModel2To4() {
                                Id = 1,
                                Name = "name1",
                                Weighting = new float[] {}
                            }
                        });
                });
        }

        [Test]
        public void ArrayWriteMinError1()
        {
            Assert.Throws<InvalidOperationException>(
                () => {
                    var engine = new DelimitedFileEngine<ArrayModel2To4>();
                    var res =
                        engine.WriteString(new[] {
                            new ArrayModel2To4() {
                                Id = 1,
                                Name = "name1",
                                Weighting = new float[] {10.2f}
                            }
                        });
                });
        }


        [Test]
        public void ArrayWriteMaxError5()
        {
            Assert.Throws<InvalidOperationException>(
                () => {
                    var engine = new DelimitedFileEngine<ArrayModel2To4>();
                    var res =
                        engine.WriteString(new[] {
                            new ArrayModel2To4() {
                                Id = 1,
                                Name = "name1",
                                Weighting = new float[] {10.2f, 1, 2, 3, 4}
                            }
                        });
                });
        }


        [Test]
        public void ArrayWriteFloatFieldsNull()
        {
            var dataToExport = new List<ArrayModel1>();
            dataToExport.Add(new ArrayModel1() {
                Id = 1,
                Name = "name1",
                Weighting = null
            });

            var engine = new DelimitedFileEngine<ArrayModel1>();
            var res = engine.WriteString(dataToExport);

            Assert.AreEqual("1,name1," + Environment.NewLine, res);

            var vals = engine.ReadString(res);
            vals.Length.AssertEqualTo(1);
            vals[0].Weighting.Length.AssertEqualTo(0);
        }


        [Test]
        public void ArrayReadFieldsNull()
        {
            var info = "1,name1,10.2,,30.5";
            var engine = new DelimitedFileEngine<ArrayModel1>();
            var res = engine.ReadString(info);

            res.Length.AssertEqualTo(1);
            res[0].Weighting.Length.AssertEqualTo(3);
            res[0].Weighting[1].AssertEqualTo(-5f);
        }

        [Test]
        public void ArrayReadFieldsNullAndNullable()
        {
            var info = "1,name1,10.2,,30.5";
            var engine = new DelimitedFileEngine<ArrayModelNullable>();
            var res = engine.ReadString(info);

            res.Length.AssertEqualTo(1);
            res[0].Weighting.Length.AssertEqualTo(3);
            res[0].Weighting[1].AssertEqualTo(null);
        }

        [Test]
        public void ArrayWriteFloatFields0()
        {
            var dataToExport = new List<ArrayModel1>();
            dataToExport.Add(new ArrayModel1() {
                Id = 1,
                Name = "name1",
                Weighting = new float[] {}
            });

            var engine = new DelimitedFileEngine<ArrayModel1>();
            var res = engine.WriteString(dataToExport);

            Assert.AreEqual("1,name1," + Environment.NewLine, res);

            var vals = engine.ReadString(res);
            vals.Length.AssertEqualTo(1);
            vals[0].Weighting.Length.AssertEqualTo(0);
        }

        [Test]
        public void ArrayWriteFloatFieldsNullable()
        {
            var dataToExport = new List<ArrayModelNullable>();
            dataToExport.Add(new ArrayModelNullable() {
                Id = 1,
                Name = "name1",
                Weighting = new float?[] {}
            });

            var engine = new DelimitedFileEngine<ArrayModelNullable>();
            var res = engine.WriteString(dataToExport);

            Assert.AreEqual("1,name1," + Environment.NewLine, res);

            var vals = engine.ReadString(res);
            vals.Length.AssertEqualTo(1);
            vals[0].Weighting.Length.AssertEqualTo(0);
        }

        [Test]
        public void ArrayWriteFloatFields1()
        {
            var dataToExport = new List<ArrayModel1>();
            dataToExport.Add(new ArrayModel1() {
                Id = 1,
                Name = "name1",
                Weighting = new float[] {10.2f}
            });

            var engine = new DelimitedFileEngine<ArrayModel1>();
            var res = engine.WriteString(dataToExport);

            Assert.AreEqual("1,name1,10.2" + Environment.NewLine, res);
        }

        [Test]
        public void ArrayWriteFloatFields2()
        {
            var dataToExport = new List<ArrayModel1>();
            dataToExport.Add(new ArrayModel1() {
                Id = 1,
                Name = "name1",
                Weighting = new float[] {10.2f, 30.5f}
            });

            var engine = new DelimitedFileEngine<ArrayModel1>();
            var res = engine.WriteString(dataToExport);

            Assert.AreEqual("1,name1,10.2,30.5" + Environment.NewLine, res);
        }


        [Test]
        public void ArrayWriteFloatFields3()
        {
            var dataToExport = new List<ArrayModel1>();
            dataToExport.Add(new ArrayModel1() {
                Id = 1,
                Name = "name1",
                Weighting = new float[] {10.2f, 30.5f, 11f}
            });

            var engine = new DelimitedFileEngine<ArrayModel1>();
            var res = engine.WriteString(dataToExport);

            Assert.AreEqual("1,name1,10.2,30.5,11" + Environment.NewLine, res);
        }

        [DelimitedRecord(",")]
        public class ArrayModel1
        {
            public int Id;
            public string Name;

            [FieldNullValue(-5f)]
            [FieldArrayLength(0, 15)]
            public float[] Weighting;
        }


        [DelimitedRecord(",")]
        public class ArrayModelNullable
        {
            public int Id;
            public string Name;

            [FieldArrayLength(0, 15)]
            public float?[] Weighting;
        }

        [DelimitedRecord(",")]
        public class ArrayModel2To4
        {
            public int Id;
            public string Name;

            [FieldArrayLength(2, 4)]
            public float[] Weighting;
        }


        [FixedLengthRecord(FixedMode.ExactLength)]
        public class ArrayType1
        {
            [FieldFixedLength(5)]
            public int CustomerID;

            [FieldFixedLength(7)]
            public int[] BuyedArts;
        }

        [FixedLengthRecord(FixedMode.ExactLength)]
        public class ArrayTypeStrings
        {
            [FieldFixedLength(5)]
            public int CustomerID;

            [FieldFixedLength(7)]
            [FieldTrim(TrimMode.Both)]
            public string[] BuyedArts;
        }

        [FixedLengthRecord(FixedMode.ExactLength)]
        public class ArrayType2
        {
            [FieldFixedLength(5)]
            public int CustomerID;

            [FieldFixedLength(7)]
            [FieldArrayLength(5)]
            public int[] BuyedArts;
        }


        [FixedLengthRecord(FixedMode.ExactLength)]
        public class ArrayType3
        {
            [FieldFixedLength(5)]
            public string CustomerID;

            [FieldFixedLength(7)]
            [FieldTrim(TrimMode.Both)]
            public string[] BuyedArts;
        }


        [DelimitedRecord("|")]
        public class ArrayTypeBad1
        {
            [FieldArrayLength(2, 30)]
            public int CustomerID;
        }

        [DelimitedRecord("|")]
        public class ArrayTypeBad2
        {
            public int[][] JaggedArray;
        }


        [DelimitedRecord("|")]
        public class ArrayTypeBad3
        {
            public int[,] TableArray;
        }

        [DelimitedRecord("|")]
        public class ArrayTypeBad4
        {
            [FieldArrayLength(20, 10)]
            public int[] ArrayField;
        }

        [DelimitedRecord("|")]
        public class ArrayTypeBad5
        {
            public int[] Customers;
            public int CustomerID;
        }

        [DelimitedRecord("|")]
        public class ArrayTypeBad6
        {
            [FieldArrayLength(20, 10)]
            public int[] ArrayField;

            public int CustomerID;
        }
    }

    [DelimitedRecord("\t")]
    public class ArrayTypeDelimited
    {
        public string[] Values;
    }


    public class ArrayComplexSubClass
    {
        public string A;
        public string B;
        public DateTime C;
    }

    [DelimitedRecord("\t")]
    public class ArrayComplexType
    {
        public string someOtherInfo;
        public string maybeAnotherThing;

        [FieldArrayLength(10)]
        public ArrayComplexSubClass[] internalClasses;
    }
}