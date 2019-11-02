using System;
using System.Threading.Tasks;
using Dictionary.Domain.Base;
using Dictionary.Domain.Models.Abstract;

namespace Dictionary.Domain.Repositories.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        DictionaryContext Context { get; }

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        Task CommitAsync();
    }
}
