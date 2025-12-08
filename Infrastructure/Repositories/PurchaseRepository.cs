using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _db;

        public PurchaseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<Guid>> AddAsync(Purchase purchase, CancellationToken cancellationToken = default)
        {
            await _db.Purchases.AddAsync(purchase, cancellationToken);
            return purchase.Id;
        }

        public async Task<List<Purchase>> GetAllAsync(CancellationToken cancellationToken = default) => await _db.Purchases.OrderBy(p => p.Id).Include(p => p.Item).ToListAsync(cancellationToken);
    }
}