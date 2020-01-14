using System.Linq;
using AutoMapper;
using Moq;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web;
using SportStore.Web.Controllers;
using SportStore.Web.Models.ViewModels;
using Xunit;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        private readonly IMapper _mapper =
            new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); }).CreateMapper();

        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new[]
            {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
                new Product {Id = 4, Name = "P4"},
                new Product {Id = 5, Name = "P5"}
            }.ToList());

            var controller = new ProductController(mock.Object, _mapper) {PageSize = 3};

            // Act
            var result =
                controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert
            Assert.NotNull(result);
            var prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new[]
            {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
                new Product {Id = 4, Name = "P4"},
                new Product {Id = 5, Name = "P5"}
            }.ToList());

            // Arrange
            var controller =
                new ProductController(mock.Object, _mapper) {PageSize = 3};

            // Act
            var result =
                controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert
            Assert.NotNull(result);
            var pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
    }
}