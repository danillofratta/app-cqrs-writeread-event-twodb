
using Base.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Command.Orm.Repository;

public abstract class CommandRepositoryBase<TEntity, TKey> : ICommandRepositoryBase<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbContext _dbContext;

    protected CommandRepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity> SaveAsync(TEntity entity)
    {
        await BeforeSaveAsync(entity);
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
        await AfterSaveAsync(entity);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await BeforeUpdateAsync(entity);
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
        await AfterUpdateAsync(entity);
        return entity;
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        await BeforeDeleteAsync(entity);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        await AfterDeleteAsync(entity);
        return entity;
    }

    public virtual Task BeforeSaveAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task AfterSaveAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task BeforeUpdateAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task AfterUpdateAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task BeforeDeleteAsync(TEntity entity) => Task.CompletedTask;
    public virtual Task AfterDeleteAsync(TEntity entity) => Task.CompletedTask;
}

