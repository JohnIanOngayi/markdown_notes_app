using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace markdown_notes_app.Core.Interfaces.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
    }
}
