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
		[Test]
		public void CsvToFixedLength()
		{
			var link = new FileTransformEngine<FromClass, ToClass>();
			link.TransformFile(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToFixedLengthCommon()
		{
			CommonEngine.TransformFile<FromClass, ToClass>(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToFixedLengthCommonAsync()
		{
            CommonEngine.TransformFileAsync<FromClass, ToClass>(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}


		[Test]
		public void CsvToFixedLength2()
		{
            var link = new FileTransformEngine<FromClass, ToClass>();
			link.TransformFile(TestCommon.GetPath("Good", "Transform2.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));
			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?                                 ", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"                               ", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\                                             ", res[2].CompanyName);
		}

		[Test]
		public void TransformRecords()
		{
			var engine = new FileTransformEngine<FromClass, ToClass>();
			ToClass[] res = engine.ReadAndTransformRecords(TestCommon.GetPath("Good", "Transform2.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}


		[Test]
		public void CsvToDelimited()
		{
			var link = new FileTransformEngine<FromClass, ToClass2>();
			link.TransformFile(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimitedCommon()
		{
			CommonEngine.TransformFile<FromClass, ToClass2>(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimitedCommonAsync()
		{
            CommonEngine.TransformFileAsync<FromClass, ToClass2>(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimited2()
		{
            var link = new FileTransformEngine<FromClass, ToClass2>();
			link.TransformFile(TestCommon.GetPath("Good", "Transform2.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));
			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}







		[Test]
		public void AsyncCsvToFixedLength()
		{
            var link = new FileTransformEngine<FromClass, ToClass>();
			link.TransformFileAsync(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void AsyncCsvToFixedLength2()
		{
            var link = new FileTransformEngine<FromClass, ToClass>();
			link.TransformFileAsync(TestCommon.GetPath("Good", "Transform2.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));
			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?                                 ", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"                               ", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\                                             ", res[2].CompanyName);
		}


		[Test]
		public void AsyncCsvToDelimited()
		{
            var link = new FileTransformEngine<FromClass, ToClass2>();
			link.TransformFileAsync(TestCommon.GetPath("Good", "Transform1.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));

			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void AsyncCsvToDelimited2()
		{
            var link = new FileTransformEngine<FromClass, ToClass2>();
			link.TransformFileAsync(TestCommon.GetPath("Good", "Transform2.txt"), TestCommon.GetPath("Good", "transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(TestCommon.GetPath("Good", "transformout.txt"));
			if (File.Exists(TestCommon.GetPath("Good", "transformout.txt"))) File.Delete(TestCommon.GetPath("Good", "transformout.txt"));

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