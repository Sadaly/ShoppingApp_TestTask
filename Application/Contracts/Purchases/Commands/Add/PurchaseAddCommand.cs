using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Contracts.Purchases.Commands.Add;
public sealed record PurchaseAddCommand(Guid ItemId, int Quantity) : ICommand<Guid>;
