using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web.Components;
using Xunit;

namespace SportStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new[]
            {
                new Product {Id = 1, Name = "P1", Category = "Apples"},
                new Product {Id = 2, Name = "P2", Category = "Apples"},
                new Product {Id = 3, Name = "P3", Category = "Plums"},
                new Product {Id = 4, Name = "P4", Category = "Oranges"}
            }.ToList());
            var target =
                new NavigationMenuViewComponent(mock.Object);

            // Act = get the set of categories
            var results = ((IEnumerable<string>) (target.Invoke()
                as ViewViewComponentResult)?.ViewData.Model).ToArray();

            // Assert
            Assert.True(new[]
            {
                "Apples",
                "Oranges", "Plums"
            }.SequenceEqual(results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // Arrange
            const string categoryToSelect = "Apples";
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.List()).Returns(new[] {
                new Product {Id = 1, Name = "P1", Category = "Apples"},
                new Product {Id = 4, Name = "P2", Category = "Oranges"},
            }.ToList());
            var target = new NavigationMenuViewComponent(mock.Object)
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext {RouteData = new RouteData()}
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            // Action
            var result = (string)(target.Invoke() as
                ViewViewComponentResult).ViewData["SelectedCategory"];

            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}