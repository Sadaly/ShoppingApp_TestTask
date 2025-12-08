using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Contracts.Items.Commands.Update;
public sealed record ItemUpdateCommand(Guid ItemId, string? Name, decimal? Price, int? Stock) : ICommand;