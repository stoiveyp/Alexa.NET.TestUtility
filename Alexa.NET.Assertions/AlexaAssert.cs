using System;
using Alexa.NET.Response;

namespace Alexa.NET.Assertions
{
    public static class AlexaAssert
    {
        public static void AsksWith(SkillResponse response)
        {
            GuardAgainstNull(nameof(response),response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.AskShouldEndSessionNotTrue);
            }
        }

        public static void TellsWith(SkillResponse response)
        {
            GuardAgainstNull(nameof(response),response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || !shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.TellShouldEndSessionNotFalse);
            }
        }

        private static void GuardAgainstNull(string argumentName, object subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
