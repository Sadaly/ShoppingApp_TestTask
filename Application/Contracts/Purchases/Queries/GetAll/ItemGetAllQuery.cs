using Application.Abstractions.Messaging;
using Domain.Models;

namespace Application.Contracts.Purchases.Queries.GetAll;
public sealed record PurchaseGetAllQuery() : ICommand<List<Purchase>>;
