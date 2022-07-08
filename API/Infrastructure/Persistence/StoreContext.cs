using API.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Persistence
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; } 
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}