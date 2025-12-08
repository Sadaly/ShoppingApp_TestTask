using Domain.Enums;
using Domain.Errors;
using FluentValidation;

namespace Application.Contracts.Items.Commands.Add
{
	public class ItemAddCommandValidator : AbstractValidator<ItemAddCommand>
	{
		public ItemAddCommandValidator()
		{
			// Минимальные и максимальные значение длины имени лучше
			// вынести в отдельные переменные и хранить в каком-нибудь
			// ValueObject классе, как и все магические числа
			RuleFor(x => x.Name).NotEmpty()
				.MinimumLength(1)
				.MaximumLength(100);

			RuleFor(x => x.Price)
				.Must(i => i > 0)
				.WithMessage(ApplicationErrors.Item.PriceInvalid.Message);

            RuleFor(x => x.Stock)
                .Must(i => i >= 0)
                .WithMessage(ApplicationErrors.Item.StockInvalid.Message);
        }
	}
}
