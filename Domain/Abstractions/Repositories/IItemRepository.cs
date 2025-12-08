using Domain.Models;
using Domain.Shared;

namespace Domain.Abstractions.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<Item>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<Guid>> AddAsync(Item item, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Item item, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Item item);
        Task<Result> ReduceStockAsync(Guid itemId, int quantity, CancellationToken cancellationToken = default);
    }
}
