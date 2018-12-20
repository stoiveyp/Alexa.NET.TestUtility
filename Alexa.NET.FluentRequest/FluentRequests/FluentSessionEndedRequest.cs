using Alexa.NET.Request.Type;

namespace Alexa.NET.FluentRequests
{
    public class FluentSessionEndedRequest : FluentRequest<SessionEndedRequest>
    {
        public FluentSessionEndedRequest(FluentSkillRequest skillRequest) : base(skillRequest)
        {
        }
    }
}