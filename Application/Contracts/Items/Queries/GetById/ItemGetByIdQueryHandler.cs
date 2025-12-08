using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Shared;

namespace Application.Contracts.Items.Queries.GetById
{
	internal class ItemGetByIdQueryHandler(IItemRepository itemRepository) : ICommandHandler<ItemGetByIdQuery, Item>
	{
		public async Task<Result<Item>> Handle(ItemGetByIdQuery request, CancellationToken cancellationToken)
		{
			var item = await itemRepository.GetByIdAsync(request.Id, cancellationToken);

			return item;
		}
	}
}
