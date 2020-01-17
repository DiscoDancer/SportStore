using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web;
using SportStore.Web.Controllers;
using SportStore.Web.Models.Dto;
using Xunit;

namespace SportStore.Tests
{
    public class AdminControllerTests
    {
        private readonly IMapper _mapper =
            new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); }).CreateMapper();

        [Fact]
        public void Index_Contains_All_Products()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new[]
            {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"}
            }.ToList());
            var target = new AdminController(mock.Object, _mapper);

            // Action
            var result
                = GetViewModel<IEnumerable<ProductDto>>(target.Index())?.ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new [] {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
            }.ToList());
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int y) => new Product { Id = y, Name = "PX" });
            var target = new AdminController(mock.Object, _mapper);

            // Act
            var p1 = GetViewModel<ProductDto>(target.Edit(1));
            var p2 = GetViewModel<ProductDto>(target.Edit(2));
            var p3 = GetViewModel<ProductDto>(target.Edit(3));

            // Assert
            Assert.Equal(1, p1.Id);
            Assert.Equal(2, p2.Id);
            Assert.Equal(3, p3.Id);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new [] {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
            }.ToList());
            var target = new AdminController(mock.Object, _mapper);

            // Act
            var result = GetViewModel<Product>(target.Edit(4));

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int y) => new Product { Id = y, Name = "PX" });
            var target = new AdminController(mock.Object, _mapper)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
            var product = new ProductDto { Name = "Test", Id = 1};

            // Act
            var result = target.Edit(product) as RedirectToActionResult;

            // Assert 
            Assert.NotNull(result);
            mock.Verify(m => m.Update(It.IsAny<Product>()), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            var target = new AdminController(mock.Object, _mapper);
            var product = new ProductDto { Name = "Test" };
            target.ModelState.AddModelError("error", "error");

            // Act
            var result = target.Edit(product);

            // Assert
            mock.Verify(m => m.Update(It.IsAny<Product>()), Times.Never());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            // Arrange
            var prod = new Product { Id = 2, Name = "Test" };
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new [] {
                new Product {Id = 1, Name = "P1"},
                prod,
                new Product {Id = 3, Name = "P3"},
            }.ToList());
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int y) => new Product { Id = y, Name = "PX" });
            var target = new AdminController(mock.Object, _mapper)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            // Act
            target.Delete(prod.Id);

            // Assert
            mock.Verify(m => m.Delete(It.IsAny<Product>()), Times.Once);
        }

        private static T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}