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
            var card = response.HasCard();
            Assert.NotNull(card);
            Assert.Equal(assertCard, card);
        }

        [Fact]
        public void HasCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => response.HasCard());
        }

        [Fact]
        public void HasSimpleCardPositiveCheck()
        {
            var assertCard = new SimpleCard { Content = "test" };
            var response = ResponseBuilder.Empty();
            response.Response.Card = assertCard;
            var simpleCard = response.HasCard<SimpleCard>();
        }

        [Fact]
        public void HasSimpleCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => response.HasCard<SimpleCard>());
        }

        [Fact]
        public void HasSimpleCardMismatchCheck()
        {
            var response = ResponseBuilder.Empty();
            response.Response.Card = new StandardCard();
            Assert.Throws<CardMismatchException>(() => response.HasCard<SimpleCard>());
        }

        [Fact]
        public void HasStandardCardPositiveCheck()
        {
            var assertCard = new StandardCard { Content = "test" };
            var response = ResponseBuilder.Empty();
            response.Response.Card = assertCard;
            var StandardCard = response.HasCard<StandardCard>();
            Assert.NotNull(StandardCard);
            Assert.Equal(assertCard, StandardCard);
        }

        [Fact]
        public void HasStandardCardNegativeCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<CardMissingException>(() => response.HasCard<StandardCard>());
        }

        [Fact]
        public void HasStandardCardMismatchCheck()
        {
            var response = ResponseBuilder.Empty();
            response.Response.Card = new SimpleCard();
            Assert.Throws<CardMismatchException>(() => response.HasCard<StandardCard>());
        }
    }
}
