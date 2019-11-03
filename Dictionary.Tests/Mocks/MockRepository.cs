using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dictionary.Domain.Models.Abstract;
using Dictionary.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Tests.Mocks
{
    public class MockRepository<T> : IRepository<T> where T : class, IEntity
    {
        public List<T> Entities { get; } = new List<T>();

        public async Task DeleteAsync(Guid entityId)
        {
            T entity = await Entities
                .AsQueryable()
                .SingleOrDefaultAsync(e => e.Id == entityId);
            Entities.Remove(entity);
        }

        public void DeleteRange(T[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                throw new ArgumentException(nameof(entities));
            }

            foreach (T entity in entities)
            {
                Entities.Remove(entity);
            }
        }

        public IQueryable<T> GetAll()
        {
            return Entities.AsQueryable();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            return Entities.AsQueryable().Where(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entities.AsQueryable().SingleOrDefaultAsync(predicate);
        }

        public async Task<Guid> InsertOrUpdateAsync(T entity)
        {
            T record = await GetSingleAsync(e => e.Id == entity.Id);
            if (record == null)
            {
                entity.Id = Guid.NewGuid();
                Entities.Add(entity);

                return entity.Id;
            }

            int index = Entities.IndexOf(record);
            Entities[index] = entity;
            
            return entity.Id;
        }
    }
}
