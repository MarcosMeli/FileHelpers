#if !NETCOREAPP
using System;
using System.Data;
using System.IO;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests.Dynamic
{
    [TestFixture]
	[Category("Dynamic")]
	public class ClassBuilderTests
    {
        private const string Class =
            @"	[FixedLengthRecord(FixedMode.AllowVariableLength)]
			public class SampleType
			{
				[FieldFixedLength(8)]
				[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
				public DateTime Field1;

				[FieldFixedLength(3)]
				[FieldAlign(AlignMode.Left, ' ')]
				[FieldTrim(TrimMode.Both)]
				public string Field2;

				[FieldFixedLength(3)]
				[FieldAlign(AlignMode.Right, '0')]
				[FieldTrim(TrimMode.Both)]
				public int Field3;
			}
		";

        private const string ClassVbNet =
            @"	<FixedLengthRecord(FixedMode.AllowVariableLength)> _
			Public Class SampleType
			
				<FieldFixedLength(8), _
				 FieldConverter(ConverterKind.Date, ""ddMMyyyy"") > _
				public Field1 As DateTime

				<FieldFixedLength(3), _
				 FieldAlign(AlignMode.Left, "" ""c), _
				 FieldTrim(TrimMode.Both)> _
				public Field2 As String

				<FieldFixedLength(3), _
				 FieldAlign(AlignMode.Right, ""0""c), _
				 FieldTrim(TrimMode.Both) > _
				public Field3 As Integer
			
			End Class
		";

        [Test]
        public void ReadFile()
        {
            Type t = ClassBuilder.ClassFromString(Class);

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
            Assert.AreEqual("901", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[0][2]);

            Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
            Assert.AreEqual("012", dt.Rows[1][1]);
            Assert.AreEqual(345, dt.Rows[1][2]);
        }

        [Test]
        public void ReadFileDiffDir()
        {
            string oldDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = Path.GetTempPath();

            Type t = ClassBuilder.ClassFromString(Class);
            Environment.CurrentDirectory = oldDir;

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
            Assert.AreEqual("901", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[0][2]);

            Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
            Assert.AreEqual("012", dt.Rows[1][1]);
            Assert.AreEqual(345, dt.Rows[1][2]);
        }

        [Test]
        public void ReadFileVbNet()
        {
            Type t = ClassBuilder.ClassFromString(ClassVbNet, NetLanguage.VbNet);

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
            Assert.AreEqual("901", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[0][2]);

            Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
            Assert.AreEqual("012", dt.Rows[1][1]);
            Assert.AreEqual(345, dt.Rows[1][2]);
        }

        [Test]
        public void ReadFileAsync()
        {
            Type t = ClassBuilder.ClassFromString(Class, "SampleType");

            var asyncEngine = new FileHelperAsyncEngine(t);

            object[] res = TestCommon.ReadAllAsync(asyncEngine, "Good", "Test1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(4, asyncEngine.TotalRecords);
            Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);
        }

        [Test]
        public void ReadFileAsyncVbNet()
        {
            Type t = ClassBuilder.ClassFromString(ClassVbNet, "SampleType", NetLanguage.VbNet);

            var asyncEngine = new FileHelperAsyncEngine(t);

            object[] res = TestCommon.ReadAllAsync(asyncEngine, "Good", "Test1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(4, asyncEngine.TotalRecords);
            Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);
        }

        [Test]
        public void ReadFileEncDec()
        {
            var tempFile = "temp.fhc";
            ClassBuilder.ClassToBinaryFile(tempFile, Class);

            Type t = ClassBuilder.ClassFromBinaryFile(tempFile);
            File.Delete(tempFile);

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
            Assert.AreEqual("901", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[0][2]);

            Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
            Assert.AreEqual("012", dt.Rows[1][1]);
            Assert.AreEqual(345, dt.Rows[1][2]);
        }

        [Test]
        [Category("NotOnMono")]
        public void ReadFileClassInFileEnc()
        {
            var t = ClassBuilder.ClassFromBinaryFile(FileTest.Classes.SampleBinaryClass.Path);

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
            Assert.AreEqual("901", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[0][2]);

            Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
            Assert.AreEqual("012", dt.Rows[1][1]);
            Assert.AreEqual(345, dt.Rows[1][2]);
        }

        [Test]
        public void ReadFileClassInFile()
        {
            Type t = ClassBuilder.ClassFromSourceFile(FileTest.Classes.SampleClassCS.Path);

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
            Assert.AreEqual("901", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[0][2]);

            Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
            Assert.AreEqual("012", dt.Rows[1][1]);
            Assert.AreEqual(345, dt.Rows[1][2]);
        }

        [Test]
        public void ReadFileClassInFileVbNet()
        {
            Type t = ClassBuilder.ClassFromSourceFile(FileTest.Classes.SampleClassVB.Path, NetLanguage.VbNet);

            var engine = new FileHelperEngine(t);

            DataTable dt = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
            Assert.AreEqual("901", dt.Rows[0][1]);
            Assert.AreEqual(234, dt.Rows[0][2]);

            Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
            Assert.AreEqual("012", dt.Rows[1][1]);
            Assert.AreEqual(345, dt.Rows[1][2]);
        }
    }
}
#endif
