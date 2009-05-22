#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class TransformEngine
	{
		[Test]
		public void CsvToFixedLength()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass));
			link.TransformFile(Common.TestPath("Good\\Transform1.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToFixedLengthCommon()
		{
			CommonEngine.TransformFile(Common.TestPath("Good\\Transform1.txt"), typeof(FromClass), Common.TestPath("Good\\transformout.txt"), typeof(ToClass));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToFixedLengthCommonAsync()
		{
			CommonEngine.TransformFileAsync(Common.TestPath("Good\\Transform1.txt"), typeof(FromClass), Common.TestPath("Good\\transformout.txt"), typeof(ToClass));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}


		[Test]
		public void CsvToFixedLength2()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass));
			link.TransformFile(Common.TestPath("Good\\Transform2.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));
			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?                                 ", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"                               ", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\                                             ", res[2].CompanyName);
		}

		[Test]
		public void TransformRecords()
		{
			FileTransformEngine engine = new FileTransformEngine(typeof(FromClass), typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadAndTransformRecords(Common.TestPath("Good\\Transform2.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}


		[Test]
		public void CsvToDelimited()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass2));
			link.TransformFile(Common.TestPath("Good\\Transform1.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimitedCommon()
		{
			CommonEngine.TransformFile(Common.TestPath("Good\\Transform1.txt"), typeof(FromClass), Common.TestPath("Good\\transformout.txt"), typeof(ToClass2));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimitedCommonAsync()
		{
			CommonEngine.TransformFileAsync(Common.TestPath("Good\\Transform1.txt"), typeof(FromClass), Common.TestPath("Good\\transformout.txt"), typeof(ToClass2));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void CsvToDelimited2()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass2));
			link.TransformFile(Common.TestPath("Good\\Transform2.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));
			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}







		[Test]
		public void AsyncCsvToFixedLength()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass));
			link.TransformFileAsync(Common.TestPath("Good\\Transform1.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void AsyncCsvToFixedLength2()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass));
			link.TransformFileAsync(Common.TestPath("Good\\Transform2.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass));
			ToClass[] res = (ToClass[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));
			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?                                 ", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"                               ", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\                                             ", res[2].CompanyName);
		}


		[Test]
		public void AsyncCsvToDelimited()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass2));
			link.TransformFileAsync(Common.TestPath("Good\\Transform1.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));

			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(6, res.Length);
		}

		[Test]
		public void AsyncCsvToDelimited2()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass2));
			link.TransformFileAsync(Common.TestPath("Good\\Transform2.txt"), Common.TestPath("Good\\transformout.txt"));

			FileHelperEngine engine = new FileHelperEngine(typeof(ToClass2));
			ToClass2[] res = (ToClass2[]) engine.ReadFile(Common.TestPath("Good\\transformout.txt"));
			if (File.Exists(Common.TestPath("Good\\transformout.txt"))) File.Delete(Common.TestPath("Good\\transformout.txt"));

			Assert.AreEqual(@"c:\Prueba1\anda ?", res[0].CompanyName);
			Assert.AreEqual("\"D:\\Glossaries\\O12\"", res[1].CompanyName);
			Assert.AreEqual(@"\\s\\", res[2].CompanyName);
		}


		[Test]
		public void TransformBad()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(ToClass), typeof(FromClass));
            Assert.Throws<BadUsageException>(()
                 => link.TransformFile(Common.TestPath("Good\\Transform1.txt"), Common.TestPath("Good\\transformout.txt")));
		}

		[Test]
		public void TransformBad2()
		{
			new FileTransformEngine(typeof(ToClass), typeof(FromClass));
		}


		[DelimitedRecord(",")]
		private class FromClass
		{
			public string CustomerId;
			public string CompanyName;
			public string CustomerName;

			[TransformToRecord(typeof(ToClass))]
			public ToClass Transform()
			{
				ToClass res = new ToClass();
				res.CustomerId = CustomerId;
				res.CompanyName = CompanyName;
				res.CustomerName = CustomerName;

				return res;
			}

			[TransformToRecord(typeof(ToClass2))]
			public ToClass2 Transform2()
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