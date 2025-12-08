using Domain.Errors;
using FluentValidation;

namespace Application.Contracts.Items.Commands.Update
{
	public class ItemUpdateCommandValidator : AbstractValidator<ItemUpdateCommand>
	{
		public ItemUpdateCommandValidator()
		{
			RuleFor(x => x)
                .Must(i => i.Stock != null
					|| i.Price != null
					|| i.Name != null)
				.WithErrorCode(ApplicationErrors.Item.NothingChanged)
				.WithMessage(ApplicationErrors.Item.NothingChanged.Message);

			// Минимальные и максимальные значение длины имени лучше
			// вынести в отдельные переменные и хранить в каком-нибудь
			// ValueObject классе
			RuleFor(x => x.Name)
				.Must(n => n == null
				|| (n.Length > 0 && n.Length <= 100) );

			RuleFor(x => x.Price)
                .Must(p => p == null
                || p > 0 )
                .WithMessage(ApplicationErrors.Item.PriceInvalid.Message);

            RuleFor(x => x.Stock)
                .Must(p => p == null
                || p >= 0)
                .WithMessage(ApplicationErrors.Item.StockInvalid.Message);
        }
	}
}
