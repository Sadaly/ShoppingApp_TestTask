using Application.Abstractions.Messaging;
using Domain.Models;

namespace Application.Contracts.Items.Queries.GetById;
public sealed record ItemGetByIdQuery(Guid Id) : ICommand<Item>;
