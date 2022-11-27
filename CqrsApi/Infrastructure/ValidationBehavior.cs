using FluentValidation;
using MediatR;

namespace CqrsApi.Infrastructure;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationTasks = _validators.Select(validator => validator.ValidateAsync(request, cancellationToken));
        var validationResults = await Task.WhenAll(validationTasks);
        var failures = validationResults.SelectMany(result => result.Errors).Where(failure => failure != null).ToList();

        return failures.Any()
            ? throw new ValidationException(failures)
            : await next();
    }
}