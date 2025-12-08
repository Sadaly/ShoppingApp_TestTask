using Domain.Models;
using Domain.Shared;

namespace Domain.Abstractions.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Result<Guid>> AddAsync(Purchase purchase, CancellationToken cancellationToken = default);
        Task<List<Purchase>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
