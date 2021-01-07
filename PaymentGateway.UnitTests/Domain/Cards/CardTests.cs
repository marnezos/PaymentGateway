using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Cards;
using System;

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
            new Card("411 111111111111", 1, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with spaces was inappropriately allowed.")]
        public void Should_NotAllowACardWithInvalidNumber_When_ACardWithLettersInNumberIsRequested()
        {
            new Card("41L1111111111111", 1, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 31 digits was inappropriately allowed.")]
        public void Should_NotAllowACardWithInvalidNumber_When_ACardWithAVeryLongNumberIsRequested()
        {
            new Card(new string('1',31), 1, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 11 digits was inappropriately allowed.")]
        public void Should_NotAllowACardWithInvalidNumber_When_ACardWithAVeryShortNumberIsRequested()
        {
            new Card(new string('1', 11), 1, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 0 as expiration month was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationMonth_When_ACardWith0AsExpirationMonthIsRequested()
        {
            new Card("4111111111111111", 0, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 13 as expiration month was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationMonth_When_ACardWith13AsExpirationMonthIsRequested()
        {
            new Card("4111111111111111", 13, (ushort)DateTime.Now.Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 1949 as expiration year was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationYear_When_ACardWith1949AsExpirationYearIsRequested()
        {
            new Card("4111111111111111", 1, 1949, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with 1949 as expiration year was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidExpirationYear_When_ACardWithOverTheMaxAsExpirationYearIsRequested()
        {
            new Card("4111111111111111", 1, (ushort)DateTime.Now.AddYears(101).Year, "123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with a CVV of length 2 was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidCVV_When_ACardWithALength2CVVIsRequested()
        {
            new Card("4111111111111111", 1, (ushort)DateTime.Now.Year, "42");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with a CVV of length 6 was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidCVV_When_ACardWithALength6CVVIsRequested()
        {
            new Card("4111111111111111", 1, (ushort)DateTime.Now.Year, "424242");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A card number with a CVV containing invalid characters was inappropriately allowed.")]
        public void Should_NotAllowACardInvalidCVV_When_ACardWithALengthCVVContainingLettersIsRequested()
        {
            new Card("4111111111111111", 1, (ushort)DateTime.Now.Year, "A03");
        }

        [TestMethod]
        public void Should_CorrectlyAllowACardToBeCreated_When_ACardWithValidPropertiesIsRequested()
        {
            Card card = new Card("4111111111111111", 1, (ushort)DateTime.Now.Year, "001");
            Assert.IsNotNull(card);
        }


        [TestMethod]
        public void Should_CovertCorrectlyToString_WhenAValidCardIsGiven()
        {
            Card card = new Card("4111111111111111", 1, (ushort)DateTime.Now.Year, "001");
            Assert.AreEqual($"4111111111111111-{DateTime.Now.Year}-01-001", card.ToString());
        }
    }
}
