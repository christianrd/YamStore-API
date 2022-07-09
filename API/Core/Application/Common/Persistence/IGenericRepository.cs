using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Core.Domain.Entities;

namespace API.Core.Application.Common.Persistence
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<int> CommitChanges();
        Task<T> Insert(T entity);
        Task Update(T entity);
        Task SoftDelete(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        IQueryable<T> Get(Expression<Func<T, bool>> where, string includeProperties = "");
        IQueryable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include);
        IQueryable<T> Get(params Expression<Func<T, object>>[] include);
    }
}