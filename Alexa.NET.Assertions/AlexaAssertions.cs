using System;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;

namespace Alexa.NET.Assertions
{
    public static class AlexaAssertions
    {
        public static void Asks(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.AskShouldEndSessionNotTrue);
            }
        }

        public static void Tells(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || !shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.TellShouldEndSessionNotFalse);
            }
        }

        public static void AskPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Asks(response);
            CheckOutput<PlainTextOutputSpeech>(response,r => r?.Response.OutputSpeech,"OutputSpeech",expectedOutput);
        }

        public static void TellPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Tells(response);
            CheckOutput<PlainTextOutputSpeech>(response, r => r?.Response.OutputSpeech, "OutputSpeech",expectedOutput);
        }

        public static void AsksSsml(SkillResponse response, Speech expectedOutput)
        {
            GuardAgainstNull(nameof(expectedOutput), expectedOutput);
            AsksSsml(response, expectedOutput.ToXml());
        }

        public static void AsksSsml(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Asks(response);
            CheckOutput<SsmlOutputSpeech>(response, r => r?.Response.OutputSpeech, "OutputSpeech",expectedOutput);
        }

        public static void TellSsml(SkillResponse response, Speech expectedOutput)
        {
            GuardAgainstNull(nameof(expectedOutput), expectedOutput);
            TellSsml(response, expectedOutput.ToXml());
        }

        public static void TellSsml(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Tells(response);
            CheckOutput<SsmlOutputSpeech>(response, r => r?.Response.OutputSpeech, "OutputSpeech",expectedOutput);
        }

        public static ICard Card(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            if (response?.Response.Card == null)
            {
                throw new CardMissingException(AlexaAssertMessages.CardNotSet);
            }

            return response.Response.Card;
        }

        public static SimpleCard SimpleCard(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var sourceOfTruth = new SimpleCard();
            var card = Card(response);
            if (!(card is SimpleCard))
            {
                throw new CardMismatchException(AlexaAssertMessages.Mismatch(sourceOfTruth.Type, card.Type));
            }

            return (SimpleCard) card;
        }

        public static StandardCard StandardCard(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var sourceOfTruth = new StandardCard();
            var card = Card(response);
            if (!(card is StandardCard))
            {
                throw new CardMismatchException(AlexaAssertMessages.Mismatch(sourceOfTruth.Type, card.Type));
            }

            return (StandardCard) card;
        }

        public static void Reprompt(SkillResponse response)
        {
            GuardAgainstNull(nameof(response),response);
            if (response?.Response.Reprompt == null)
            {
                throw new RepromptMissingException(AlexaAssertMessages.RepromptNotSet);
            }
        }

        public static void RepromptPlainText(SkillResponse response,string expectedReprompt)
        {
            Reprompt(response);
            CheckOutput<PlainTextOutputSpeech>(response,r => r?.Response.Reprompt.OutputSpeech,"Reprompt",expectedReprompt);
        }

        public static void RepromptSsml(SkillResponse response, Speech expectedReprompt)
        {
            RepromptSsml(response,expectedReprompt.ToXml());
        }

        public static void RepromptSsml(SkillResponse response, string expectedReprompt)
        {
            Reprompt(response);
            CheckOutput<SsmlOutputSpeech>(response, r => r?.Response.Reprompt.OutputSpeech, "Reprompt", expectedReprompt);
        }

        private static void CheckOutput<T>(SkillResponse response, Func<SkillResponse,IOutputSpeech> parser, string property, string expectedOutput) where T : class, IOutputSpeech
        {
            var output = parser(response);
            if (output == null)
            {
                throw new OutputMismatchException(property + " not set");
            }

            var outputType = output.GetType();
            if (outputType != typeof(T))
            {
                throw new OutputMismatchException(AlexaAssertMessages.Mismatch(typeof(T).Name, outputType.Name));
            }
            CheckText(output,expectedOutput);
        }

        private static void CheckText(IOutputSpeech speech, string expectedoutput)
        {
            switch (speech)
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
