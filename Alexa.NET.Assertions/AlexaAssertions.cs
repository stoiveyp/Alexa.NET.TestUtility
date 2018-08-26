using System;
using Alexa.NET.Response;

namespace Alexa.NET.Assertions
{
    public static class AlexaAssertions
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

        public static void AsksWithPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response),response);
            AsksWith(response);
            CheckPlainText(response,expectedOutput);
        }

        public static void TellWithPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            TellsWith(response);
            CheckPlainText(response, expectedOutput);
        }

        private static void CheckPlainText(SkillResponse response, string expectedoutput)
        {
            if (response?.Response?.OutputSpeech == null)
            {
                throw new OutputMismatchException("OutputSpeech not set");
            }

            var speech = response?.Response?.OutputSpeech as PlainTextOutputSpeech;
            if (speech == null)
            {
                throw new OutputMismatchException("Output isn't plain text");
            }

            if (speech.Text != expectedoutput)
            {
                throw new OutputMismatchException($"Expected: \"{expectedoutput}\". Actual: \"{speech.Text}\"");
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
