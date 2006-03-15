#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class TransformEngine
	{
		[Test]
		public void CsvToVerticalDelimited()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass), typeof(ToClass));
			link.TransformFile1To2(TestCommon.TestPath("Good\\Transform1.txt"), TestCommon.TestPath("Good\\transformout.txt"));
		}

		[DelimitedRecord(",")]
		private class FromClass
		{
			public string Field1;
			public string Field2;
			public string Field3;

			[TransformToRecord(typeof(ToClass))]
			public ToClass Transform()
			{
				ToClass res = new ToClass();
				res.Field1 = Field1;
				res.Field2 = Field2;
				res.Field3 = Field3;

				return res;
			}
		}
	
		[FixedLengthRecord()]
		private class ToClass
		{
			[FieldFixedLength(10)]
			public string Field1;
			[FieldFixedLength(50)]
			public string Field2;
			[FieldFixedLength(60)]
			public string Field3;
		}
	
	}
}

#endif