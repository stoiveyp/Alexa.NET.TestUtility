using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.FluentRequests;
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

        [Fact]
        public void Session_OverrideNew()
        {
            var fluent = new FluentRequest();
            var session = fluent.Session;
            session.IsNew(false);
            Assert.False(fluent.Request.Session.New);
        }

        [Fact]
        public void Session_OverrideSessionId()
        {
            var fluent = new FluentRequest();
            var session = fluent.Session;
            session.WithSessionId("sess");
            Assert.Equal("sess", fluent.Request.Session.SessionId);
        }

        [Fact]
        public void Session_OverrideUserId()
        {
            var fluent = new FluentRequest();
            var session = fluent.Session;
            session.WithUserId("user123");
            Assert.Equal("user123", fluent.Request.Session.User.UserId);
        }

        [Fact]
        public void Session_OverrideAccessToken()
        {
            var fluent = new FluentRequest();
            var session = fluent.Session;
            session.WithAccessToken("access1");
            Assert.Equal("access1", fluent.Request.Session.User.AccessToken);
        }

        [Fact]
        public void Session_OverrideApplicationId()
        {
            var fluent = new FluentRequest();
            var session = fluent.Session;
            session.WithApplicationId("appid1");
            Assert.Equal("appid1", fluent.Request.Session.Application.ApplicationId);
        }

        [Fact]
        public void Session_AddAttribute()
        {
            var fluent = new FluentRequest();
            var session = fluent.Session;
            session.AddAttribute("attrib1","test");
            Assert.Single(fluent.Request.Session.Attributes);
        }

        [Fact]
        public void Session_AddAttribute_LastOneWins()
        {
            var fluent = new FluentRequest();
            var session = fluent.Session;
            session.AddAttribute("attrib1", "test");
            session.AddAttribute("attrib1", "test2");
            Assert.Single(fluent.Request.Session.Attributes);
            Assert.Equal("test2",fluent.Request.Session.Attributes["attrib1"]);
        }
    }
}
