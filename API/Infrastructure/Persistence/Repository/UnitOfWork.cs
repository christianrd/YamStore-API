using API.Core.Application.Common.Persistence;
using API.Core.Domain.Entities;

namespace API.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IGenericRepository<Product> product)
        {
            Product = product;
        }

        public IGenericRepository<Product> Product { get; }
    }
}