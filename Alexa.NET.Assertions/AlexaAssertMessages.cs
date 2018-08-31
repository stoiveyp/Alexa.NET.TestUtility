using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Alexa.NET.TestUtility.Tests")]

namespace Alexa.NET.Assertions
{
    internal class AlexaAssertMessages
    {
        public const string AskShouldEndSessionNotTrue = "To 'ask' Response.ShouldEndSesson needs to be set to false";
        public const string TellShouldEndSessionNotFalse = "To 'tell' Response.ShouldEndSession needs to be set to true";
        public const string CardNotSet = "No card set";
        public const string RepromptNotSet = "No reprompt set";

        public static string Mismatch(string expected, string actual)
        {
            return "Expected: \"" + expected + "\". Actual: \"" + actual + "\"";
        }

        public static string NoDirective(string directiveName)
        {
            return "No directives of type \""+ directiveName +"\"";
        }

        public static string MultipleDirectives(string directiveName)
        {
            return "Multiple directives of type \"" + directiveName + "\" found. Unsure of which to return";
        }
    }
}
