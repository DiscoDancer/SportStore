using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Core;
using SportStore.Core.BusinessLogic;
using SportStore.Core.Entities;
using SportStore.Web;
using SportStore.Web.Controllers;
using Xunit;

namespace SportStore.Tests
{
    public class OrderControllerTests
    {
        private readonly IMapper _mapper =
            new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); }).CreateMapper();

        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange
            var mock = new Mock<IRepository<Order>>();
            var cart = new Cart();
            var order = new Order();
            var target = new OrderController(mock.Object, cart, _mapper);

            // Act
            var result = target.Checkout(order) as ViewResult;

            // Assert
            Assert.NotNull(result);
            mock.Verify(m => m.Add(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange
            var mock = new Mock<IRepository<Order>>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new OrderController(mock.Object, cart, _mapper);
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            var result = target.Checkout(new Order()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            mock.Verify(m => m.Add(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange 
            var mock = new Mock<IRepository<Order>>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new OrderController(mock.Object, cart, _mapper);

            // Act
            var result =
                target.Checkout(new Order()) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            mock.Verify(m => m.Add(It.IsAny<Order>()), Times.Once);
            Assert.Equal("Completed", result.ActionName);
        }
    }
}