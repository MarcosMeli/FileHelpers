using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class DelimitedMoreFieldsTests
    {
        [Test]
        public void MoreFields()
        {
            Assert.Throws<BadUsageException>(() =>
                FileTest.Good.OrdersSmallVerticalBar
                    .ReadWithEngine<Orders3Fields>());
        }

        [Test]
        public void MoreFields2()
        {
            Assert.Throws<BadUsageException>(() =>
                FileTest.Good.OrdersSmallVerticalBar
                    .ReadWithEngine<Orders2Fields>());
        }


        [DelimitedRecord("|")]
        public class Orders3Fields
        {
            public string OrderID;

            public string CustomerID;

            public string OrderDate;
        }

        [DelimitedRecord("|")]
        public class Orders2Fields
        {
            public string OrderID;

            public string CustomerID;
        }
    }
}