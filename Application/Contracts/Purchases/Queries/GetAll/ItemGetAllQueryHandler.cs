using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Shared;

namespace Application.Contracts.Purchases.Queries.GetAll
{
	internal class PurchaseGetAllQueryHandler(IPurchaseRepository itemRepository) : ICommandHandler<PurchaseGetAllQuery, List<Purchase>>
	{
		public async Task<Result<List<Purchase>>> Handle(PurchaseGetAllQuery request, CancellationToken cancellationToken)
		{
			return await itemRepository.GetAllAsync(cancellationToken);
		}
	}
}
