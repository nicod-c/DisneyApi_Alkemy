using System.Collections.Generic;

namespace AlkemyDisney.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Delete(int id);
        List<TEntity> GetEntities();
        TEntity GetEntity(int id);
        TEntity Update(TEntity entity);
    }
}