using System.Linq.Expressions;

namespace ProjetoCantina.API.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>?> GetAllAsync<TType>(
            Expression<Func<TEntity, bool>>? where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "", Expression<Func<TEntity, TType>>? select = null,
            bool distinct = false) where TType : class;

    Task<TEntity?> GetByIdAsync(
            Expression<Func<TEntity, bool>> firstOrDefault,
            string includeProperties = "");

    Task<bool> InsertAsync(TEntity entity);

    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> firstOrDefault);

    bool Delete(TEntity entityToDelete);

    bool Update(TEntity entityToUpdate);

}
