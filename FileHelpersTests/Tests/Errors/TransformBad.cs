#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class TransformBad
	{
		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void Transform1()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass1), typeof(ToClass1));
			link.TransformFile("a","b");
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void Transform2()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass2), typeof(ToClass1));
			link.TransformFile("a","b");
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void Transform3()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass3), typeof(ToClass2));
			link.TransformFile("a","b");
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void Transform4()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass4), typeof(ToClass1));
			link.TransformFile("a","b");
		}

		[DelimitedRecord(",")]
		private class FromClass1
		{
			public string Field1;
			public string Field2;
			public string Field3;
		}
	
		
		[DelimitedRecord(",")]
		private class FromClass2
		{
			public string Field1;
			public string Field2;
			public string Field3;

			[TransformToRecord(typeof(ToClass1))]
			public void  Transform()
			{
//				ToClass2 res = new ToClass2();
//				res.Field1 = Field1;
//				res.Field2 = Field2;
//				res.Field3 = Field3;
//
//				return res;
			}
		}
	

		[DelimitedRecord(",")]
		private class FromClass3
		{
			public string Field1;
			public string Field2;
			public string Field3;

			[TransformToRecord(typeof(ToClass2))]
			public ToClass2 Transform()
			{
				ToClass2 res = new ToClass2();
				res.Field1 = Field1;
				res.Field2 = Field2;
				res.Field3 = Field3;

				return res;
			}
		}
	
		[DelimitedRecord(",")]
		private class FromClass4
		{
			public string Field1;
			public string Field2;
			public string Field3;

			[TransformToRecord(typeof(ToClass1))]
			public ToClass1 Transform(bool test)
			{
				ToClass1 res = new ToClass1();
				res.Field1 = Field1;
				res.Field2 = Field2;
				res.Field3 = Field3;

				return res;
			}
		}
		[FixedLengthRecord()]
		private class ToClass1
		{
			[FieldFixedLength(10)]
			public string Field1;
			[FieldFixedLength(50)]
			public string Field2;
			[FieldFixedLength(60)]
			public string Field3;
		}

		private class ToClass2
		{
			public string Field1;
			public string Field2;
			public string Field3;
		}
	
	}
}

#endif