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

		[Test]
		[ExpectedException(typeof (FileNotFoundException))]
		public void FileNotFound()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.ReadFile("No found this file.txt");
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullReader()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			engine.ReadStream(null);
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


	}
}