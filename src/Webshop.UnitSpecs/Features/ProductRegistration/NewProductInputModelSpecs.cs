using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using Webshop.Features.ProductRegistration;
using Webshop.UnitSpecs.Helpers;

namespace Webshop.UnitSpecs.Features.ProductRegistration
{
    public class NewProductInputModelSpecs : FeatureSpecifications
    {
        public override void When() =>
            _isValid = Validator.TryValidateObject(_model, new ValidationContext(_model), _validationResults, true);

        // It's no necessary to test the happy path because it's already included at the acceptance test level

        public class When_the_number_is_negative : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(number: -1);

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_number() =>
                _validationResults.First().MemberNames.First().Should().Be("Number");

            [Test]
            public void Should_be_considered_invalid_because_the_number_is_not_positive() =>
                _validationResults.First().ErrorMessage.Should().Be("The field Number must be between 1 and 2147483647.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_the_number_is_zero : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(number: 0);

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_number() =>
                _validationResults.First().MemberNames.First().Should().Be("Number");

            [Test]
            public void Should_be_considered_invalid_because_the_number_is_not_positive() =>
                _validationResults.First().ErrorMessage.Should().Be("The field Number must be between 1 and 2147483647.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_the_title_was_not_provided : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(title: null); // It could be tested also cases like empty and spaces

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_title() =>
                _validationResults.First().MemberNames.First().Should().Be("Title");

            [Test]
            public void Should_be_considered_invalid_because_the_title_was_not_provided() =>
                _validationResults.First().ErrorMessage.Should().Be("The Title field is required.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_the_title_has_more_than_50_characters : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(title: "123456789012345678901234567890123456789012345678901");

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_title() =>
                _validationResults.First().MemberNames.First().Should().Be("Title");

            [Test]
            public void Should_be_considered_invalid_because_the_text_is_too_long() =>
                _validationResults.First().ErrorMessage.Should().Be("The field Title must be a string with a maximum length of 50.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_the_price_is_negative : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(price: -1);

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_price() =>
                _validationResults.First().MemberNames.First().Should().Be("Price");

            [Test]
            public void Should_be_considered_invalid_because_the_price_is_not_positive() =>
                _validationResults.First().ErrorMessage.Should().Be("The field Price must be between 1 and 1000000000.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_the_price_is_zero : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(price: 0);

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_price() =>
                _validationResults.First().MemberNames.First().Should().Be("Price");

            [Test]
            public void Should_be_considered_invalid_because_the_price_is_not_positive() =>
                _validationResults.First().ErrorMessage.Should().Be("The field Price must be between 1 and 1000000000.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_the_price_is_more_expensive_than_1_million : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(price: 1000000001);

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_price() =>
                _validationResults.First().MemberNames.First().Should().Be("Price");

            [Test]
            public void Should_be_considered_invalid_because_the_price_exceeds_the_maximum_amount() =>
                _validationResults.First().ErrorMessage.Should().Be("The field Price must be between 1 and 1000000000.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_the_description_was_not_provided : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(description: null); // It could be tested also cases like empty and spaces

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_be_considered_invalid_because_the_description() =>
                _validationResults.First().MemberNames.First().Should().Be("Description");

            [Test]
            public void Should_be_considered_invalid_because_the_description_was_not_provided() =>
                _validationResults.First().ErrorMessage.Should().Be("The Description field is required.");

            [Test]
            public void Should_be_considered_invalid_for_only_that_reason() =>
                _validationResults.Should().HaveCount(1);
        }

        public class When_are_not_satisfied_more_than_a_validation_rule : NewProductInputModelSpecs
        {
            public override void Given() =>
                _model = ObjectMother.CreateNewProductInputModelWith(number: -1, description: null); // It could be tested also cases like empty and spaces

            [Test]
            public void Should_be_considered_invalid() =>
                _isValid.Should().BeFalse();

            [Test]
            public void Should_inform_all_the_failed_rules() =>
                _validationResults.Should().HaveCount(2);
        }

        NewProductInputModel _model;
        bool _isValid;
        ICollection<ValidationResult> _validationResults = new List<ValidationResult>();
    }
}