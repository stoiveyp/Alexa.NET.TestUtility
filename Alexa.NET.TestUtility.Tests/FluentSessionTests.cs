using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.FluentRequests;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class FluentSessionTests
    {
        [Fact]
        public void Session_OverrideNew()
        {
            var fluent = new FluentSkillRequest();
            var session = fluent.Session;
            session.IsNew(false);
            Assert.False(fluent.SkillRequest.Session.New);
        }

        [Fact]
        public void Session_OverrideSessionId()
        {
            var fluent = new FluentSkillRequest();
            var session = fluent.Session;
            session.WithSessionId("sess");
            Assert.Equal("sess", fluent.SkillRequest.Session.SessionId);
        }

        [Fact]
        public void Session_OverrideUserId()
        {
            var fluent = new FluentSkillRequest();
            var session = fluent.Session;
            session.WithUserId("user123");
            Assert.Equal("user123", fluent.SkillRequest.Session.User.UserId);
        }

        [Fact]
        public void Session_OverrideAccessToken()
        {
            var fluent = new FluentSkillRequest();
            var session = fluent.Session;
            session.WithAccessToken("access1");
            Assert.Equal("access1", fluent.SkillRequest.Session.User.AccessToken);
        }

        [Fact]
        public void Session_OverrideApplicationId()
        {
            var fluent = new FluentSkillRequest();
            var session = fluent.Session;
            session.WithApplicationId("appid1");
            Assert.Equal("appid1", fluent.SkillRequest.Session.Application.ApplicationId);
        }

        [Fact]
        public void Session_AddAttribute()
        {
            var fluent = new FluentSkillRequest();
            var session = fluent.Session;
            session.AddAttribute("attrib1", "test");
            Assert.Single(fluent.SkillRequest.Session.Attributes);
        }

        [Fact]
        public void Session_AddAttribute_LastOneWins()
        {
            var fluent = new FluentSkillRequest();
            var session = fluent.Session;
            session.AddAttribute("attrib1", "test");
            session.AddAttribute("attrib1", "test2");
            Assert.Single(fluent.SkillRequest.Session.Attributes);
            Assert.Equal("test2", fluent.SkillRequest.Session.Attributes["attrib1"]);
        }
    }
}
