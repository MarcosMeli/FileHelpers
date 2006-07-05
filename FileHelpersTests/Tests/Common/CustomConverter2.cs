using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	// SPECIAL FIELD
	public class AddressField
	{
		public string Street;
		public string Number;
		public string City;

		public override string ToString()
		{
			return Street + " - " + Number + ", " + City;
		}

	}

	// CUSTOM CONVERTER
	public class AddressConverter : ConverterBase
	{
		public override object StringToField(string from)
		{
			string[] splited1 = from.Split(',');
			string[] splited2 = splited1[0].Split('-');

			AddressField res = new AddressField();
			res.Street = splited2[0].Trim();
			res.Number = splited2[1].Trim();
			res.City = splited1[1].Trim();

			return res;
		}
	}

	// TEST CLASS
	[DelimitedRecord("|")]
	public class AddressConvClass
	{
		[FieldConverter(typeof (AddressConverter))] public AddressField Address;
		public int Age;
	}


	// NUNIT TESTS
	[TestFixture]
	public class CustomConvertAddress
	{
		FileHelperEngine engine;

		[Test]
		public void NameConverterTest()
		{
			engine = new FileHelperEngine(typeof (AddressConvClass));

			AddressConvClass[] res = (AddressConvClass[]) Common.ReadTest(engine, @"Good\CustomConverter2.txt");

			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("Bahia Blanca", res[0].Address.City);
			Assert.AreEqual("Sin Nombre", res[0].Address.Street);
			Assert.AreEqual("13", res[0].Address.Number);

			Assert.AreEqual("Saavedra", res[1].Address.City);
			Assert.AreEqual("Florencio Sanches", res[1].Address.Street);
			Assert.AreEqual("s/n", res[1].Address.Number);

			Assert.AreEqual("Bs.As", res[2].Address.City);
			Assert.AreEqual("12 de Octubre", res[2].Address.Street);
			Assert.AreEqual("4", res[2].Address.Number);

			Assert.AreEqual("Chilesito", res[3].Address.City);
			Assert.AreEqual("Pololo", res[3].Address.Street);
			Assert.AreEqual("5421", res[3].Address.Number);

		}

	}
}