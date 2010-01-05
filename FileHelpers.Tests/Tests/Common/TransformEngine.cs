#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class TransformEngine
	{
        string fileOut = TestCommon.GetTempFile("transformout.txt");

		[Test]
		public void CsvToFixedLength()
		{
			var link = new FileTransformEngine<FromClass, ToClass>();
			link.TransformFile(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
            ToClass[] res = (ToClass[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToFixedLengthCommon()
		{
            CommonEngine.TransformFile<FromClass, ToClass>(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToFixedLengthCommonAsync()
		{
            CommonEngine.TransformFileAsync<FromClass, ToClass>(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}


		[Test]
		public void CsvToFixedLength2()
		{
            var link = new FileTransformEngine<FromClass, ToClass>();
            link.TransformFile(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
            ToClass[] res = (ToClass[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(@"c:\Prueba1\anda ?                                 ", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"                               ", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\                                             ", res[2].CompanyName);
		}

		[Test]
		public void TransformRecords()
		{
			var engine = new FileTransformEngine<FromClass, ToClass>();
			ToClass[] res = engine.ReadAndTransformRecords(FileTest.Good.Transform2.Path);

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}


		[Test]
		public void CsvToDelimited()
		{
			var link = new FileTransformEngine<FromClass, ToClass2>();
            link.TransformFile(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
            ToClass2[] res = (ToClass2[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimitedCommon()
		{
            CommonEngine.TransformFile<FromClass, ToClass2>(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
            ToClass2[] res = (ToClass2[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimitedCommonAsync()
		{
            CommonEngine.TransformFileAsync<FromClass, ToClass2>(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
            ToClass2[] res = (ToClass2[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimited2()
		{
            var link = new FileTransformEngine<FromClass, ToClass2>();
            link.TransformFile(FileTest.Good.Transform2.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
            ToClass2[] res = (ToClass2[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}







		[Test]
		public void AsyncCsvToFixedLength()
		{
            var link = new FileTransformEngine<FromClass, ToClass>();
            link.TransformFileAsync(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
            ToClass[] res = (ToClass[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void AsyncCsvToFixedLength2()
		{
            var link = new FileTransformEngine<FromClass, ToClass>();
            link.TransformFileAsync(FileTest.Good.Transform2.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
            ToClass[] res = (ToClass[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(@"c:\Prueba1\anda ?                                 ", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"                               ", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\                                             ", res[2].CompanyName);
		}


		[Test]
		public void AsyncCsvToDelimited()
		{
            var link = new FileTransformEngine<FromClass, ToClass2>();
            link.TransformFileAsync(FileTest.Good.Transform1.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
            ToClass2[] res = (ToClass2[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void AsyncCsvToDelimited2()
		{
            var link = new FileTransformEngine<FromClass, ToClass2>();
            link.TransformFileAsync(FileTest.Good.Transform2.Path, fileOut);

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
            ToClass2[] res = (ToClass2[])engine.ReadFile(fileOut);

            if (File.Exists(fileOut)) File.Delete(fileOut);

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}


		[DelimitedRecord(",")]
		private class FromClass
            :ITransformable<ToClass>,
             ITransformable<ToClass2>
		{
			public string CustomerId;
			public string CompanyName;
			public string CustomerName;

		    ToClass ITransformable<ToClass>.TransformTo()
		    {
                ToClass res = new ToClass();
                res.CustomerId = CustomerId;
                res.CompanyName = CompanyName;
                res.CustomerName = CustomerName;

                return res;
		    }

		    ToClass2 ITransformable<ToClass2>.TransformTo()
		    {
                ToClass2 res = new ToClass2();
                res.CustomerId = CustomerId;
                res.CompanyName = CompanyName;
                res.CustomerName = CustomerName;

                return res;
            }
		}
	
		[DelimitedRecord("|")]
		private class ToClass2
		{
			public string CustomerId;
			public string CompanyName;
			public string CustomerName;
		}
		[FixedLengthRecord()]
		private class ToClass
		{
			[FieldFixedLength(10)]
			public string CustomerId;
			[FieldFixedLength(50)]
			public string CompanyName;
			[FieldFixedLength(60)]
			public string CustomerName;
		}
	
	}
}

#endif