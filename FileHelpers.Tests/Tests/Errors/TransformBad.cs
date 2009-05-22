#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class TransformBad
	{
		[Test]
		public void Transform1()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass1), typeof(ToClass1));
            Assert.Throws<BadUsageException>(() 
                => link.TransformFile("a","b"));
		}

		[Test]
		public void Transform2()
		{
			Assert.Throws<BadUsageException>(() 
                => new FileTransformEngine(typeof(FromClass2), typeof(ToClass1)));
            
		}

		[Test]
		public void Transform3()
		{
			FileTransformEngine link = new FileTransformEngine(typeof(FromClass3), typeof(ToClass2));
            Assert.Throws<FileNotFoundException>(() 
                => link.TransformFile("aaskdhaklhdla","baskdkalsd"));
		}

		[Test]
		public void Transform4()
		{
			Assert.Throws<BadUsageException>(() 
                => new FileTransformEngine(typeof(FromClass4), typeof(ToClass1)));
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