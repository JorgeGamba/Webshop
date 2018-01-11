using System;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using Webshop.Features.ProductRegistration;
using Webshop.UnitSpecs.Helpers;

namespace Webshop.UnitSpecs.Features.ProductRegistration
{
    public class ProductRegisterSpecs : FeatureSpecifications
    {
        public override void When() => _exception = Catch.Exception(() =>
            _result = ProductRegister.Register(_dao, _newProduct)
        );

        public class When_the_new_product_accomplish_all_the_constraints : ProductRegisterSpecs
        {
            public override void Given()
            {
                _newProduct = _someValidProduct;
                _dao = new ProductStoringDAOFake(numberIsAlreadyUsed: false, titleIsAlreadyUsed: false);
            }

            [Test]
            public void Should_not_throw_an_exception() =>
                _exception.Should().BeNull();

            [Test]
            public void Should_return_a_result() =>
                _result.Should().NotBeNull();

            [Test]
            public void Should_return_a_result_of_type_SuccessfulProductRegistrationResult() =>
                _result.Should().BeOfType<SuccessfulProductRegistrationResult>();

            [Test]
            public void Should_return_a_result_with_the_actual_stored_product() =>
                ((SuccessfulProductRegistrationResult) _result).StoredProduct.Should().NotBeNull();
        }

        public class When_the_number_is_already_being_used_by_other_product : ProductRegisterSpecs
        {
            public override void Given()
            {
                _newProduct = _someValidProduct;
                _dao = new ProductStoringDAOFake(numberIsAlreadyUsed: true, titleIsAlreadyUsed: false);
            }

            [Test]
            public void Should_not_throw_an_exception() =>
                _exception.Should().BeNull();

            [Test]
            public void Should_return_a_result() =>
                _result.Should().NotBeNull();

            [Test]
            public void Should_return_a_result_of_type_FailedProductRegistrationResult() =>
                _result.Should().BeOfType<FailedProductRegistrationResult>();

            [Test]
            public void Should_indicate_the_reason_for_the_failure() =>
                ((FailedProductRegistrationResult)_result).Reason.Should().Be("There is already another product that is using the same number.");
        }

        public class When_the_title_is_already_being_used_by_other_product : ProductRegisterSpecs
        {
            public override void Given()
            {
                _newProduct = _someValidProduct;
                _dao = new ProductStoringDAOFake(numberIsAlreadyUsed: false, titleIsAlreadyUsed: true);
            }

            [Test]
            public void Should_not_throw_an_exception() =>
                _exception.Should().BeNull();

            [Test]
            public void Should_return_a_result() =>
                _result.Should().NotBeNull();

            [Test]
            public void Should_return_a_result_of_type_FailedProductRegistrationResult() =>
                _result.Should().BeOfType<FailedProductRegistrationResult>();

            [Test]
            public void Should_indicate_the_reason_for_the_failure() =>
                ((FailedProductRegistrationResult)_result).Reason.Should().Be("There is already another product that is using the same title.");
        }

        public class When_any_of_the_business_rules_are_not_met : ProductRegisterSpecs // Because the violation of any business rule always throws an exception
        {
            public override void Given()
            {
                _newProduct = ObjectMother.CreateNewProductInputModelWith(price: 1000000001); // More than 1 million is not allowed
                _dao = new ProductStoringDAOFake(numberIsAlreadyUsed: false, titleIsAlreadyUsed: false);
            }

            [Test]
            public void Should_not_handle_exception() =>
                _exception.Should().NotBeNull();
        }

        public class When_the_command_produces_a_runtime_exception : ProductRegisterSpecs
        {
            public override void Given()
            {
                _newProduct = _someValidProduct;
                _dao = new ProductStoringDAOFake(numberIsAlreadyUsed: false, titleIsAlreadyUsed: false, storingFails: true);
            }

            [Test]
            public void Should_handle_the_exception() =>
                _exception.Should().BeNull();

            [Test]
            public void Should_return_a_result() =>
                _result.Should().NotBeNull();

            [Test]
            public void Should_return_a_result_of_type_FailedProductSearchResult() =>
                _result.Should().BeOfType<FailedProductRegistrationResult>();

            [Test]
            public void Should_indicate_the_reason_for_the_failure() =>
                ((FailedProductRegistrationResult)_result).Reason.Should().Be("There was a problem, please try again.");
        }


        NewProductInputModel _newProduct;
        ProductStoringDAOFake _dao;
        IProductRegistrationResult _result;
        Exception _exception;

        static NewProductInputModel _someValidProduct = new NewProductInputModel { Number = 111, Title = "Some title", Price = 1000, Description = "Some description" };
    }
}