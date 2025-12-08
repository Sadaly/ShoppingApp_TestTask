using Application.Contracts.Items.Commands.Add;
using Application.Contracts.Items.Commands.Delete;
using Application.Contracts.Items.Commands.Update;
using Application.Contracts.Items.Queries.GetAll;
using Application.Contracts.Items.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppWebApi.Abstraction;
using ShoppingAppWebApi.Extensions;

namespace ShoppingAppWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "seller")]
    public class SellerController(ISender sender) : ApiController(sender)
    {
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(
            [FromQuery] ItemAddCommand command,
            CancellationToken cancellationToken)
            => (await Sender.Send(command, cancellationToken)).ToActionResult();

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateItem(
            [FromQuery] ItemUpdateCommand command,
            CancellationToken cancellationToken)
            => (await Sender.Send(command, cancellationToken)).ToActionResult();

        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> DeleteItem(
            [FromQuery] ItemDeleteCommand command,
            CancellationToken cancellationToken)
            => (await Sender.Send(command, cancellationToken)).ToActionResult();

        [HttpGet("GetItemById")]
        public async Task<IActionResult> GetItemById(
            [FromQuery] ItemGetByIdQuery query,
            CancellationToken cancellationToken)
            => (await Sender.Send(query, cancellationToken)).ToActionResult();

        [HttpGet("GetAllItems")]
        public async Task<IActionResult> GetAllItems(
            [FromQuery] ItemGetAllQuery query,
            CancellationToken cancellationToken)
            => (await Sender.Send(query, cancellationToken)).ToActionResult();

    }
}
