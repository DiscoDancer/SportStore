using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        private static T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}