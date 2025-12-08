using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Shared;

namespace Application.Contracts.Items.Commands.Update
{
	internal class ItemUpdateCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork) : ICommandHandler<ItemUpdateCommand>
	{
		public async Task<Result> Handle(ItemUpdateCommand request, CancellationToken cancellationToken)
		{
			var get = await itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
			if (get.IsFailure) return get;

			var item = new Item(
				id: request.ItemId, 
				name: request?.Name ?? get.Value.Name, 
				category: get.Value.Category, 
				price: request?.Price ?? get.Value.Price, 
				stock: request?.Stock ?? get.Value.Stock);

			var update = await itemRepository.UpdateAsync(item, cancellationToken);
			var save = await unitOfWork.SaveChangesAsync(update, cancellationToken);

			return save;
		}
	}
}
