using System;
using System.Linq;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;

namespace Alexa.NET.Assertions
{
    public static class AlexaAssertions
    {
        public static IOutputSpeech Asks(this SkillResponse response, Predicate<IOutputSpeech> predicate = null)
        {
            return Asks<IOutputSpeech>(response, predicate);
        }

        //TODO: Add tests
        public static TOutput Asks<TOutput>(this SkillResponse response, Predicate<TOutput> predicate = null) where TOutput : class, IOutputSpeech
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.AskShouldEndSessionNotTrue);
            }

            var output = response.Response?.OutputSpeech;

            if (!(output is TOutput))
            {
                throw new OutputMismatchException(AlexaAssertMessages.Mismatch(typeof(TOutput).FullName, output?.GetType().FullName ?? "null"));
            }

            if (predicate != null && !predicate(output as TOutput))
            {
                throw new PredicateFailedException();
            }

            return output as TOutput;
        }

        public static IOutputSpeech Tells(this SkillResponse response, Predicate<IOutputSpeech> predicate = null)
        {
            return Tells<IOutputSpeech>(response, predicate);
        }

        //TODO: Add tests
        public static TOutput Tells<TOutput>(this SkillResponse response, Predicate<TOutput> predicate = null) where TOutput : class, IOutputSpeech
        {
            GuardAgainstNull(nameof(response), response);
            var shouldEnd = response?.Response?.ShouldEndSession;
            if (!shouldEnd.HasValue || !shouldEnd.Value)
            {
                throw new ShouldEndSessionException(AlexaAssertMessages.TellShouldEndSessionNotFalse);
            }

            var output = response.Response?.OutputSpeech;

            if (!(output is TOutput))
            {
                throw new OutputMismatchException(AlexaAssertMessages.Mismatch(typeof(TOutput).FullName, output?.GetType().FullName ?? "null"));
            }

            if (predicate != null && !predicate(output as TOutput))
            {
                throw new PredicateFailedException();
            }

            return output as TOutput;
        }

        public static ICard HasCard(this SkillResponse response, Predicate<ICard> predicate = null)
        {
            return HasCard<ICard>(response, predicate);
        }

        public static ICard HasCard<TCard>(this SkillResponse response, Predicate<TCard> predicate = null) where TCard : class, ICard
        {
            GuardAgainstNull(nameof(response), response);
            if (response.Response?.Card == null)
            {
                throw new CardMissingException(AlexaAssertMessages.CardNotSet);
            }

            var card = response.Response.Card;

            if (!(card is TCard))
            {
                throw new CardMismatchException(AlexaAssertMessages.Mismatch(typeof(TCard).FullName, card?.GetType().FullName ?? "null"));
            }

            if (predicate != null && !predicate(response.Response.Card as TCard))
            {
                throw new PredicateFailedException();
            }

            return response.Response.Card;
        }

        public static Reprompt HasReprompt(this SkillResponse response, Predicate<IOutputSpeech> predicate = null)
        {
            return HasReprompt<IOutputSpeech>(response, predicate);
        }

        public static Reprompt HasReprompt<TOutput>(this SkillResponse response, Predicate<TOutput> predicate = null) where TOutput : class, IOutputSpeech
        {
            GuardAgainstNull(nameof(response), response);
            if (response.Response.Reprompt == null)
            {
                throw new RepromptMissingException(AlexaAssertMessages.RepromptNotSet);
            }

            var output = response.Response.Reprompt.OutputSpeech;
            if (!(output is TOutput))
            {
                throw new OutputMismatchException(AlexaAssertMessages.Mismatch(typeof(TOutput).FullName, output?.GetType().FullName ?? "null"));
            }

            if (predicate != null && !predicate(output as TOutput))
            {
                throw new PredicateFailedException();
            }

            return response.Response.Reprompt;
        }

        public static T HasDirective<T>(this SkillResponse response, Predicate<T> predicate = null) where T : IDirective
        {
            GuardAgainstNull(nameof(response), response);
            var directives = response?.Response?.Directives?.OfType<T>().Where(d => predicate?.Invoke(d) ?? true).ToArray() ?? new T[] { };

            if (directives.Length == 0)
            {
                throw new DirectiveMissingException(AlexaAssertMessages.NoDirective(typeof(T).Name));
            }

            if (predicate != null && directives.Length > 1)
            {
                throw new AmbiguousDirectiveException(AlexaAssertMessages.MultipleDirectives(typeof(T).Name));
            }

            return directives.First();
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
