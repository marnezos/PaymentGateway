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
        public void Should_CorrectlyDetectAStringNotContainingOnlyDigits_When_AnIntegerMixedWithLettersIsGivenAsString()
        {
            string digits = "1234567890A";

            bool hasOnlyDigits = digits.ContainsOnlyDigits();

            Assert.IsFalse(hasOnlyDigits);
        }

        [TestMethod]
        public void Should_CorrectlyDetectAStringNotContainingOnlyDigits_When_AnIntegerMixedWithSpacesIsGivenAsString()
        {
            string digits = "1234 5678 90";

            bool hasOnlyDigits = digits.ContainsOnlyDigits();

            Assert.IsFalse(hasOnlyDigits);
        }


        [TestMethod]
        public void Should_CorrectlyDetectAStringContainingOnlyLetters_When_OnlyCapitalLettersAreGivenAsInput()
        {
            string letters = "ABC";
            bool hasOnlyLetters = letters.ContainsOnlyLetters();

            Assert.IsTrue(hasOnlyLetters);
        }

        [TestMethod]
        public void Should_CorrectlyDetectAStringContainingOnlyLetters_When_OnlyLowerCaseLettersAreGivenAsInput()
        {
            string letters = "abc";
            bool hasOnlyLetters = letters.ContainsOnlyLetters();

            Assert.IsTrue(hasOnlyLetters);
        }

        [TestMethod]
        public void Should_CorrectlyDetectAStringNotContainingOnlyLetters_When_NumbersAreGivenAsInput()
        {
            string letters = "123";
            bool hasOnlyLetters = letters.ContainsOnlyLetters();

            Assert.IsFalse(hasOnlyLetters);
        }

        [TestMethod]
        public void Should_CorrectlyDetectAStringNotContainingOnlyLetters_When_MixedNumbersAndLettersAreGivenAsInput()
        {
            string letters = "AB3";
            bool hasOnlyLetters = letters.ContainsOnlyLetters();

            Assert.IsFalse(hasOnlyLetters);
        }

        [TestMethod]
        public void Should_CorrectlyDetectAStringNotContainingOnlyLetters_When_MixedLettersAndSpacesAreGivenAsInput()
        {
            string letters = "AB ";
            bool hasOnlyLetters = letters.ContainsOnlyLetters();

            Assert.IsFalse(hasOnlyLetters);
        }

    }
}
