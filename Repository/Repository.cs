using LibraryAppRestapi.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryAppRestapi.Repository
{
   public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {

            return Context.Set<TEntity>().Find(id);

        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }



        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }



        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);

        }



        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

       
    }
}
