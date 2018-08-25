using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly:InternalsVisibleTo("Alexa.NET.TestUtility.Tests")]

namespace Alexa.NET.Assertions
{
    internal class AlexaAssertMessages
    {
        public const string AskShouldEndSessionNotTrue = "To 'ask' Response.ShouldEndSesson needs to be set to false";
        public const string TellShouldEndSessionNotFalse = "To 'tell' Response.ShouldEndSession needs to be set to true";
    }
}
