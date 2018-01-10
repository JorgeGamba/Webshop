using System.Web.Mvc;
using FluentAssertions;
using TechTalk.SpecFlow;
using Webshop.Features.ProductRegistration;

namespace Webshop.AcceptanceSpecs.Steps
{
    [Binding]
    public class RegistrationOfNewProductsSteps
    {
        private InMemoryStorage _inMemoryStorage;
        private readonly ProductRegistrationController _controller;
        private ActionResult _result;
        private NewProductInputModel _newProduct;

        public RegistrationOfNewProductsSteps()
        {
            _inMemoryStorage = new InMemoryStorage();
            var dao = new ProductStoringMemoryDao(_inMemoryStorage);
            var register = new ProductRegister(dao);
            _controller = new ProductRegistrationController(register);
        }

        [Given]
        public void Given_I_have_provided_all_the_information_valid()
        {
            _newProduct = new NewProductInputModel { Number = 1, Title = "Some title", Price = 1000, Description = "Some description" };
        }
        
        [Given]
        public void Given_I_have_provided_some_invalid_information()
        {
            _newProduct = new NewProductInputModel { Number = 1, /*Title = "Some title",*/ Price = 1000, Description = "Some description" };
            _controller.ModelState.AddModelError("", "Error");
        }

        [When]
        public void When_I_request_to_register()
        {
            _result = _controller.Index(_newProduct);
        }

        [Then]
        public void Then_the_new_product_should_be_registered()
        {
            _inMemoryStorage.StoredProducts.Should().ContainKey(_newProduct.Number);
        }

        [Then]
        public void Then_the_new_product_should_not_be_registered()
        {
            _inMemoryStorage.StoredProducts.Should().NotContainKey(_newProduct.Number);
        }

        [Then]
        public void Then_I_should_see_the_product_list()
        {
            _result.Should().NotBeOfType<ViewResult>();
        }

        [Then]
        public void Then_I_should_be_able_to_try_again_fixing_the_data()
        {
            _result.Should().BeOfType<ViewResult>();
        }

        [Then]
        public void Then_I_should_be_informed_that_my_data_is_invalid()
        {
            ((ViewResult) _result).ViewData.ModelState.IsValid.Should().BeFalse();
        }
    }
}
