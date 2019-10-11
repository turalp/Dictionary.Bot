using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dictionary.Domain.Models.Abstract;

namespace Dictionary.Domain.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate);

        Task DeleteAsync(Guid entityId);

        Task<Guid> InsertOrUpdateAsync(T entity);
    }
}
