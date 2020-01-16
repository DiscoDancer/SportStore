using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportStore.Core.Entities;

namespace SportStore.Infrastructure.Data
{
    public class OrderEfRepository : EfRepository<Order>
    {
        public OrderEfRepository(AppDbContext dbContext): base(dbContext) { }

        public override List<Order> List()
        {
            return DbContext.Orders
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product)
                .ToList();
        }

        public override Order Add(Order order)
        {
            DbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.Id == 0)
            {
                DbContext.Orders.Add(order);
            }

            DbContext.SaveChanges();

            return order;
        }
    }
}