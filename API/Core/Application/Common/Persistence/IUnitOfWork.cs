namespace API.Core.Application.Common.Persistence
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Domain.Entities.Product> Product { get; }
    }
}