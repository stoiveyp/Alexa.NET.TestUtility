using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;

namespace Alexa.NET.FluentRequests
{
    public abstract class FluentRequest<T>:FluentRequest where T : Request.Type.Request,new()
    {
        public override Request.Type.Request Request => new T
        {
            Locale = GetLocale(),
            RequestId = GetRequestId(),
            Timestamp = GetTimestamp()
        };

        protected FluentRequest(FluentSkillRequest skillRequest) : base(skillRequest)
        {
        }
    }

    public abstract class FluentRequest
    {
        protected FluentSkillRequest SkillRequest { get; }

        protected FluentRequest(FluentSkillRequest skillRequest)
        {
            SkillRequest = skillRequest;
            SkillRequest.Request = this;
        }

        public FluentSkillRequest And => SkillRequest;

        public abstract Request.Type.Request Request { get; }

        public FluentRequest WithLocale(string locale)
        {
            SkillRequest.RequestLocale = locale;
            return this;
        }
        public FluentRequest WithRequestId(string requestId)
        {
            SkillRequest.RequestId = requestId;
            return this;
        }

        public FluentRequest WithTimestamp(DateTime timestamp)
        {
            SkillRequest.RequestTimestamp = timestamp;
            return this;
        }

        protected string GetLocale() => SkillRequest.RequestLocale ?? "en-US";

        protected string GetRequestId() => SkillRequest.RequestId ?? "requestId" + SkillRequest.NextRandom();

        protected DateTime GetTimestamp() => SkillRequest.RequestTimestamp ?? DateTime.Now;

        //Locale
        //RequestId
        //Timestamp
    }
}
