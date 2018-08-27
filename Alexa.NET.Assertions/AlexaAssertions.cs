using System;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;

namespace Alexa.NET.Assertions
{
    public static class AlexaAssertions
    {
        public static void AsksWith(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.AskShouldEndSessionNotTrue);
            }
        }

        public static void TellsWith(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || !shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.TellShouldEndSessionNotFalse);
            }
        }

        public static void AsksWithPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            AsksWith(response);
            CheckText<PlainTextOutputSpeech>(response, expectedOutput);
        }

        public static void TellWithPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            TellsWith(response);
            CheckText<PlainTextOutputSpeech>(response, expectedOutput);
        }

        public static void AsksWithSsml(SkillResponse response, Speech expectedOutput)
        {
            GuardAgainstNull(nameof(expectedOutput), expectedOutput);
            AsksWithSsml(response,expectedOutput.ToXml());
        }

        public static void AsksWithSsml(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            AsksWith(response);
            CheckText<SsmlOutputSpeech>(response, expectedOutput);
        }

        public static void TellWithSsml(SkillResponse response, Speech expectedOutput)
        {
            GuardAgainstNull(nameof(expectedOutput),expectedOutput);
            TellWithSsml(response, expectedOutput.ToXml());
        }

        public static void TellWithSsml(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            TellsWith(response);
            CheckText<SsmlOutputSpeech>(response, expectedOutput);
        }


        private static void CheckText<T>(SkillResponse response, string expectedoutput) where T : class, IOutputSpeech
        {
            if (response?.Response?.OutputSpeech == null)
            {
                throw new OutputMismatchException("OutputSpeech not set");
            }

            var outputType = response.Response.OutputSpeech.GetType();
            if (outputType != typeof(T))
            {
                throw new OutputMismatchException($"Expected: \"{typeof(T).Name}\". Actual: \"{outputType.Name}\"");
            }

            switch (response.Response.OutputSpeech)
            {
                case PlainTextOutputSpeech plain:
                    if (plain.Text != expectedoutput)
                    {
                        throw new OutputMismatchException($"Expected: \"{expectedoutput}\". Actual: \"{plain.Text}\"");
                    }

                    break;
                case SsmlOutputSpeech ssml:
                    if (ssml.Ssml != expectedoutput)
                    {
                        throw new OutputMismatchException($"Expected: \"{expectedoutput}\". Actual: \"{ssml.Ssml}\"");
                    }

                    break;
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
