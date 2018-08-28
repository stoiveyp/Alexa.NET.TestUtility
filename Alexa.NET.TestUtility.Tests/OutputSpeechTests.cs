using System;
using Alexa.NET.Assertions;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class OutputSpeechTests
    {
        [Fact]
        public void AskThrowsExceptionOnNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AlexaAssertions.Asks(null));
            Assert.Equal("response", exception.ParamName);
        }

        [Fact]
        public void AskNegativeShouldEndSession()
        {
            var response = ResponseBuilder.Ask("test", null);
            AlexaAssertions.Asks(response);
        }

        [Fact]
        public void AskNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = null;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.Asks(response));
            Assert.Equal(AlexaAssertMessages.AskShouldEndSessionNotTrue, exception.Message);
        }

        [Fact]
        public void AskPositiveShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = true;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.Asks(response));
            Assert.Equal(AlexaAssertMessages.AskShouldEndSessionNotTrue, exception.Message);
        }

        [Fact]
        public void TellsThrowsExceptionOnNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AlexaAssertions.Tells(null));
            Assert.Equal("response", exception.ParamName);
        }

        [Fact]
        public void TellPositiveShouldEndSession()
        {
            var response = ResponseBuilder.Tell("test");
            AlexaAssertions.Tells(response);
        }

        [Fact]
        public void TellNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.Tells(response));
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }

        [Fact]
        public void TellNegativeShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssertions.Tells(response));
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }

        [Fact]
        public void AskWithPlainTextMatchingPhrase()
        {
            var response = ResponseBuilder.Ask("test phrase", null);
            AlexaAssertions.AskPlainText(response, "test phrase");
        }

        [Fact]
        public void AskWithPlainTextMismatchPhrase()
        {
            var response = ResponseBuilder.Ask("test phrase", null);
            Assert.Throws<OutputMismatchException>(() => AlexaAssertions.AskPlainText(response, "not the test phrase"));
        }

        [Fact]
        public void TellWithPlainTextMatchingPhrase()
        {
            var response = ResponseBuilder.Tell("test phrase", null);
            AlexaAssertions.TellPlainText(response, "test phrase");
        }

        [Fact]
        public void TellWithPlainTextMismatchPhrase()
        {
            var response = ResponseBuilder.Tell("test phrase", null);
            Assert.Throws<OutputMismatchException>(() =>
                AlexaAssertions.TellPlainText(response, "not the test phrase"));
        }

        [Fact]
        public void AskWithSsmlMatchingPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.AsksSsml(response, "<speak>test phrase</speak>");
        }

        [Fact]
        public void AskWithSpeechMatchingPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.AsksSsml(response, new Speech(new PlainText("test phrase")));
        }

        [Fact]
        public void AskWithSsmlMismatchPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            var exception = Assert.Throws<OutputMismatchException>(() => AlexaAssertions.AsksSsml(response, "not the test phrase"));
            Assert.Equal("Expected: \"not the test phrase\". Actual: \"<speak>test phrase</speak>\"", exception.Message);
        }

        [Fact]
        public void TellWithSsmlMatchingPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.TellSsml(response, "<speak>test phrase</speak>");
        }

        [Fact]
        public void TellWithSpeechMatchingPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            AlexaAssertions.TellSsml(response, new Speech(new PlainText("test phrase")));
        }

        [Fact]
        public void TellWithSsmlMismatchPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            var exception = Assert.Throws<OutputMismatchException>(() => AlexaAssertions.TellSsml(response, "not the test phrase"));
            Assert.Equal("Expected: \"not the test phrase\". Actual: \"<speak>test phrase</speak>\"", exception.Message);
        }

        [Fact]
        public void HasRepromptNullCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<RepromptMissingException>(() => AlexaAssertions.Reprompt(response));
        }

        [Fact]
        public void AskRepromptPlainTextPositiveCheck()
        {
            var testphrase = "test reprompt";
            var response = ResponseBuilder.Ask("test", new Reprompt(testphrase));
            AlexaAssertions.RepromptPlainText(response,testphrase);
        }

        [Fact]
        public void AskRepromptPlainTextNegativeCheck()
        {
            var response = ResponseBuilder.Ask("test", new Reprompt("invalid phrase"));
            Assert.Throws<OutputMismatchException>(() => AlexaAssertions.RepromptPlainText(response,"test reprompt"));
        }
    }
}
