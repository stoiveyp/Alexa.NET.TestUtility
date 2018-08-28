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
            var card = AlexaAssertions.Card(response);
            Assert.NotNull(card);
            Assert.Equal(assertCard, card);
        }

        [Fact]
        public void HasCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => AlexaAssertions.Card(response));
        }

        [Fact]
        public void HasSimpleCardPositiveCheck()
        {
            var assertCard = new SimpleCard { Content = "test" };
            var response = ResponseBuilder.Empty();
            response.Response.Card = assertCard;
            var simpleCard = AlexaAssertions.SimpleCard(response);
            Assert.NotNull(simpleCard);
            Assert.Equal(assertCard,simpleCard);
        }

        [Fact]
        public void HasSimpleCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => AlexaAssertions.SimpleCard(response));
        }

        [Fact]
        public void HasSimpleCardMismatchCheck()
        {
            var response = ResponseBuilder.Empty();
            response.Response.Card = new StandardCard();
            Assert.Throws<CardMismatchException>(() => AlexaAssertions.SimpleCard(response));
        }

        [Fact]
        public void HasStandardCardPositiveCheck()
        {
            var assertCard = new StandardCard { Content = "test" };
            var response = ResponseBuilder.Empty();
            response.Response.Card = assertCard;
            var StandardCard = AlexaAssertions.StandardCard(response);
            Assert.NotNull(StandardCard);
            Assert.Equal(assertCard, StandardCard);
        }

        [Fact]
        public void HasStandardCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => AlexaAssertions.StandardCard(response));
        }

        [Fact]
        public void HasStandardCardMismatchCheck()
        {
            var response = ResponseBuilder.Empty();
            response.Response.Card = new SimpleCard();
            Assert.Throws<CardMismatchException>(() => AlexaAssertions.StandardCard(response));
        }
    }
}
