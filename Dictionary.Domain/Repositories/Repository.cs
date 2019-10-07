using System;
using System.Linq;
using System.Linq.Expressions;
using Dictionary.Domain.Models.Abstract;
using Dictionary.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return _unitOfWork.Context.Set<T>().Single(predicate);
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return _unitOfWork.Context.Set<T>().Where(predicate);
        }

        public Guid Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Id = Guid.NewGuid();
            _unitOfWork.Context.Set<T>().Add(entity);

            return entity.Id;
        }

        public void Delete(Guid entityId)
        {
            T record = GetSingle(e => e.Id == entityId);
            if (record != null)
            {
                _unitOfWork.Context.Set<T>().Remove(record);
            }
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.Context.Set<T>().Attach(entity);
        }
    }
}
