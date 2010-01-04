using FileHelpers;
using System.Diagnostics;
using System;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
	[TestFixture]
	public class BadFormat
	{

		[Test]
		public void Reading_DateWithBadFormat_Throws_ConvertException()
		{
            Assert.Throws<ConvertException>(()
                  => FileTest.Bad.BadDate1
                    .ReadWithEngine<SampleType>());
        }

		[Test]
        public void Reading_DateWithBadFormat_Throws_ConvertException2()
		{
            Assert.Throws<ConvertException>(()
                  => FileTest.Bad.BadDate2
                    .ReadWithEngine<SampleType>());
        }

		[Test]
        public void Reading_IntWithLetters_Throws_ConvertException()
		{
            Assert.Throws<ConvertException>(()
                  => FileTest.Bad.IntWithLetters
                    .ReadWithEngine<SampleType>());
        }

		[Test]
        public void Reading_IntWithDot_Throws_ConvertException()
		{
            Assert.Throws<ConvertException>(()
                      => FileTest.Bad.IntWithDot
                        .ReadWithEngine<SampleType>());
        }

        [Test]
        public void Reading_IntWithSpaces_Throws_ConvertException()
        {
            Assert.Throws<ConvertException>(()
                      => FileTest.Bad.IntWithSpaces1
                        .ReadWithEngine<SampleTypeInt>());
        }
            
		[Test]
        public void Reading_IntWithSpaces_Throws_ConvertException2()
		{
			Assert.Throws<ConvertException>(()
                => FileTest.Bad.IntWithSpaces2
                        .ReadWithEngine<SampleTypeInt>());
		}

		[Test]
		public void NoPendingNullValue()
		{
            var res = FileTest.Bad.NoBadNullvalue.ReadWithEngine<SampleType>();
            res.Length.AssertEqualTo(4);
        
        }

	}
}