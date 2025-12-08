using Domain.Abstractions.Repositories;
using Domain.Errors;
using Domain.Models;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _db;

        public ItemRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Item>> GetAllAsync(CancellationToken cancellationToken = default) => await _db.Items.OrderBy(i => i.Id).ToListAsync();

        public async Task<Result> ReduceStockAsync(Guid itemId, int quantity, CancellationToken cancellationToken = default)
        {
            var item = await _db.Items.FirstOrDefaultAsync(i => i.Id == itemId, cancellationToken);
            if (item == null) return Result.Failure(PersistenceErrors.Item.NotFound);
            if (item.Stock < quantity) return Result.Failure(PersistenceErrors.Item.NotEnoughInStock);

            item.Stock -= quantity;
            return Result.Success();
            
        }

        public async Task<Result<Item>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
            var item = await _db.Items.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (item == null) return Result.Failure<Item>(PersistenceErrors.Item.NotFound);
            return item;
        }

        public async Task<Result<Guid>> AddAsync(Item item, CancellationToken cancellationToken = default)
        {
            var result = await _db.Items.AddAsync(item, cancellationToken);
            return result.Entity.Id;
        }

        public async Task<Result> UpdateAsync(Item updatedItem, CancellationToken cancellationToken = default)
        {
            var item = await _db.Items.FirstOrDefaultAsync(i => i.Id == updatedItem.Id, cancellationToken);
            if (item == null) return Result.Failure<Item>(PersistenceErrors.Item.NotFound);
            item = updatedItem;
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Item item)
        {
            _db.Items.Remove(item);
            return Result.Success();
        }
    }
}