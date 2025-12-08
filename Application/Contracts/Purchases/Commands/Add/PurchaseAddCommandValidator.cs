using Domain.Errors;
using FluentValidation;

namespace Application.Contracts.Purchases.Commands.Add
{
	public class PurchaseAddCommandValidator : AbstractValidator<PurchaseAddCommand>
	{
		public PurchaseAddCommandValidator()
		{
			RuleFor(x => x)
                .Must(i => i.Quantity > 0)
                .WithMessage(ApplicationErrors.Item.PriceInvalid.Message);
        }
	}
}
