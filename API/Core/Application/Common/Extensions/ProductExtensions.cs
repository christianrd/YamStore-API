using System.Linq;

namespace API.Core.Application.Common.Extensions
{
    public static class ProductExtensions 
    {
        public static IQueryable<Domain.Entities.Product> Sort(this IQueryable<Domain.Entities.Product> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Name);

            query = orderBy switch 
            {
                "price" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Name)
            };

            return query;
        }
    }
}