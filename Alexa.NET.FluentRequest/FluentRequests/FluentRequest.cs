using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;

namespace Alexa.NET.FluentRequests
{
    public abstract class FluentRequest
    {
        protected FluentSkillRequest SkillRequest { get; }

        protected FluentRequest(FluentSkillRequest skillRequest)
        {
            SkillRequest = skillRequest;
        }

        public FluentSkillRequest And => SkillRequest;

        public abstract Request.Type.Request Request { get; }
    }
}
