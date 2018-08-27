using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Assertions;
using Alexa.NET.Response;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class CardTests
    {
        [Fact]
        public void HasCardPositiveCheck()
        {
            var assertCard = new SimpleCard();
            var response = ResponseBuilder.Empty();
            response.Response.Card = assertCard;
            var card = AlexaAssertions.HasCard(response);
            Assert.NotNull(card);
            Assert.Equal(assertCard, card);
        }

        [Fact]
        public void HasCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => AlexaAssertions.HasCard(response));
        }

        [Fact]
        public void HasSimpleCardPositiveCheck()
        {
            var assertCard = new SimpleCard { Content = "test" };
            var response = ResponseBuilder.Empty();
            response.Response.Card = assertCard;
            var simpleCard = AlexaAssertions.HasSimpleCard(response);
            Assert.NotNull(simpleCard);
            Assert.Equal(assertCard,simpleCard);
        }

        [Fact]
        public void HasSimpleCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => AlexaAssertions.HasSimpleCard(response));
        }

        [Fact]
        public void HasSimpleCardMismatchCheck()
        {
            var response = ResponseBuilder.Empty();
            response.Response.Card = new StandardCard();
            Assert.Throws<CardMismatchException>(() => AlexaAssertions.HasSimpleCard(response));
        }
    }
}
