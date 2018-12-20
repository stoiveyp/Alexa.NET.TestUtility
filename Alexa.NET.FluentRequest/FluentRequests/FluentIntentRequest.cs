using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;

namespace Alexa.NET.FluentRequests
{
    public class FluentIntentRequest:NET.FluentRequests.FluentRequest
    {
        public FluentIntentRequest(FluentSkillRequest skillRequest, string name) : base(skillRequest)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override Request.Type.Request Request => new IntentRequest
        {
            RequestId = GetRequestId(),
            Locale = GetLocale(),
            Timestamp = GetTimestamp(),
            Intent = new Intent {Name = Name}
        };
    }
}
