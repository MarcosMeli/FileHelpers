using System;
using System.IO;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class OtherErrors
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine engine2;

		[Test]
		[ExpectedException(typeof (FileNotFoundException))]
		public void FileNotFound()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.ReadFile("No foun343333d this file.txt");
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullReader()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.ReadStream(null);
		}

		[Test]
		public void NullString()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.ReadString(null);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullWriter()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.WriteStream(null, null);
		}


		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullRecords()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.WriteStream(new StringWriter(new StringBuilder()), null);
		}






		[Test]
		[ExpectedException(typeof (FileNotFoundException))]
		public void FileNotFound2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginReadFile("No fouffffnd this file.txt");
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void FileNotFound3()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginAppendToFile(null);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullReader2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginReadStream(null);
		}

		[Test]
		public void NullString2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginReadString(null);
		}

		[Test]
		[ExpectedException(typeof (ArgumentException))]
		public void NullWriter2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginWriteStream(null);
		}


		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullRecords2()
		{
			engine2 = new FileHelperAsyncEngine(typeof (SampleType));
			engine2.BeginWriteStream(new StringWriter(new StringBuilder()));

			engine2.WriteNexts(null);
		}


	}
}