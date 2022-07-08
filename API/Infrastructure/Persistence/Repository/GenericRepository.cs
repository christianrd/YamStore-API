using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Core.Application.Common.Persistence;
using API.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        protected DbSet<T> DbSet => _context.Set<T>();
        
        public async Task<int> CommitChanges() => await _context.SaveChangesAsync();

        public async Task<T> Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public async Task SoftDelete(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.Deleted = true;

            await Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await DbSet.Where(x => x.Deleted == false).OrderByDescending(x => x.CreatedAt).ToListAsync();

        public async Task<T> GetById(Guid id) => await DbSet.FirstOrDefaultAsync(x => x.Id == id && x.Deleted == false);
        public IQueryable<T> Get(Expression<Func<T, bool>> where, string includeProperties = "")
        {
            var query = DbSet.AsQueryable();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (where != null)
                query = query.Where(where);

            return query;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var includeProperty in include)
            {
                query = query.Include(includeProperty);
            }

            if (where != null)
                query = query.Where(where);

            return query;
        }

        public IQueryable<T> Get(params Expression<Func<T, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var includeProperty in include)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }
    }
}