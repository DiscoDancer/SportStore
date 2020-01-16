﻿using System.Linq;
using SportStore.Core.BusinessLogic;
using SportStore.Core.Entities;
using Xunit;

namespace SportStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };
            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            var results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };
            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            var results = target.Lines
                .OrderBy(c => c.Product.Id).ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1" };
            var p2 = new Product { Id = 2, Name = "P2" };
            var p3 = new Product { Id = 3, Name = "P3" };
            var target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.Equal(0, target.Lines.Count(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            var p2 = new Product { Id = 2, Name = "P2", Price = 50M };
            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            var result = target.ComputeTotalValue();

            // Assert
            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange
            var p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            var p2 = new Product { Id = 2, Name = "P2", Price = 50M };
            var target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act
            target.Clear();

            // Assert
            Assert.Empty(target.Lines);
        }
    }
}