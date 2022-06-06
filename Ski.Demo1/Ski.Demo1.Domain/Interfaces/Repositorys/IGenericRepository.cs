using System.Linq.Expressions;

namespace Ski.Demo1.Domain
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Entity();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                   params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetById(object Id);

        void Create(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Delete(object id);

        void Delete(TEntity entityToDelete);
    }
}