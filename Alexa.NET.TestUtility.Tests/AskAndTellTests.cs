using System;
using Alexa.NET.Assertions;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class AskAndTellTests
    {
        [Fact]
        public void AskThrowsExceptionOnNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AlexaAssertions.AsksWith(null));
            Assert.Equal("response",exception.ParamName);
        }

        [Fact]
        public void AskNegativeShouldEndSession()
        {
            var response = ResponseBuilder.Ask("test", null);
            AlexaAssertions.AsksWith(response);
        }

        [Fact]
        public void AskNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = null;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.AsksWith(response));
            Assert.Equal(AlexaAssertMessages.AskShouldEndSessionNotTrue, exception.Message);
        }

        [Fact]
        public void AskPositiveShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = true;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.AsksWith(response));
            Assert.Equal(AlexaAssertMessages.AskShouldEndSessionNotTrue, exception.Message);
        }

        [Fact]
        public void TellsThrowsExceptionOnNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AlexaAssertions.TellsWith(null));
            Assert.Equal("response", exception.ParamName);
        }

        [Fact]
        public void TellPositiveShouldEndSession()
        {
            var response = ResponseBuilder.Tell("test");
            AlexaAssertions.TellsWith(response);
        }

        [Fact]
        public void TellNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.TellsWith(response));
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }

        [Fact]
        public void TellNegativeShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.TellsWith(response));
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }

        [Fact]
        public void AskWithPlainTextMatchingPhrase()
        {
            var response = ResponseBuilder.Ask("test phrase",null);
            AlexaAssertions.AsksWithPlainText(response,"test phrase");
        }

        [Fact]
        public void AskWithPlainTextMismatchPhrase()
        {
            var response = ResponseBuilder.Ask("test phrase", null);
            Assert.Throws<OutputMismatchException>(() => AlexaAssertions.AsksWithPlainText(response, "not the test phrase"));
        }

        [Fact]
        public void TellWithPlainTextMatchingPhrase()
        {
            var response = ResponseBuilder.Tell("test phrase", null);
            AlexaAssertions.TellWithPlainText(response, "test phrase");
        }

        [Fact]
        public void TellWithPlainTextMismatchPhrase()
        {
            var response = ResponseBuilder.Tell("test phrase", null);
            Assert.Throws<OutputMismatchException>(() =>
                AlexaAssertions.TellWithPlainText(response, "not the test phrase"));
        }
    }
}
