using markdown_notes_app.Core.Interfaces;
using markdown_notes_app.Core.Interfaces.Repositories;
using markdown_notes_app.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace markdown_notes_app.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext DbContext { get; set; }

        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().Where(expression).AsNoTracking().FirstAsync() != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task RestoreAsync(Expression<Func<T, bool>> expression)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                var entity = await DbContext.Set<T>()
                    .IgnoreQueryFilters()
                    .Where(expression)
                    .FirstOrDefaultAsync();

                // FIX: Cast to ISoftDelete
                if (entity is ISoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsDeleted = false;
                    softDeleteEntity.DeletedAt = null;
                    await DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
