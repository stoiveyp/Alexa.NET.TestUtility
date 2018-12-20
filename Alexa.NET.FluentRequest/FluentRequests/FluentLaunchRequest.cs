using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request.Type;

namespace Alexa.NET.FluentRequests
{
    public class FluentLaunchRequest:FluentRequest
    {
        public FluentLaunchRequest(FluentSkillRequest skillRequest) : base(skillRequest)
        {
        }

        public override Request.Type.Request Request => new LaunchRequest
        {
            Locale = GetLocale(),
            RequestId = GetRequestId(),
            Timestamp = GetTimestamp()
        };
    }
}
