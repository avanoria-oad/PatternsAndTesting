namespace Domain.Abstractions.Repositories;

public interface IRepositoryBase<TModel, TId>
{
    Task<TModel> AddAsync(TModel model, CancellationToken ct = default);
    Task<TModel> UpdateAsync(TId id,TModel updateModel, CancellationToken ct = default);
    Task<bool> DeleteAsync(TId id, CancellationToken ct = default);

    Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default);
}
