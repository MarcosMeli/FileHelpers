using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class Customers
    {
        private const int ExpectedRecords = 91;

        private void RunTests<type>(params string[] pathElements) where type : class
        {
            var engine = new FileHelperEngine<type>();

            var res = TestCommon.ReadTest<type>(engine, pathElements);

            Assert.AreEqual(ExpectedRecords, res.Length);
        }

        [Test]
        public void Fixed()
        {
            RunTests<CustomersFixed>("Good", "CustomersFixed.txt");
        }

        [Test]
        public void Tab()
        {
            RunTests<CustomersTab>("Good", "CustomersTab.txt");
        }

        [Test]
        public void VerticalBar()
        {
            RunTests<CustomersVerticalBar>("Good", "CustomersVerticalBar.txt");
        }

        [Test]
        public void SemiColon()
        {
            RunTests<CustomersSemiColon>("Good", "CustomersSemiColon.txt");
        }
    }
}