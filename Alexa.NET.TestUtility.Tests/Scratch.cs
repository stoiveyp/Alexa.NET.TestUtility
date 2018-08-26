using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Assertions;

namespace Alexa.NET.TestUtility.Tests
{
    class Scratch
    {
        private void ScratchMethod()
        {
            var response = ResponseBuilder.Tell("test");

            AlexaAssertions.AsksWith(response);

        }
    }
}
