using Domain.Abstractions.Logging;
using Domain.Abstractions.Repositories;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EFC.Repositories;

public abstract class RepositoryBase<TModel, TId, TEntity, TContext>(TContext context, ILogger logger) : IRepositoryBase<TModel, TId>  where TModel : class where TEntity : class where TContext : DbContext
{
    protected readonly TContext _context = context;
    protected DbSet<TEntity> Set => _context.Set<TEntity>();

    protected abstract void ApplyUpdates(TModel model, TEntity entity);
    protected abstract TModel ToModel(TEntity entity);
    protected abstract TEntity ToEntity(TModel model);

    public virtual async Task<TModel> AddAsync(TModel model, CancellationToken ct = default)
    {
        try
        {
            if (model is null)
                throw new ValidationDomainException("model must be provided");

            var entity = ToEntity(model);

            await Set.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);

            return ToModel(entity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.Log(ex.ToString());
            throw;
        }
    }

    public virtual async Task<TModel> UpdateAsync(TId id, TModel updatedModel, CancellationToken ct = default)
    {
        try
        {
            if (updatedModel is null)
                throw new ValidationDomainException("updated model must be provided");

            var entity = ToEntity(updatedModel);

            var existingEntity = await Set.FindAsync([id], ct) 
                ?? throw new NotFoundDomainException($"entity with id {id} was not found.");

            ApplyUpdates(updatedModel, existingEntity);
            await _context.SaveChangesAsync(ct);

            return ToModel(existingEntity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.Log(ex.ToString());
            throw;
        }
    }

    public virtual async Task<bool> DeleteAsync(TId id, CancellationToken ct = default)
    {
        try
        {
            var existingEntity = await Set.FindAsync([id], ct);
            if (existingEntity is null)
                return false;

            Set.Remove(existingEntity);
            await _context.SaveChangesAsync(ct);

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.Log(ex.ToString());
            throw;
        }
    }

    public virtual async Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        try
        {
            var entity = await Set
                .AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<TId>(e, "Id")!.Equals(id), ct);

            return entity is null ? null : ToModel(entity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.Log(ex.ToString());
            throw;
        }
    }

    public virtual async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default)
    {
        try
        {
            var entities = await Set
                .AsNoTracking()
                .ToListAsync(ct);

            return [.. entities.Select(ToModel)];
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.Log(ex.ToString());
            throw;
        }
    }
}
