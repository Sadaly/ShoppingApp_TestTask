using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingAppWebApi.Abstraction;

[ApiController]
public abstract class ApiController : ControllerBase
{
	protected readonly ISender Sender;

	protected ApiController(ISender sender)
	{
		Sender = sender;
	}
}
