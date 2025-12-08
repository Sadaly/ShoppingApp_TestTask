using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Shared;

namespace Application.Contracts.Items.Queries.GetAll
{
	internal class ItemGetAllQueryHandler(IItemRepository itemRepository) : ICommandHandler<ItemGetAllQuery, List<Item>>
	{
		public async Task<Result<List<Item>>> Handle(ItemGetAllQuery request, CancellationToken cancellationToken)
		{
			return await itemRepository.GetAllAsync(cancellationToken);
		}
	}
}
