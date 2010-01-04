#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class TransformBad
	{

        [Test]
		public void Transform3()
		{
			var link = new FileTransformEngine<FromClass3, ToClass2>();
            Assert.Throws<FileNotFoundException>(() 
                => link.TransformFile("aaskdhaklhdla","baskdkalsd"));
		}

		


		[DelimitedRecord(",")]
		private class FromClass3
            :ITransformable<ToClass2>
		{
			public string Field1;
			public string Field2;
			public string Field3;

		    public ToClass2 TransformTo()
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
            :ITransformable<ToClass1>
		{
			public string Field1;
			public string Field2;
			public string Field3;

		    public ToClass1 TransformTo()
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