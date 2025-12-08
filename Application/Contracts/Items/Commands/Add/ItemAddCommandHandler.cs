using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Shared;

namespace Application.Contracts.Items.Commands.Add
{
	internal class ItemAddCommandHandler(IItemRepository itemRepository, ITopCategoryCalculator topCategoryCalculator, IUnitOfWork unitOfWork) : ICommandHandler<ItemAddCommand, Guid>
	{
		public async Task<Result<Guid>> Handle(ItemAddCommand request, CancellationToken cancellationToken)
		{
			var item = new Item(request.Name, request.ItemCategory, request.Price, request.Stock);
			var id = await itemRepository.AddAsync(item, cancellationToken);
            var save = await unitOfWork.SaveChangesAsync(id, cancellationToken);
			if (save.IsSuccess) await topCategoryCalculator.CalculateTopCategoryAsync();

			return save;
		}
	}
}
