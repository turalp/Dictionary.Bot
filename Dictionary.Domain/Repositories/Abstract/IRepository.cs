using System;
using System.Linq;
using System.Linq.Expressions;
using Dictionary.Domain.Models.Abstract;

namespace Dictionary.Domain.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IEntity
    {
        T GetSingle(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate);

        Guid Insert(T entity);

        void Delete(Guid entityId);

        void Update(T entity);
    }
}
