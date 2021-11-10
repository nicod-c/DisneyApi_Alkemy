using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyDisney.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> 
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _dBcontext;

        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet ??= _dBcontext.Set<TEntity>(); }
        }

        protected BaseRepository(TContext context)
        {
            _dBcontext = context;
        }

        public List<TEntity> GetEntities()
        {
            return _dBcontext.Set<TEntity>().ToList();
        }

        public TEntity GetEntity(int id)
        {
            return _dBcontext.Set<TEntity>().Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            _dBcontext.Set<TEntity>().Add(entity);
            _dBcontext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dBcontext.Attach(entity);
            _dBcontext.Entry(entity).State = EntityState.Modified;
            _dBcontext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entityToelete = _dBcontext.Find<TEntity>(id);
            _dBcontext.Remove(entityToelete);
            _dBcontext.SaveChanges();
        }
    }
}
