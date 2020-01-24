using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Assertions;
using Alexa.NET.Response.Directive;
using Xunit;

namespace Alexa.NET.TestUtility.Tests
{
    public class DirectiveTests
    {
        [Fact]
        public void DirectiveWithNullDirectiveThrows()
        {
            var response = ResponseBuilder.Empty();
            Assert.Throws<DirectiveMissingException>(() => response.HasDirective<HintDirective>());
        }

        [Fact]
        public void HasDirectiveMatchingTypeReturnsDirective()
        {
            var directive = new HintDirective();
            var response = ResponseBuilder.Empty();
            response.Response.Directives.Add(directive);
            var output = response.HasDirective<HintDirective>();
            Assert.Equal(directive, output);
        }

        [Fact]
        public void MultipleDirectivesOfTheSameKindReturnsFirst()
        {
            var response = ResponseBuilder.Empty();
            var hint1 = new HintDirective();
            response.Response.Directives.Add(hint1);
            response.Response.Directives.Add(new HintDirective());
            Assert.Equal(hint1,response.HasDirective<HintDirective>());
        }

        [Fact]
        public void MultipleDirectiveWithPredicateFailsOnAmbigous()
        {
            var response = ResponseBuilder.Empty();
            response.Response.Directives.Add(new HintDirective());
            response.Response.Directives.Add(new HintDirective());
            Assert.Throws<AmbiguousDirectiveException>(() => response.HasDirective<HintDirective>(h => true));
        }

        [Fact]
        public void MultipleDirectivesWithFilterReturnCorrectOne()
        {
            var expectedHint = new HintDirective { Hint = new Hint { Text = "test" } };
            var response = ResponseBuilder.Empty();
            response.Response.Directives.Add(new HintDirective());
            response.Response.Directives.Add(expectedHint);

            var result = response.HasDirective<HintDirective>(h => h.Hint?.Text == "test");
            Assert.Equal(expectedHint, result);
        }
    }
}
