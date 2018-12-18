using System;
using System.Collections.Generic;
using Alexa.NET.Request;

namespace Alexa.NET.FluentRequests
{

    public class FluentSession
    {
        private FluentRequest Request { get; }

        public FluentSession(FluentRequest fluentRequest)
        {
            Request = fluentRequest;
        }

        public Session Build()
        {
            return new Session
            {
                New = true,
                Attributes = new Dictionary<string, object>(),
                SessionId = "testSession" + Request.NextRandom(),
                User = new User
                {
                    UserId="testUser"+Request.NextRandom(),
                    AccessToken = "testAccessToken"+Request.NextRandom()
                }
            };
        }
    }
}
