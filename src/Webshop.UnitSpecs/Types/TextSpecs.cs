using System;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using Webshop.Types;

namespace Webshop.UnitSpecs.Types
{
    public class TextSpecs : FeatureSpecifications
    {
        public override void When() =>
            _success = Text.TryCreate(_rawText, out _result);

        public class When_the_provided_string_is_valid : TextSpecs
        {
            public override void Given() =>
                _rawText = "some valid text";

            [Test]
            public void Should_be_success_the_creation() =>
                _success.Should().BeTrue();

            [Test]
            public void Should_have_the_same_former_value() =>
                _result.Value.Should().Be(_rawText);
        }

        public class When_the_provided_string_is_null : TextSpecs
        {
            public override void Given() =>
                _rawText = null;

            [Test]
            public void Should_not_be_success_the_creation() =>
                _success.Should().BeFalse();
        }

        public class When_the_provided_string_is_empty : TextSpecs
        {
            public override void Given() =>
                _rawText = String.Empty;

            [Test]
            public void Should_not_be_success_the_creation() =>
                _success.Should().BeFalse();
        }

        public class When_the_provided_string_is_only_spaces : TextSpecs
        {
            public override void Given() =>
                _rawText = "   ";

            [Test]
            public void Should_not_be_success_the_creation() =>
                _success.Should().BeFalse();
        }

        string _rawText;
        bool _success;
        Text _result;
    }
}