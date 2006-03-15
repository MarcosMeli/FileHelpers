#if ! MINI

using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.DataLink
{
	[TestFixture]
	public class DataProviders
	{
		[Test]
		public void OrdersProvider()
		{
			DataStorage provider = new OrdersLinkProvider();
			object[] res = provider.ExtractRecords();

			Assert.AreEqual(830, res.Length);
			Assert.AreEqual(typeof (OrdersFixed), res[0].GetType());

		}

		[Test]
		public void CustomersProvider()
		{
			DataStorage provider = new CustomersDataSotrage();
			object[] res = provider.ExtractRecords();

			Assert.AreEqual(91, res.Length);
			Assert.AreEqual(typeof (CustomersVerticalBar), res[0].GetType());
		}

	}

}

#endif