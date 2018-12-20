using Alexa.NET.Request.Type;

namespace Alexa.NET.FluentRequests
{
    public class FluentLaunchRequest:FluentRequest<LaunchRequest>
    {
        public FluentLaunchRequest(FluentSkillRequest skillRequest) : base(skillRequest)
        {
        }
    }
}
