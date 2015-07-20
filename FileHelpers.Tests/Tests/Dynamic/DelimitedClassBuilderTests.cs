using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests.Dynamic
{
    [TestFixture]
    public class DelimitedClassBuilderTests
    {
        private FileHelperEngine mEngine;

        [Test]
		[Category("Dynamic")]
        public void FullClassBuilding()
        {
            var cb = new DelimitedClassBuilder("Customers", ",");
            cb.IgnoreFirstLines = 1;
            cb.IgnoreEmptyLines = true;

            cb.AddField("Field1", typeof(DateTime));
            cb.LastField.TrimMode = TrimMode.Both;
            cb.LastField.QuoteMode = QuoteMode.AlwaysQuoted;
            cb.LastField.FieldNullValue = DateTime.Today;

            cb.AddField("Field2", typeof(string));
            cb.LastField.FieldQuoted = true;
            cb.LastField.QuoteChar = '"';

            cb.AddField("Field3", typeof(int));

            mEngine = new FileHelperEngine(cb.CreateRecordClass());

            DataTable dt = mEngine.ReadFileAsDT(TestCommon.GetPath("Good", "Test2.txt"));

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, mEngine.TotalRecords);
            Assert.AreEqual(0, mEngine.ErrorManager.ErrorCount);

            Assert.AreEqual("Field1", dt.Columns[0].ColumnName);
            Assert.AreEqual("Field2", dt.Columns[1].ColumnName);
            Assert.AreEqual("Field3", dt.Columns[2].ColumnName);

            Assert.AreEqual("Hola", dt.Rows[0][1]);
            Assert.AreEqual(DateTime.Today, dt.Rows[2][0]);
        }

        
        [Test]
		[Category("Dynamic")]
		public void TestingNameAndTypes()
        {
            var cb = new DelimitedClassBuilder("Customers", ",");
            cb.IgnoreFirstLines = 1;
            cb.IgnoreEmptyLines = true;

            cb.AddField("Field1", typeof (DateTime));
            cb.LastField.TrimMode = TrimMode.Both;
            cb.LastField.QuoteMode = QuoteMode.AlwaysQuoted;
            cb.LastField.FieldNullValue = DateTime.Today;

            cb.AddField("Field2", typeof (string));
            cb.LastField.FieldQuoted = true;
            cb.LastField.QuoteChar = '"';

            cb.AddField("Field3", typeof (int));

            mEngine = new FileHelperEngine(cb.CreateRecordClass());

            DataTable dt = mEngine.ReadFileAsDT(TestCommon.GetPath("Good", "Test2.txt"));

            Assert.AreEqual("Field1", dt.Columns[0].ColumnName);
            Assert.AreEqual(typeof (DateTime), dt.Columns[0].DataType);

            Assert.AreEqual("Field2", dt.Columns[1].ColumnName);
            Assert.AreEqual(typeof (string), dt.Columns[1].DataType);

            Assert.AreEqual("Field3", dt.Columns[2].ColumnName);
            Assert.AreEqual(typeof (int), dt.Columns[2].DataType);
        }

        [Test]
        public void SaveLoadXmlFileDelimited()
        {
            var cb = new DelimitedClassBuilder("Customers", ",");
            cb.IgnoreFirstLines = 1;
            cb.IgnoreEmptyLines = true;

            cb.AddField("Field1", typeof(DateTime));
            cb.LastField.TrimMode = TrimMode.Both;
            cb.LastField.QuoteMode = QuoteMode.AlwaysQuoted;
            cb.LastField.FieldNullValue = DateTime.Today;

            cb.AddField("FieldTwo", typeof(string));
            cb.LastField.FieldQuoted = true;
            cb.LastField.QuoteChar = '"';

            cb.AddField("Field333", typeof(int));

            cb.SaveToXml(@"dynamic.xml");

            var loaded = (DelimitedClassBuilder)ClassBuilder.LoadFromXml(@"dynamic.xml");

            Assert.AreEqual("Field1", loaded.FieldByIndex(0).FieldName);
            Assert.AreEqual("FieldTwo", loaded.FieldByIndex(1).FieldName);
            Assert.AreEqual("Field333", loaded.FieldByIndex(2).FieldName);

            Assert.AreEqual("System.DateTime", loaded.FieldByIndex(0).FieldType);
            Assert.AreEqual("System.String", loaded.FieldByIndex(1).FieldType);
            Assert.AreEqual("System.Int32", loaded.FieldByIndex(2).FieldType);

            Assert.AreEqual(QuoteMode.AlwaysQuoted, loaded.FieldByIndex(0).QuoteMode);
            Assert.AreEqual(false, loaded.FieldByIndex(0).FieldQuoted);

            Assert.AreEqual('"', loaded.FieldByIndex(1).QuoteChar);
            Assert.AreEqual(true, loaded.FieldByIndex(1).FieldQuoted);
        }

        [Test]
		[Category("Dynamic")]
		public void SaveLoadXmlFileDelimited2()
        {
            var cb = new DelimitedClassBuilder("Customers", ",");
            cb.IgnoreFirstLines = 1;
            cb.IgnoreEmptyLines = true;

            cb.AddField("Field1", typeof(DateTime));
            cb.LastField.TrimMode = TrimMode.Both;
            cb.LastField.QuoteMode = QuoteMode.AlwaysQuoted;
            cb.LastField.FieldNullValue = DateTime.Today;

            cb.AddField("FieldTwo", typeof(string));
            cb.LastField.FieldQuoted = true;
            cb.LastField.QuoteChar = '"';

            cb.AddField("Field333", typeof(int));

            cb.SaveToXml(@"dynamic.xml");

            mEngine = new FileHelperEngine(ClassBuilder.ClassFromXmlFile("dynamic.xml"));

            Assert.AreEqual("Customers", mEngine.RecordType.Name);
            Assert.AreEqual(3, mEngine.RecordType.GetFields().Length);
            Assert.AreEqual("Field1", mEngine.RecordType.GetFields()[0].Name);
        }

        [Test]
        public void SaveLoadXmlOptions()
        {
            var cbOrig = new DelimitedClassBuilder("Customers", ",");
            cbOrig.AddField("Field1", typeof(DateTime));
            cbOrig.AddField("FieldTwo", typeof(string));

            cbOrig.RecordCondition.Condition = RecordCondition.ExcludeIfMatchRegex;
            cbOrig.RecordCondition.Selector = @"\w*";

            cbOrig.IgnoreCommentedLines.CommentMarker = "//";
            cbOrig.IgnoreCommentedLines.InAnyPlace = false;

            cbOrig.IgnoreEmptyLines = true;
            cbOrig.IgnoreFirstLines = 123;
            cbOrig.IgnoreLastLines = 456;

            cbOrig.SealedClass = false;
            cbOrig.SaveToXml(@"dynamic.xml");
            cbOrig.SaveToXml(@"dynamic.xml");

            ClassBuilder cb2 = ClassBuilder.LoadFromXml("dynamic.xml");

            Assert.AreEqual("Customers", cb2.ClassName);
            Assert.AreEqual(2, cb2.FieldCount);
            Assert.AreEqual("Field1", cb2.Fields[0].FieldName);

            Assert.AreEqual(RecordCondition.ExcludeIfMatchRegex, cb2.RecordCondition.Condition);
            Assert.AreEqual(@"\w*", cb2.RecordCondition.Selector);

            Assert.AreEqual("//", cb2.IgnoreCommentedLines.CommentMarker);
            Assert.AreEqual(false, cb2.IgnoreCommentedLines.InAnyPlace);
            Assert.AreEqual(false, cb2.SealedClass);

            Assert.AreEqual(true, cb2.IgnoreEmptyLines);
            Assert.AreEqual(123, cb2.IgnoreFirstLines);
            Assert.AreEqual(456, cb2.IgnoreLastLines);
        }

        [Test]
        public void SaveLoadXmlOptionsString()
        {
            var cbOrig = new DelimitedClassBuilder("Customers", ",");
            cbOrig.AddField("Field1", typeof(DateTime));
            cbOrig.AddField("FieldTwo", typeof(string));

            cbOrig.RecordCondition.Condition = RecordCondition.ExcludeIfMatchRegex;
            cbOrig.RecordCondition.Selector = @"\w*";

            cbOrig.IgnoreCommentedLines.CommentMarker = "//";
            cbOrig.IgnoreCommentedLines.InAnyPlace = false;

            cbOrig.IgnoreEmptyLines = true;
            cbOrig.IgnoreFirstLines = 123;
            cbOrig.IgnoreLastLines = 456;

            cbOrig.SealedClass = false;
            string xml = cbOrig.SaveToXmlString();

            ClassBuilder cb2 = ClassBuilder.LoadFromXmlString(xml);

            Assert.AreEqual("Customers", cb2.ClassName);
            Assert.AreEqual(2, cb2.FieldCount);
            Assert.AreEqual("Field1", cb2.Fields[0].FieldName);

            Assert.AreEqual(RecordCondition.ExcludeIfMatchRegex, cb2.RecordCondition.Condition);
            Assert.AreEqual(@"\w*", cb2.RecordCondition.Selector);

            Assert.AreEqual("//", cb2.IgnoreCommentedLines.CommentMarker);
            Assert.AreEqual(false, cb2.IgnoreCommentedLines.InAnyPlace);
            Assert.AreEqual(false, cb2.SealedClass);

            Assert.AreEqual(true, cb2.IgnoreEmptyLines);
            Assert.AreEqual(123, cb2.IgnoreFirstLines);
            Assert.AreEqual(456, cb2.IgnoreLastLines);
        }

        public class MyCustomConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                if (from == "NaN")
                    return null;
                else
                    return Convert.ToInt32(Int32.Parse(from));
            }

            public override string FieldToString(object fieldValue)
            {
                if (fieldValue == null)
                    return "NaN";
                else
                    return fieldValue.ToString();
            }
        }

        [Test]
		[Category("Dynamic")]
		public void ReadAsDataTableWithCustomConverter()
        {
            var fields = new[] {
                "FirstName",
                "LastName",
                "StreetNumber",
                "StreetAddress",
                "Unit",
                "City",
                "State",
            };
            var cb = new DelimitedClassBuilder("ImportContact", ",");

            foreach (var f in fields)
            {
                cb.AddField(f, typeof(string));
                cb.LastField.TrimMode = TrimMode.Both;
                cb.LastField.FieldQuoted = false;
            }

            cb.AddField("Zip", typeof(int?));
            cb.LastField.Converter.TypeName = "FileHelpers.Tests.Dynamic.DelimitedClassBuilderTests.MyCustomConverter";

            mEngine = new FileHelperEngine(cb.CreateRecordClass());

            string source = "Alex & Jen,Bouquet,1815,Bell Rd,, Batavia,OH,45103" + Environment.NewLine +
                            "Mark & Lisa K ,Arlinghaus,1817,Bell Rd,, Batavia,OH,NaN" + Environment.NewLine +
                            "Ed & Karen S ,Craycraft,1819,Bell Rd,, Batavia,OH,45103" + Environment.NewLine;

            var contactData = mEngine.ReadString(source);

            Assert.AreEqual(3, contactData.Length);
            var zip = mEngine.RecordType.GetFields()[7];
            Assert.AreEqual("Zip", zip.Name);
            Assert.IsNull(zip.GetValue(contactData[1]));
            Assert.AreEqual((decimal)45103, zip.GetValue(contactData[2]));
        }

        [Test]
		[Category("Dynamic")]
		public void LoopingFields()
        {
            var cb = new DelimitedClassBuilder("MyClass", ",");

            string[] lst = { "fieldOne", "fieldTwo", "fieldThree" };

            for (int i = 0; i < lst.Length; i++)
                cb.AddField(lst[i], typeof(string));

            mEngine = new FileHelperEngine(cb.CreateRecordClass());
        }

        [Test]
        public void GotchClassName1()
        {
            new DelimitedClassBuilder(" MyClass", ",");
        }

        [Test]
        public void GotchClassName2()
        {
            new DelimitedClassBuilder(" MyClass  ", ",");
        }

        [Test]
        public void GotchClassName3()
        {
            new DelimitedClassBuilder("MyClass  ", ",");
        }

        [Test]
        public void GotchClassName4()
        {
            new DelimitedClassBuilder("_MyClass2", ",");
        }

        [Test]
        public void BadClassName1()
        {
            Assert.Throws<FileHelpersException>(()
                => new DelimitedClassBuilder("2yClass", ","));
        }

        [Test]
        public void BadClassName2()
        {
            Assert.Throws<FileHelpersException>(()
                => new DelimitedClassBuilder("My Class", ","));
        }

        [Test]
        public void BadClassName3()
        {
            Assert.Throws<FileHelpersException>(()
                => new DelimitedClassBuilder("", ","));
        }
    }
}