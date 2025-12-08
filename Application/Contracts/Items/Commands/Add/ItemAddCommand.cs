using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Contracts.Items.Commands.Add;
public sealed record ItemAddCommand(string Name, ItemCategory ItemCategory, decimal Price, int Stock) : ICommand<Guid>;
