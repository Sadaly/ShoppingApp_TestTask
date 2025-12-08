using Application.Abstractions.Messaging;
using Domain.Models;

namespace Application.Contracts.Items.Queries.GetAll;
public sealed record ItemGetAllQuery() : ICommand<List<Item>>;
