using System.Threading.Tasks;
using Dictionary.Domain.Base;
using Dictionary.Domain.Models.Abstract;
using Dictionary.Domain.Repositories.Abstract;

namespace Dictionary.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DictionaryContext Context { get; }

        public UnitOfWork(DictionaryContext context)
        {
            Context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            return new Repository<TEntity>(this);
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
