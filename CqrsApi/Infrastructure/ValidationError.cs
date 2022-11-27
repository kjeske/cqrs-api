using FluentValidation.Results;

namespace CqrsApi.Infrastructure;

public record ValidationError : Error
{
    public ValidationError(IEnumerable<ValidationFailure> errorItems)
        : base("validationError", errorItems.GroupBy(vf => vf.PropertyName).ToDictionary(vf => vf.Key, vf => vf.Select(f => f.ErrorMessage).First()))
    {
    }
}