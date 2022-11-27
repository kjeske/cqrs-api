using MediatR;

namespace CqrsApi.Infrastructure;

public static class EndpointsRequestExtensions
{
    public static RouteHandlerBuilder MapCommand<TCommand>(this IEndpointRouteBuilder endpoints) where TCommand : IBaseCommand =>
        MapRequest<TCommand>(endpoints);

    public static RouteHandlerBuilder MapQuery<TQuery>(this IEndpointRouteBuilder endpoints) where TQuery : IBaseQuery =>
        MapRequest<TQuery>(endpoints);

    private static RouteHandlerBuilder MapRequest<TRequest>(IEndpointRouteBuilder endpoints) where TRequest : IBaseRequest
    {
        var routeName = ToLowerCase(typeof(TRequest).Name);
        return endpoints
            .MapPost(routeName, (IMediator mediator, TRequest request) => mediator.Send(request!))
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static string ToLowerCase(string type) =>
        char.ToLowerInvariant(type[0]) + type[1..];
}