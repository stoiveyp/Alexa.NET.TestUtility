using System;
using System.Collections.Generic;
using Alexa.NET.Request;

namespace Alexa.NET.FluentRequests
{
    public class FluentSession
    {
        private FluentSkillRequest SkillRequest { get; }
        internal Session Session { get; }

        internal FluentSession(FluentSkillRequest fluentSkillRequest)
        {
            SkillRequest = fluentSkillRequest;
            Session = new Session
            {
                New = true,
                Attributes = new Dictionary<string, object>(),
                SessionId = "testSession" + SkillRequest.NextRandom(),
                User = new User
                {
                    UserId = "testUser" + SkillRequest.NextRandom(),
                    AccessToken = "testAccessToken" + SkillRequest.NextRandom()
                },
                Application = new Application
                {
                    ApplicationId = "testApplication" + SkillRequest.NextRandom()
                }
            };
        }

        public FluentSession IsNew(bool value)
        {
            Session.New = value;
            return this;
        }

        public FluentSession WithSessionId(string sessionId)
        {
            Session.SessionId = sessionId;
            return this;
        }

        public FluentSession WithUserId(string userId)
        {
            Session.User.UserId = userId;
            return this;
        }

        public FluentSession WithAccessToken(string accessToken)
        {
            Session.User.AccessToken = accessToken;
            return this;
        }

        public FluentSession WithApplicationId(string applicationId)
        {
            Session.Application.ApplicationId = applicationId;
            return this;
        }

        public FluentSession AddAttribute(string key, object value)
        {
            if (Session.Attributes.ContainsKey(key))
            {
                Session.Attributes.Remove(key);
            }
            Session.Attributes.Add(key,value);
            return this;
        }

        public FluentSession WithAttributes(Dictionary<string, object> attributes)
        {
            Session.Attributes = attributes;
            return this;
        }

        public FluentSkillRequest And => SkillRequest;
    }
}
