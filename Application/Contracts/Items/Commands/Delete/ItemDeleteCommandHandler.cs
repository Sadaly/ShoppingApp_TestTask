using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Shared;

namespace Application.Contracts.Items.Commands.Delete
{
	internal class ItemDeleteCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork) : ICommandHandler<ItemDeleteCommand>
	{
		public async Task<Result> Handle(ItemDeleteCommand request, CancellationToken cancellationToken)
		{
			var item = await itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
			if (item.IsFailure) return item;
			var delete = await itemRepository.DeleteAsync(item.Value);
            var save = await unitOfWork.SaveChangesAsync(delete, cancellationToken);
			return save;
		}
	}
}
