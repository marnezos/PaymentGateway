using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.UnitTests.Domain.Helpers
{
    [TestClass]
    public class StringExtensionsTests
    {

        [TestMethod]
        public void Should_CorrectlyDetectAStringContainingOnlyDigits_When_AnIntegerIsGivenAsString()
        {
            string digits = "1234567890";

            bool hasOnlyDigits = digits.ContainsOnlyDigits();

            Assert.IsTrue(hasOnlyDigits);
        }


        [TestMethod]
        public void Should_CorrectlyDetectAStringContainingDigitsAndNumber_When_AnIntegerMixedWithLettersIsGivenAsString()
        {
            string digits = "1234567890A";

            bool hasOnlyDigits = digits.ContainsOnlyDigits();

            Assert.IsFalse(hasOnlyDigits);
        }

        [TestMethod]
        public void Should_CorrectlyDetectAStringContainingDigitsAndNumber_When_AnIntegerMixedWithSpacesIsGivenAsString()
        {
            string digits = "1234 5678 90";

            bool hasOnlyDigits = digits.ContainsOnlyDigits();

            Assert.IsFalse(hasOnlyDigits);
        }
    }
}
