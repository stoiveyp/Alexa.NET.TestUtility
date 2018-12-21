using System;
using System.Linq;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;

namespace Alexa.NET.Assertions
{
    public static class AlexaAssertions
    {
        public static IOutputSpeech Ask(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.AskShouldEndSessionNotTrue);
            }

            return response.Response.OutputSpeech;
        }

        public static IOutputSpeech Tell(SkillResponse response)
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || !shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.TellShouldEndSessionNotFalse);
            }
            return response.Response.OutputSpeech;
        }

        public static IOutputSpeech AskPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Ask(response);
            CheckOutput<PlainTextOutputSpeech>(response,r => r?.Response.OutputSpeech,"OutputSpeech",expectedOutput);
            return response.Response.OutputSpeech;
        }

        public static IOutputSpeech TellPlainText(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Tell(response);
            CheckOutput<PlainTextOutputSpeech>(response, r => r?.Response.OutputSpeech, "OutputSpeech",expectedOutput);
            return response.Response.OutputSpeech;
        }

        public static IOutputSpeech AskSsml(SkillResponse response, Speech expectedOutput)
        {
            GuardAgainstNull(nameof(expectedOutput), expectedOutput);
            AskSsml(response, expectedOutput.ToXml());
            return response.Response.OutputSpeech;
        }

        public static IOutputSpeech AskSsml(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Ask(response);
            CheckOutput<SsmlOutputSpeech>(response, r => r?.Response.OutputSpeech, "OutputSpeech",expectedOutput);
            return response.Response.OutputSpeech;
        }

        public static void TellSsml(SkillResponse response, Speech expectedOutput)
        {
            GuardAgainstNull(nameof(expectedOutput), expectedOutput);
            TellSsml(response, expectedOutput.ToXml());
        }

        public static IOutputSpeech TellSsml(SkillResponse response, string expectedOutput)
        {
            GuardAgainstNull(nameof(response), response);
            Tell(response);
            CheckOutput<SsmlOutputSpeech>(response, r => r?.Response.OutputSpeech, "OutputSpeech",expectedOutput);
            return response.Response.OutputSpeech;
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

        public static Reprompt Reprompt(SkillResponse response)
        {
            GuardAgainstNull(nameof(response),response);
            if (response?.Response.Reprompt == null)
            {
                throw new RepromptMissingException(AlexaAssertMessages.RepromptNotSet);
            }

            return response.Response.Reprompt;
        }

        public static Reprompt RepromptPlainText(SkillResponse response,string expectedReprompt)
        {
            Reprompt(response);
            CheckOutput<PlainTextOutputSpeech>(response,r => r?.Response.Reprompt.OutputSpeech,"Reprompt",expectedReprompt);
            return response.Response.Reprompt;
        }

        public static Reprompt RepromptSsml(SkillResponse response, Speech expectedReprompt)
        {
            RepromptSsml(response,expectedReprompt.ToXml());
            return response.Response.Reprompt;
        }

        public static Reprompt RepromptSsml(SkillResponse response, string expectedReprompt)
        {
            Reprompt(response);
            CheckOutput<SsmlOutputSpeech>(response, r => r?.Response.Reprompt.OutputSpeech, "Reprompt", expectedReprompt);
            return response.Response.Reprompt;
        }

        public static T Directive<T>(SkillResponse response) where T : IDirective
        {
            return Directive<T>(response, t => true);
        }

        public static T Directive<T>(SkillResponse response, Func<T,bool> filter) where T:IDirective
        {
            GuardAgainstNull(nameof(response),response);
            var directives = response?.Response?.Directives?.OfType<T>().Where(filter).ToArray() ?? new T[]{};

            if (directives.Length  == 0)
            {
                throw new DirectiveMissingException(AlexaAssertMessages.NoDirective(typeof(T).Name));
            }

            if (directives.Length > 1)
            {
                throw new AmbiguousDirectiveException(AlexaAssertMessages.MultipleDirectives(typeof(T).Name));
            }

            return directives.First();
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
