using Microsoft.EntityFrameworkCore;
using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ProjetoCantina.API.Repositories.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _appDbContext;
    private readonly DbSet<TEntity> _entities;
    private static readonly char[] separatorArray = [','];

    protected GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _entities = appDbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync<TType>(
        Expression<Func<TEntity, bool>>? where = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        Expression<Func<TEntity, TType>>? select = null, bool distinct = false) where TType : class
    {
        IQueryable<TEntity> query = _entities;

        if (where != null)
        {
            query = query.Where(where);
        }

        if (distinct)
        {
            query.Distinct();
        }

        if (select != null)
        {
            query.Select(select);
        }

        foreach (var includeProperty in includeProperties.Split
            (separatorArray, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty.Trim());
        }

        if (orderBy != null)
        {
            return await orderBy(query).AsNoTracking().ToListAsync();
        }
        else
        {
            return await query.AsNoTracking().ToListAsync();
        }
    }

    public async Task<TEntity?> GetByIdAsync(
        Expression<Func<TEntity, bool>> firstOrDefault,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _entities;

        foreach (var includeProperty in includeProperties.Split
            (separatorArray, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty.Trim());
        }

        return await query.FirstOrDefaultAsync(firstOrDefault);
    }

    public async Task<bool> InsertAsync(TEntity entity)
    {
        try
        {
            await _entities.AddAsync(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> firstOrDefault)
    {
        TEntity? entityToDelete = await _entities
            .FirstOrDefaultAsync(firstOrDefault);

        if (entityToDelete != null)
            return Delete(entityToDelete);
        else
            return false;
    }

    public bool Delete(TEntity entityToDelete)
    {
        try
        {
            if (_appDbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entities.Attach(entityToDelete);
            }

            _entities.Remove(entityToDelete);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Update(TEntity entityToUpdate)
    {
        try
        {
            _entities.Attach(entityToUpdate);
            _appDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return true;
        }
        catch
        {
            return false;
        }
    }
}
