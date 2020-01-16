using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

            var controller = new ProductController(mock.Object, _mapper) { PageSize = 3 };

            // Act
            var result =
                controller.List(null, 2).ViewData.Model as ProductsListViewModel;

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
                new ProductController(mock.Object, _mapper) { PageSize = 3 };

            // Act
            var result =
                controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            Assert.NotNull(result);
            var pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new[] {
                new Product {Id = 1, Name = "P1", Category = "Cat1"},
                new Product {Id = 2, Name = "P2", Category = "Cat2"},
                new Product {Id = 3, Name = "P3", Category = "Cat1"},
                new Product {Id = 4, Name = "P4", Category = "Cat2"},
                new Product {Id = 5, Name = "P5", Category = "Cat3"}
            }.ToList());
            var controller = new ProductController(mock.Object, _mapper) {PageSize = 3};

            // Action
            var result =
                (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel)?.Products.ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new [] {
                new Product {Id = 1, Name = "P1", Category = "Cat1"},
                new Product {Id = 2, Name = "P2", Category = "Cat2"},
                new Product {Id = 3, Name = "P3", Category = "Cat1"},
                new Product {Id = 4, Name = "P4", Category = "Cat2"},
                new Product {Id = 5, Name = "P5", Category = "Cat3"}
            }.ToList);
            var target = new ProductController(mock.Object, _mapper) {PageSize = 3};
            static ProductsListViewModel GetModel(ViewResult result) => result?.ViewData?.Model as ProductsListViewModel;

            // Action
            var res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            var res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            var res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            var resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;
            // Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}