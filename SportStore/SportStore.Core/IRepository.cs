using System.Collections.Generic;
using SportStore.Core.Entities;

namespace SportStore.Core
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        List<T> List();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}