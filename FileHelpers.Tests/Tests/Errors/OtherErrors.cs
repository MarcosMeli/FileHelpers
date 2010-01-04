using System;
using System.IO;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
	[TestFixture]
	public class OtherErrors
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine engine2;

		[Test]
		public void FileNotFound()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			Assert.Throws<FileNotFoundException>(()
                => engine.ReadFile("No foun343333d this file.txt"));
		}

		[Test]
		public void NullReader()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			Assert.Throws<ArgumentNullException>(()
                => engine.ReadStream(null));
		}

		[Test]
		public void NullString()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.ReadString(null);
		}

		[Test]
		public void NullWriter()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			Assert.Throws<ArgumentNullException>(()
                => engine.WriteStream(null, null));
		}


		[Test]
		public void NullRecords()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			Assert.Throws<ArgumentNullException>(()
                => engine.WriteStream(new StringWriter(new StringBuilder()), null));
		}






		[Test]
		public void FileNotFound2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			Assert.Throws<FileNotFoundException>(()
                => engine2.BeginReadFile("No fouffffnd this file.txt"));
		}

		[Test]
		public void FileNotFound3()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			Assert.Throws<ArgumentNullException>(()
                => engine2.BeginAppendToFile(null));
		}

		[Test]
		public void NullReader2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			Assert.Throws<ArgumentNullException>(()
                => engine2.BeginReadStream(null));
		}

		[Test]
		public void NullString2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginReadString(null);
		}

		[Test]
		public void NullWriter2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			Assert.Throws<ArgumentException>(()
                => engine2.BeginWriteStream(null));
		}


		[Test]
		public void NullRecords2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginWriteStream(new StringWriter(new StringBuilder()));

			Assert.Throws<ArgumentNullException>(()
                => engine2.WriteNexts(null));
		}


	}
}