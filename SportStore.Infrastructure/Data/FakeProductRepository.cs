using System;
using System.Collections.Generic;
using SportStore.Core;
using SportStore.Core.Entities;

namespace SportStore.Infrastructure.Data
{
    public class FakeProductRepository : IRepository<Product>
    {
        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> List() =>
            new List<Product>
            {
                new Product {Name = "Football", Price = 25},
                new Product {Name = "Surf board", Price = 179},
                new Product {Name = "Running shoes", Price = 95}
            };


        public Product Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
