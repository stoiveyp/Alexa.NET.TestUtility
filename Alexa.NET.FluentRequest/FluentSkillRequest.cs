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

        public FluentRequest Request { get; set; }


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
