using System;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using Webshop.Types;

namespace Webshop.UnitSpecs.Types
{
    public class TextSpecs : FeatureSpecifications
    {
        public class TryCreateSpecs : TextSpecs
        {
            public override void When() =>
                _success = Text.TryCreate(_rawText, out _result);

            public class When_try_to_create_a_product_with_a_valid_string : TryCreateSpecs
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

            public class When_try_to_create_a_product_with_a_null_string : TryCreateSpecs
            {
                public override void Given() =>
                    _rawText = null;

                [Test]
                public void Should_not_be_success_the_creation() =>
                    _success.Should().BeFalse();
            }

            public class When_try_to_create_a_product_with_an_empty_string : TryCreateSpecs
            {
                public override void Given() =>
                    _rawText = String.Empty;

                [Test]
                public void Should_not_be_success_the_creation() =>
                    _success.Should().BeFalse();
            }

            public class When_try_to_create_a_product_with_only_spaces : TryCreateSpecs
            {
                public override void Given() =>
                    _rawText = "   ";

                [Test]
                public void Should_not_be_success_the_creation() =>
                    _success.Should().BeFalse();
            }

            bool _success;
        }
        public class CreateSpecs : TextSpecs
        {
            public override void When() => _exception = Catch.Exception(() =>
                _result = Text.Create(_rawText)
            );

            public class When_creates_a_product_with_a_valid_string : CreateSpecs
            {
                public override void Given() =>
                    _rawText = "some valid text";

                [Test]
                public void Should_not_throw_an_exception() =>
                    _exception.Should().BeNull();

                [Test]
                public void Should_have_the_same_former_value() =>
                    _result.Value.Should().Be(_rawText);
            }

            public class When_creates_a_product_with_a_null_string : CreateSpecs
            {
                public override void Given() =>
                    _rawText = null;

                [Test]
                public void Should_throw_an_exception() =>
                    _exception.Should().NotBeNull();

                [Test]
                public void Should_throw_an_exception_indicating_the_reason() =>
                    _exception.Message.Should().Be("The provided text does not meet the rules.");
            }

            public class When_creates_a_product_with_an_empty_string : CreateSpecs
            {
                public override void Given() =>
                    _rawText = String.Empty;

                [Test]
                public void Should_throw_an_exception() =>
                    _exception.Should().NotBeNull();

                [Test]
                public void Should_throw_an_exception_indicating_the_reason() =>
                    _exception.Message.Should().Be("The provided text does not meet the rules.");
            }

            public class When_creates_a_product_with_only_spaces : CreateSpecs
            {
                public override void Given() =>
                    _rawText = "   ";

                [Test]
                public void Should_throw_an_exception() =>
                    _exception.Should().NotBeNull();

                [Test]
                public void Should_throw_an_exception_indicating_the_reason() =>
                    _exception.Message.Should().Be("The provided text does not meet the rules.");
            }

            Exception _exception;
        }


        string _rawText;
        Text _result;

    }
}