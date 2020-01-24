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
            response.Asks();
        }

        [Fact]
        public void AskNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = null;
            var exception = Assert.Throws<ShouldEndSessionException>(() => response.Asks());
            Assert.Equal(AlexaAssertMessages.AskShouldEndSessionNotTrue, exception.Message);
        }

        [Fact]
        public void AskPositiveShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = true;
            var exception = Assert.Throws<ShouldEndSessionException>(() => response.Asks());
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
            response.Tells();
        }

        [Fact]
        public void TellNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => response.Tells());
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }

        [Fact]
        public void TellNegativeShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => response.Tells());
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }

        [Fact]
        public void AskWithPlainTextMatchingPhrase()
        {
            var response = ResponseBuilder.Ask("test phrase", null);
            response.Asks<PlainTextOutputSpeech>(s => s.Text == "test phrase");
        }

        [Fact]
        public void AskWithPlainTextMismatchPhrase()
        {
            var response = ResponseBuilder.Ask("test phrase", null);
            Assert.Throws<PredicateFailedException>(() => response.Asks<PlainTextOutputSpeech>(s => s.Text == "not the test phrase"));
        }

        [Fact]
        public void TellWithPlainTextMatchingPhrase()
        {
            var response = ResponseBuilder.Tell("test phrase", null);
            response.Tells<PlainTextOutputSpeech>(s => s.Text == "test phrase");
        }

        [Fact]
        public void TellWithPlainTextMismatchPhrase()
        {
            var response = ResponseBuilder.Tell("test phrase", null);
            Assert.Throws<OutputMismatchException>(() =>
                response.Tells<SsmlOutputSpeech>());
        }

        [Fact]
        public void AskWithSsmlMatchingPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            response.Asks<SsmlOutputSpeech>(s => s.Ssml == "<speak>test phrase</speak>");
        }

        [Fact]
        public void AskWithSpeechMatchingPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            response.Asks<SsmlOutputSpeech>(s => s.Ssml == new Speech(new PlainText("test phrase")).ToXml());
        }

        [Fact]
        public void AskWithSsmlMismatchPhrase()
        {
            var response = ResponseBuilder.Ask(new Speech(new PlainText("test phrase")), null);
            Assert.Throws<PredicateFailedException>(() => response.Asks<SsmlOutputSpeech>(s => s.Ssml ==  "not the test phrase"));
        }

        [Fact]
        public void TellWithSsmlMatchingPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            response.Tells<SsmlOutputSpeech>(s => s.Ssml == "<speak>test phrase</speak>");
        }

        [Fact]
        public void TellWithSpeechMatchingPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            response.Tells<SsmlOutputSpeech>(s => s.Ssml == new Speech(new PlainText("test phrase")).ToXml());
        }

        [Fact]
        public void TellWithSsmlMismatchPhrase()
        {
            var response = ResponseBuilder.Tell(new Speech(new PlainText("test phrase")), null);
            Assert.Throws<PredicateFailedException>(() => response.Tells<SsmlOutputSpeech>(s => s.Ssml == "not the test phrase"));
        }

        [Fact]
        public void HasRepromptNullCheck()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<RepromptMissingException>(() => response.HasReprompt());
        }

        [Fact]
        public void AskRepromptPlainTextPositiveCheck()
        {
            var testphrase = "test reprompt";
            var response = ResponseBuilder.Ask("test", new Reprompt(testphrase));
            response.HasReprompt<PlainTextOutputSpeech>(p => p.Text.Contains(testphrase));
        }

        [Fact]
        public void AskRepromptPlainTextNegativeCheck()
        {
            var response = ResponseBuilder.Ask("test", new Reprompt("invalid phrase"));
            Assert.Throws<PredicateFailedException>(() => response.HasReprompt<PlainTextOutputSpeech>(pt => pt.Text == "test reprompt"));
        }
    }
}
