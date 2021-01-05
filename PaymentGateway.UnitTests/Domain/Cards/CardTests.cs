using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.UnitTests.Domain.Cards
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "An empty card number was inappropriately allowed.")]
        public void Should_NotAllowACardWithoutANumber_When_ACardWithoutANumberIsRequested()
        {
            new Card("", 1, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with spaces was inappropriately allowed.")]
        public void Should_NotAllowACardWithInvalidNumber_When_ACardWithSpacesInNumberIsRequested()
        {
            new Card("1 1" , 1, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with spaces was inappropriately allowed.")]
        public void Should_NotAllowACardWithInvalidNumber_When_ACardWithLettersInNumberIsRequested()
        {
            new Card("1L1", 1, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 0 as expiration month was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationMonth_When_ACardWith0AsExpirationMonthIsRequested()
        {
            new Card("11112222", 0, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 13 as expiration month was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationMonth_When_ACardWith13AsExpirationMonthIsRequested()
        {
            new Card("11112222", 13, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 1949 as expiration year was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationYear_When_ACardWith1949AsExpirationYearIsRequested()
        {
            new Card("11112222", 1, 1949, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 1949 as expiration year was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationYear_When_ACardWithOverTheMaxAsExpirationYearIsRequested()
        {
            new Card("11112222", 1, (ushort)DateTime.Now.AddYears(101).Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with a CVV of length 2 was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidCVV_When_ACardWithALength2CVVIsRequested()
        {
            new Card("11112222", 1, (ushort)DateTime.Now.Year, "42");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with a CVV of length 6 was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidCVV_When_ACardWithALength6CVVIsRequested()
        {
            new Card("11112222", 1, (ushort)DateTime.Now.Year, "424242");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with a CVV containing invalid characters was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidCVV_When_ACardWithALengthCVVContainingLettersIsRequested()
        {
            new Card("11112222", 1, (ushort)DateTime.Now.Year, "A03");
        }

        [TestMethod]
        public void Should_CorrectlyAllowACardToBeCreated_When_ACardWithValidPropertiesIsRequested()
        {
            Card card = new Card("11112222", 1, (ushort)DateTime.Now.Year, "001");
            Assert.IsNotNull(card);
        }
    }
}
