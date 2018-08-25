using System;
using Alexa.NET.Assertions;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class ResponseAssertionTests
    {
        [Fact]
        public void AskThrowsExceptionOnNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AlexaAssert.AsksWith(null));
            Assert.Equal("response",exception.ParamName);
        }

        [Fact]
        public void AskNegativeShouldEndSession()
        {
            var response = ResponseBuilder.Ask("test", null);
            AlexaAssert.AsksWith(response);
        }

        [Fact]
        public void AskNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = null;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssert.AsksWith(response));
            Assert.Equal(AlexaAssertMessages.AskShouldEndSessionNotTrue, exception.Message);
        }

        [Fact]
        public void AskPositiveShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = true;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssert.AsksWith(response));
            Assert.Equal(AlexaAssertMessages.AskShouldEndSessionNotTrue, exception.Message);
        }

        [Fact]
        public void TellsThrowsExceptionOnNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AlexaAssert.TellsWith(null));
            Assert.Equal("response", exception.ParamName);
        }

        [Fact]
        public void TellPositiveShouldEndSession()
        {
            var response = ResponseBuilder.Tell("test");
            AlexaAssert.TellsWith(response);
        }

        [Fact]
        public void TellNullShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssert.TellsWith(response));
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }

        [Fact]
        public void TellNegativeShouldEndSession()
        {
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            var exception = Assert.Throws<ShouldEndSessionException>(() => AlexaAssert.TellsWith(response));
            Assert.Equal(AlexaAssertMessages.TellShouldEndSessionNotFalse, exception.Message);
        }
    }
}
