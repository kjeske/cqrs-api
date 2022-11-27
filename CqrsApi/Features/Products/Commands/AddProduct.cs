using CqrsApi.Infrastructure;
using FluentValidation;

namespace CqrsApi.Features.Products.Commands;

public record AddProduct(string Name) : ICommand<string>
{
    public class Validator : RequestValidator<AddProduct>
    {
        public Validator() => RuleFor(c => c.Name).MaximumLength(255).NotEmpty();
    }
}