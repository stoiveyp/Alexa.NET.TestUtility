using System;
using Alexa.NET.Assertions;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class AskAndTellTests
    {
        [Fact]
        public void AskThrowsExceptionOnNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AlexaAssertions.AsksWith(null));
            Assert.Equal("response", exception.ParamName);
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
            var response = ResponseBuilder.Ask("test phrase", null);
            AlexaAssertions.AsksWithPlainText(response, "test phrase");
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

        [Fact]
        public void AskWithSsmlMatchingPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.AsksWithSsml(response, "<speak>test phrase</speak>");
        }

        [Fact]
        public void AskWithSpeechMatchingPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.AsksWithSsml(response, new Speech(new PlainText("test phrase")));
        }

        [Fact]
        public void AskWithSsmlMismatchPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            var exception = Assert.Throws<OutputMismatchException>(() => AlexaAssertions.AsksWithSsml(response, "not the test phrase"));
            Assert.Equal("Expected: \"not the test phrase\". Actual: \"<speak>test phrase</speak>\"", exception.Message);
        }

        [Fact]
        public void TellWithSsmlMatchingPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.TellWithSsml(response, "<speak>test phrase</speak>");
        }

        [Fact]
        public void TellWithSpeechMatchingPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.TellWithSsml(response, new Speech(new PlainText("test phrase")));
        }

        [Fact]
        public void TellWithSsmlMismatchPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            var exception = Assert.Throws<OutputMismatchException>(() => AlexaAssertions.TellWithSsml(response, "not the test phrase"));
            Assert.Equal("Expected: \"not the test phrase\". Actual: \"<speak>test phrase</speak>\"", exception.Message);
        }
    }
}
