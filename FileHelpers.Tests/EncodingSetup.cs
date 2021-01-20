using System.Text;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [SetUpFixture]
    public class EncodingSetup
    {
        [OneTimeSetUp]
        public void RegisterCodePages()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}