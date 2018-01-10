using System;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using Webshop.Features.ProductRegistration;
using Webshop.Types;

namespace Webshop.UnitSpecs.Features.ProductRegistration
{
    public class ProductSpecs : FeatureSpecifications
    {
        public override void When() => _exception = Catch.Exception(() =>
            _result = new Product(_harmlessNumber, _harmlessTitle, _price, _harmlessDescription)
        );

        public class When_the_price_is_valid : ProductSpecs
        {
            public override void Given() =>
                _price = 1000;

            [Test]
            public void Should_not_throw_an_exception() =>
                _exception.Should().BeNull();
        }

        public class When_the_price_is_negative : ProductSpecs
        {
            public override void Given() =>
                _price = -1;

            [Test]
            public void Should_throw_an_exception() =>
                _exception.Should().NotBeNull();

            [Test]
            public void Should_throw_an_exception_indicating_the_reason() =>
                _exception.Message.Should().Be("The Price must be between 1 and 1000000000.");
        }

        public class When_the_price_is_zero : ProductSpecs
        {
            public override void Given() =>
                _price = 0;

            [Test]
            public void Should_throw_an_exception() =>
                _exception.Should().NotBeNull();

            [Test]
            public void Should_throw_an_exception_indicating_the_reason() =>
                _exception.Message.Should().Be("The Price must be between 1 and 1000000000.");
        }

        public class When_the_price_is_more_expensive_than_1_million : ProductSpecs
        {
            public override void Given() =>
                _price = 1000000001;

            [Test]
            public void Should_throw_an_exception() =>
                _exception.Should().NotBeNull();

            [Test]
            public void Should_throw_an_exception_indicating_the_reason() =>
                _exception.Message.Should().Be("The Price must be between 1 and 1000000000.");
        }


        decimal _price;
        Product _result;
        Exception _exception;


        static int _harmlessNumber = 1;
        // The next values are protected from failures because the types guarantees that
        static Text _harmlessTitle = Text.Create("Some title");
        static Text _harmlessDescription = Text.Create("Some description");
    }
}