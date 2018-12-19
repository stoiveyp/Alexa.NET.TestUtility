using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request.Type;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class FluentContextTests
    {
        [Fact]
        public void Context_OverrideUserId()
        {
            var fluent = new FluentSkillRequest();
            var context = fluent.Context;
            context.WithUserId("user123");
            Assert.Equal("user123", fluent.SkillRequest.Context.System.User.UserId);
        }

        [Fact]
        public void Context_OverrideApiAccessToken()
        {
            var fluent = new FluentSkillRequest();
            var context = fluent.Context;
            context.WithApiAccessToken("apiaccesswibble");
            Assert.Equal("apiaccesswibble", fluent.SkillRequest.Context.System.ApiAccessToken);
        }

        [Fact]
        public void Context_OverridePlaybackState()
        {
            var fluent = new FluentSkillRequest();
            var context = fluent.Context;
            var state = new PlaybackState
            {
                OffsetInMilliseconds = 10,
                Token = "wibble"
            };
            context.WithPlaybackState(state);
            Assert.Equal(state, fluent.SkillRequest.Context.AudioPlayer);
        }


        [Fact]
        public void Context_OverrideAccessToken()
        {
            var fluent = new FluentSkillRequest();
            var context = fluent.Context;
            context.WithAccessToken("access1");
            Assert.Equal("access1", fluent.SkillRequest.Context.System.User.AccessToken);
        }

        [Fact]
        public void Context_OverrideApplicationId()
        {
            var fluent = new FluentSkillRequest();
            var context = fluent.Context;
            context.WithApplicationId("appid1");
            Assert.Equal("appid1", fluent.SkillRequest.Context.System.Application.ApplicationId);
        }

        [Fact]
        public void Context_OverrideDeviceId()
        {
            var fluent = new FluentSkillRequest();
            var context = fluent.Context;
            context.WithDeviceId("devicetest");
            Assert.Equal("devicetest", fluent.SkillRequest.Context.System.Device.DeviceID);
        }

        [Fact]
        public void Context_AddDeviceSupport()
        {
            var fluent = new FluentSkillRequest();
            var context = fluent.Context;
            context.AddInterface("testInterface");
            Assert.Single(fluent.SkillRequest.Context.System.Device.SupportedInterfaces);
            Assert.True(fluent.SkillRequest.Context.System.Device.IsInterfaceSupported("testInterface"));
        }

    }
}
