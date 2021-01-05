using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Helpers;

namespace PaymentGateway.UnitTests.Domain.Helpers
{
    [TestClass]
    public class ByteArrayExtensionsTests
    {
        [TestMethod]
        public void Should_ConvertCorrectlyToHexString_GivenAByteArray()
        {
            byte[] array = new byte[] { 1, 2, 3, 4, 255 };
            string hex = array.ToHexString();
            Assert.AreEqual("01020304FF", hex);
        }
    }
}
