using Microsoft.EntityFrameworkCore;
using Ski.Demo1.Domain;

namespace Ski.Demo1.Data
{
    public class DummyGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private List<TEntity> _entity = null;

        public DummyGenericRepository(List<TEntity> context)
        {
            _entity = context;
        }

        public void Create(TEntity entity)
        {
            _entity.Add(entity);
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entityToDelete)
        {
            _entity.Remove(entityToDelete);
        }

        public IQueryable<TEntity> Entity()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _entity.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetById(object Id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entityToUpdate)
        {
            _entity.Where(s => s == entityToUpdate).SingleOrDefault();
        }
    }
}