using ShoppingAppWebApi.Middleware;

namespace ShoppingAppWebApi.Extensions;

public static class ExceptionHandlerExtensions
{
	public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ExceptionHandlingMiddleware>();
	}
}