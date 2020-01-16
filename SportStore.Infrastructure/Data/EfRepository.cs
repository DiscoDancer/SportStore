using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportStore.Core;
using SportStore.Core.Entities;

namespace SportStore.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext DbContext;

        public EfRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual T GetById(int id)
        {
            return DbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public virtual List<T> List()
        {
            return DbContext.Set<T>().ToList();
        }

        public virtual T Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();

            return entity;
        }

        public virtual void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            DbContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
    }
}