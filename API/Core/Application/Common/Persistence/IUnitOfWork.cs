namespace API.Core.Application.Common.Persistence
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Domain.Entities.Product> Product { get; }
        public IGenericRepository<Domain.Entities.Basket> Baskets { get; }
        public IGenericRepository<Domain.Entities.BasketItem> BasketItems { get; }
    }
}