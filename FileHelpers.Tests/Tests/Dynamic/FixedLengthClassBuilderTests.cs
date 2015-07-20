using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests.Dynamic
{
    [TestFixture]
	[Category("Dynamic")]
	public class FixedLengthClassBuilderTests
    {
        private const string TempClassFile = "tempclass.cs";
        private FileHelperEngine mEngine;

        [Test]
        public void FullClassBuildingFixed()
        {
            var cb = new FixedLengthClassBuilder("Customers");

            cb.AddField("Field1", 8, typeof (DateTime));
            cb.LastField.Converter.Kind = ConverterKind.Date;
            cb.LastField.Converter.Arg1 = "ddMMyyyy";
            cb.LastField.FieldNullValue = DateTime.Now;


            cb.AddField("Field2", 3, typeof (string));
            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = ' ';

            cb.AddField("Field3", 3, typeof (int));

            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = '0';
            cb.LastField.TrimMode = TrimMode.Both;

            mEngine = new FileHelperEngine(cb.CreateRecordClass());

            DataTable dt = mEngine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, mEngine.TotalRecords);
            Assert.AreEqual(0, mEngine.ErrorManager.ErrorCount);
        }

        [Test]
        public void SaveLoadSourceFile()
        {
            ClassBuilder cb = CommonCreate();
            cb.SaveToSourceFile(TempClassFile);
            ValidateType(ClassBuilder.ClassFromSourceFile(TempClassFile));
        }

        [Test]
        public void SaveLoadSourceFileVbNet()
        {
            ClassBuilder cb = CommonCreate();
            cb.SaveToSourceFile(TempClassFile, NetLanguage.VbNet);
            ValidateType(ClassBuilder.ClassFromSourceFile(TempClassFile, NetLanguage.VbNet));
        }

        [Test]
        public void SaveLoadBinaryFile()
        {
            ClassBuilder cb = CommonCreate();
            cb.SaveToSourceFile(TempClassFile);
            ValidateType(ClassBuilder.ClassFromSourceFile(TempClassFile));
        }

        [Test]
        public void SaveLoadBinaryFileVbNet()
        {
            ClassBuilder cb = CommonCreate();
            cb.SaveToBinaryFile(TempClassFile, NetLanguage.VbNet);
            ValidateType(ClassBuilder.ClassFromBinaryFile(TempClassFile, NetLanguage.VbNet));
        }

        [Test]
        public void SaveLoadXmlFileFixed()
        {
            var cb = new FixedLengthClassBuilder("Customers");

            cb.FixedMode = FixedMode.ExactLength;
            cb.AddField("Field1", 8, typeof (DateTime));
            cb.LastField.Converter.Kind = ConverterKind.Date;
            cb.LastField.Converter.Arg1 = "ddMMyyyy";
            cb.LastField.FieldNullValue = DateTime.Now;

            cb.AddField("FieldSecond", 3, typeof (string));
            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = ' ';

            cb.AddField("Field33", 3, typeof (int));

            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = '0';
            cb.LastField.TrimMode = TrimMode.Both;

            cb.SaveToXml(@"dynamic.xml");

            var loaded = (FixedLengthClassBuilder)ClassBuilder.LoadFromXml(@"dynamic.xml");

            Assert.AreEqual("Field1", loaded.FieldByIndex(0).FieldName);
            Assert.AreEqual("FieldSecond", loaded.FieldByIndex(1).FieldName);
            Assert.AreEqual("Field33", loaded.FieldByIndex(2).FieldName);

            Assert.AreEqual("System.DateTime", loaded.FieldByIndex(0).FieldType);
            Assert.AreEqual("System.String", loaded.FieldByIndex(1).FieldType);
            Assert.AreEqual("System.Int32", loaded.FieldByIndex(2).FieldType);

            Assert.AreEqual(ConverterKind.Date, loaded.FieldByIndex(0).Converter.Kind);
            Assert.AreEqual("ddMMyyyy", loaded.FieldByIndex(0).Converter.Arg1);

            Assert.AreEqual(AlignMode.Right, loaded.FieldByIndex(1).AlignMode);
            Assert.AreEqual(' ', loaded.FieldByIndex(1).AlignChar);

            Assert.AreEqual(FixedMode.ExactLength, loaded.FixedMode);
        }

        [Test]
        public void SaveLoadXmlFileFixed2()
        {
            var cb = new FixedLengthClassBuilder("Customers");

            cb.AddField("Field1", 8, typeof (DateTime));
            cb.LastField.Converter.Kind = ConverterKind.Date;
            cb.LastField.Converter.Arg1 = "ddMMyyyy";
            cb.LastField.FieldNullValue = DateTime.Now;

            cb.AddField("FieldSecond", 3, typeof (string));
            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = ' ';

            cb.AddField("Field33", 3, typeof (int));

            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = '0';
            cb.LastField.TrimMode = TrimMode.Both;

            cb.SaveToXml(@"dynamic.xml");

            mEngine = new FileHelperEngine(ClassBuilder.ClassFromXmlFile("dynamic.xml"));

            Assert.AreEqual("Customers", mEngine.RecordType.Name);
            Assert.AreEqual(3, mEngine.RecordType.GetFields().Length);
            Assert.AreEqual("Field1", mEngine.RecordType.GetFields()[0].Name);
        }

        private static ClassBuilder CommonCreate()
        {
            var cb = new FixedLengthClassBuilder("Customers");

            cb.AddField("Field1", 8, typeof(DateTime));
            cb.LastField.Converter.Kind = ConverterKind.Date;
            cb.LastField.Converter.Arg1 = "ddMMyyyy";
            cb.LastField.FieldNullValue = DateTime.Now;

            cb.AddField("Field2", 3, typeof(string));

            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = ' ';

            cb.AddField("Field3", 3, typeof(int));

            cb.LastField.AlignMode = AlignMode.Right;
            cb.LastField.AlignChar = '0';
            cb.LastField.TrimMode = TrimMode.Both;

            return cb;
        }

        private void ValidateType(Type t)
        {
            mEngine = new FileHelperEngine(t);

            DataTable dt = mEngine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, mEngine.TotalRecords);
            Assert.AreEqual(0, mEngine.ErrorManager.ErrorCount);
        }
    }
}