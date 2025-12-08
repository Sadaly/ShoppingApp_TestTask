using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Shared;

namespace Application.Contracts.Purchases.Commands.Add
{
	internal class PurchaseAddCommandHandler(IPurchaseRepository purchaseRepository, IItemRepository itemRepository, ITopCategoryCalculator topCategoryCalculator, IUnitOfWork unitOfWork) : ICommandHandler<PurchaseAddCommand, Guid>
	{
		public async Task<Result<Guid>> Handle(PurchaseAddCommand request, CancellationToken cancellationToken)
		{
			var item = await itemRepository.GetByIdAsync(request.ItemId);
			if (item.IsFailure) return Result.Failure<Guid>(item);

            var take = await itemRepository.ReduceStockAsync(request.ItemId, request.Quantity, cancellationToken);
			if (take.IsFailure) return Result.Failure<Guid>(take);

            var purchase = new Purchase(item.Value, item.Value.Price, request.Quantity);

			var id = await purchaseRepository.AddAsync(purchase, cancellationToken);
            var save = await unitOfWork.SaveChangesAsync(id, cancellationToken);
            if (save.IsSuccess) await topCategoryCalculator.CalculateTopCategoryAsync();

            return save;
		}
	}
}
