using Application.Abstractions.Messaging;

namespace Application.Contracts.Items.Commands.Delete;
public sealed record ItemDeleteCommand(Guid ItemId) : ICommand;
