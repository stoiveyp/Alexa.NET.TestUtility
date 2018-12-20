using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.FluentRequests;

namespace Alexa.NET
{
    public class FluentSkillRequest
    {
        private int nextRandom = 0;
        internal string NextRandom()
        {
            return (++nextRandom).ToString();
        }

        public FluentSession Session { get; }
        public FluentContext Context { get; }

        internal FluentRequest Request { get; set; }
        internal string RequestLocale { get; set; }
        internal DateTime? RequestTimestamp { get; set; }
        internal string RequestId { get; set; }

        public FluentLaunchRequest LaunchRequest()
        {
            var launch = new FluentLaunchRequest(this);
            Request = launch;
            return launch;
        }

        public FluentSkillRequest()
        {
           Session = new FluentSession(this);
           Context = new FluentContext(this);
        }

        public SkillRequest SkillRequest =>  new SkillRequest
            {
                Version = "1.0",
                Session = Session.Session,
                Context = Context.Context,
                Request = Request?.Request
            };
    }
}
