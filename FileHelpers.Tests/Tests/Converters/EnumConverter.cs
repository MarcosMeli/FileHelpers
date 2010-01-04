using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
	[TestFixture]
	public class EnumConverter
	{
		FileHelperEngine engine;
        
		[Test]
		public void EnumSingleCase()
		{
            engine = new FileHelperEngine(typeof (EnumType2));

			EnumType2[] res = (EnumType2[]) TestCommon.ReadTest(engine, "Good", "EnumConverter2.txt");

			Assert.AreEqual(5, res.Length);

			Assert.AreEqual(Enum2.One, res[0].EnumValue);
			Assert.AreEqual(Enum2.One, res[1].EnumValue);
			Assert.AreEqual(Enum2.Two, res[2].EnumValue);
			Assert.AreEqual(Enum2.Three, res[3].EnumValue);
			Assert.AreEqual(Enum2.Three, res[4].EnumValue);
		}

		[Test]
		public void EnumexplicitConverter()
		{
			engine = new FileHelperEngine(typeof (EnumType3));

			EnumType3[] res = (EnumType3[]) TestCommon.ReadTest(engine, "Good", "EnumConverter2.txt");

			Assert.AreEqual(5, res.Length);

			Assert.AreEqual(Enum2.One, res[0].EnumValue);
			Assert.AreEqual(Enum2.One, res[1].EnumValue);
			Assert.AreEqual(Enum2.Two, res[2].EnumValue);
			Assert.AreEqual(Enum2.Three, res[3].EnumValue);
			Assert.AreEqual(Enum2.Three, res[4].EnumValue);
		}

		[Test]
		public void EnumMulticase()
		{
			engine = new FileHelperEngine(typeof (EnumType1));

			EnumType1[] res = (EnumType1[]) TestCommon.ReadTest(engine, "Good", "EnumConverter1.txt");

			Assert.AreEqual(5, res.Length);

			Assert.AreEqual(Enum1.ONe, res[0].EnumValue);
			Assert.AreEqual(Enum1.ONe, res[1].EnumValue);
			Assert.AreEqual(Enum1.two, res[2].EnumValue);
			Assert.AreEqual(Enum1.ThreE, res[3].EnumValue);
			Assert.AreEqual(Enum1.ThreE, res[4].EnumValue);
		}

		[Test]
		public void EnumValueNotFound()
		{
			engine = new FileHelperEngine(typeof (EnumType2));
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

			EnumType2[] res = (EnumType2[]) TestCommon.ReadTest(engine, "Good", "EnumConverter3.txt");

			Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(3, engine.ErrorManager.Errors[0].LineNumber);
			Assert.AreEqual(typeof(ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());

			Assert.AreEqual(4, res.Length);


			Assert.AreEqual(Enum2.One, res[0].EnumValue);
			Assert.AreEqual(Enum2.Two, res[1].EnumValue);
			Assert.AreEqual(Enum2.Three, res[2].EnumValue);
			Assert.AreEqual(Enum2.Three, res[3].EnumValue);
		}

	}


	public enum Enum1 {ONe, two, ThreE};
	public enum Enum2 {One, Two, Three};

	[DelimitedRecord(",")]
	public class EnumType1
	{
		public Enum1 EnumValue;
	}

	[DelimitedRecord(",")]
	public class EnumType2
	{
		public Enum2 EnumValue;
	}

	[DelimitedRecord(",")]
	public class EnumType3
	{
		[FieldConverter(typeof(Enum2))]
		public Enum2 EnumValue;
	}
}
