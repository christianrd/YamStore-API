using API.Core.Application.Common.Persistence;
using API.Core.Domain.Entities;

namespace API.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IGenericRepository<Product> product, IGenericRepository<Basket> baskets, IGenericRepository<BasketItem> basketItems)
        {
            Product = product;
            Baskets = baskets;
            BasketItems = basketItems;
        }

        public IGenericRepository<Product> Product { get; }
        public IGenericRepository<Basket> Baskets { get; }
        public IGenericRepository<BasketItem> BasketItems { get; }
    }
}