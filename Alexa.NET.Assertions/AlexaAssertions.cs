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
            CheckOutput<PlainTextOutputSpeech>(response, expectedOutput);
        }

        public static void TellWithPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            TellsWith(response);
            CheckOutput<PlainTextOutputSpeech>(response, expectedOutput);
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
            CheckOutput<SsmlOutputSpeech>(response, expectedOutput);
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
            CheckOutput<SsmlOutputSpeech>(response, expectedOutput);
        }

        public static ICard HasCard(SkillResponse response)
        {
            GuardAgainstNull(nameof(response),response);
            if (response?.Response.Card == null)
            {
                throw new CardMissingException(AlexaAssertMessages.CardNotSet);
            }

            return response.Response.Card;
        }

        public static SimpleCard HasSimpleCard(SkillResponse response)
        {
            GuardAgainstNull(nameof(response),response);
            var sourceOfTruth = new SimpleCard();
            var card = HasCard(response);
            if (!(card is SimpleCard))
            {
                throw new CardMismatchException(AlexaAssertMessages.Mismatch(sourceOfTruth.Type, card.Type));
            }

            return (SimpleCard)card;
        }

        public static StandardCard HasStandardCard(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var sourceOfTruth = new StandardCard();
            var card = HasCard(response);
            if (!(card is StandardCard))
            {
                throw new CardMismatchException(AlexaAssertMessages.Mismatch(sourceOfTruth.Type, card.Type));
            }

            return (StandardCard)card;
        }

        private static void CheckOutput<T>(SkillResponse response, string expectedoutput) where T : class, IOutputSpeech
        {
            if (response?.Response?.OutputSpeech == null)
            {
                throw new OutputMismatchException("OutputSpeech not set");
            }

            var outputType = response.Response.OutputSpeech.GetType();
            if (outputType != typeof(T))
            {
                throw new OutputMismatchException(AlexaAssertMessages.Mismatch(typeof(T).Name,outputType.Name));
            }

            switch (response.Response.OutputSpeech)
            {
                case PlainTextOutputSpeech plain:
                    if (plain.Text != expectedoutput)
                    {
                        throw new OutputMismatchException(AlexaAssertMessages.Mismatch(expectedoutput,plain.Text));
                    }

                    break;
                case SsmlOutputSpeech ssml:
                    if (ssml.Ssml != expectedoutput)
                    {
                        throw new OutputMismatchException(AlexaAssertMessages.Mismatch(expectedoutput,ssml.Ssml));
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
