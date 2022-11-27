using MediatR;

namespace CqrsApi.Infrastructure;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseQuery
{
}

public interface IBaseQuery : IBaseRequest
{
}

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>, IRequest<Result<TResponse>>
{
}