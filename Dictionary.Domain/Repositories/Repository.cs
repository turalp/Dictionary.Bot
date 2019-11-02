using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public IQueryable<T> GetAll()
        {
            return _unitOfWork.Context.Set<T>().AsQueryable();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return await _unitOfWork.Context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return _unitOfWork.Context.Set<T>().Where(predicate);
        }

        public async Task DeleteAsync(Guid entityId)
        {
            T record = await GetSingleAsync(e => e.Id == entityId);
            if (record != null)
            {
                _unitOfWork.Context.Set<T>().Remove(record);
            }
        }

        public void DeleteRange(T[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                throw new ArgumentException(nameof(entities));
            }

            _unitOfWork.Context.Set<T>().RemoveRange(entities);
        }

        public async Task<Guid> InsertOrUpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            T record = await GetSingleAsync(e => e.Id == entity.Id);
            if (record == null)
            {
                entity.Id = Guid.NewGuid();
                _unitOfWork.Context.Set<T>().Add(entity);
            }
            else
            {
                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
                _unitOfWork.Context.Set<T>().Attach(entity);
            }

            return entity.Id;
        }
    }
}
