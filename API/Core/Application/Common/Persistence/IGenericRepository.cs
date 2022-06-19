using System;
using System.Collections.Generic;
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
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        
    }
}