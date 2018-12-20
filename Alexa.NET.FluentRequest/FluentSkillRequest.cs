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
            return new FluentLaunchRequest(this);
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

        public FluentSessionEndedRequest SessionEndedRequest()
        {
            return new FluentSessionEndedRequest(this);
        }

        public FluentIntentRequest IntentRequest(string intentName, string dialogState)
        {
            var intent = IntentRequest(intentName);
            intent.WithDialogState(dialogState);
            return intent;
        }

        public FluentIntentRequest IntentRequest(string intentName)
        {
            if (string.IsNullOrWhiteSpace(intentName))
            {
                throw new ArgumentNullException(nameof(intentName));
            }
            return new FluentIntentRequest(this,intentName);
        }
    }
}
