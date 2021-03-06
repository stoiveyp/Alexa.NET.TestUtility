﻿using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class FluentRequestTests
    {
        [Fact]
        public void EmptyGeneratesSkillRequest()
        {
            var request = new FluentSkillRequest().SkillRequest;
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
        public void LaunchRequest()
        {
            var request = new FluentSkillRequest().LaunchRequest().And;
            Assert.IsType<LaunchRequest>(request.SkillRequest.Request);
            Assert.Equal("en-US",request.SkillRequest.Request.Locale);
            Assert.Equal("requestId9",request.SkillRequest.Request.RequestId);
            Assert.True(request.SkillRequest.Request.Timestamp > DateTime.Now.AddMinutes(-1));
        }

        [Fact]
        public void SessionEndedRequest()
        {
            var request = new FluentSkillRequest().SessionEndedRequest().And;
            Assert.IsType<SessionEndedRequest>(request.SkillRequest.Request);
        }

        [Fact]
        public void IntentRequestRequiresName()
        {
            Assert.Throws<ArgumentNullException>(() => new FluentSkillRequest().IntentRequest(null));
        }

        [Fact]
        public void IntentRequestSetsName()
        {
            var request = new FluentSkillRequest().IntentRequest(BuiltInIntent.Help).And;
            var intent = Assert.IsType<IntentRequest>(request.SkillRequest.Request);
            Assert.Equal(BuiltInIntent.Help,intent.Intent.Name);
        }

        [Fact]
        public void IntentRequestSetsDialogStatus()
        {
            var request = new FluentSkillRequest().IntentRequest(BuiltInIntent.Help,DialogState.Completed).And;
            var intent = Assert.IsType<IntentRequest>(request.SkillRequest.Request);
            Assert.Equal(DialogState.Completed,intent.DialogState);

        }

        [Fact]
        public void IntentRequestSetsConfirmationStatus()
        {
            var request = new FluentSkillRequest().IntentRequest(BuiltInIntent.Help).WithConfirmationStatus(ConfirmationStatus.Confirmed).And;
            var intent = Assert.IsType<IntentRequest>(request.SkillRequest.Request);
            Assert.Equal(ConfirmationStatus.Confirmed,intent.Intent.ConfirmationStatus);
        }

        [Fact]
        public void AddSlotAddsKeyAndValue()
        {
            var request = new FluentSkillRequest().IntentRequest(BuiltInIntent.Help);
            request.AddSlot("test", "value");
            var intent = Assert.IsType<IntentRequest>(request.And.SkillRequest.Request);
            Assert.Single(intent.Intent.Slots);
            Assert.Equal("test",intent.Intent.Slots["test"].Name);
            Assert.Equal("value",intent.Intent.Slots["test"].Value);
        }

        [Fact]
        public void AddSlotAddsSlot()
        {
            var slot = new Slot {Name = "test"};
            var request = new FluentSkillRequest().IntentRequest(BuiltInIntent.Help);
            request.AddSlot(slot);
            var intent = Assert.IsType<IntentRequest>(request.And.SkillRequest.Request);
            Assert.Single(intent.Intent.Slots);
            Assert.Equal(slot, intent.Intent.Slots["test"]);
        }

    }
}
