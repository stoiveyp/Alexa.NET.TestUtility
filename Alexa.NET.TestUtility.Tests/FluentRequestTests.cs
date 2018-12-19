using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class FluentRequestTests
    {
        [Fact]
        public void EmptyGeneratesSkillRequest()
        {
            var request = new FluentRequest().Request;
            Assert.NotNull(request);
            Assert.NotNull(request.Session);

            var session = request.Session;
            Assert.True(session.New);
            Assert.Equal("testSession1", session.SessionId);
            Assert.Equal("testUser2", session.User.UserId);
            Assert.Equal("testAccessToken3", session.User.AccessToken);
            Assert.Equal("testApplication4", session.Application.ApplicationId);
            Assert.Equal("1.0", request.Version);
        }
    }
}
