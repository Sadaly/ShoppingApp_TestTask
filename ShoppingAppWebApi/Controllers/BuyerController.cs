using Application.Contracts.Purchases.Commands.Add;
using Application.Contracts.Purchases.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppWebApi.Abstraction;
using ShoppingAppWebApi.Extensions;

namespace ShoppingAppWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "buyer")]
    public class BuyerController(ISender sender) : ApiController(sender)
    {
        [HttpPost("CreatePurchase")]
        public async Task<IActionResult> CreatePurchase(
            [FromBody] PurchaseAddCommand command,
            CancellationToken cancellationToken)
            => (await Sender.Send(command, cancellationToken)).ToActionResult();

        [HttpGet("GetAllPurchases")]
        public async Task<IActionResult> GetAllPurchases(
            [FromQuery] PurchaseGetAllQuery command,
            CancellationToken cancellationToken)
            => (await Sender.Send(command, cancellationToken)).ToActionResult();
    }
}
