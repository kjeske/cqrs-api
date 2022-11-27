using CqrsApi.Features.Products.Domain;
using CqrsApi.Infrastructure;

namespace CqrsApi.Features.Products.Commands;

public class AddProductHandler : ICommandHandler<AddProduct, string>
{
    public Task<Result<string>> Handle(AddProduct command, CancellationToken ct)
    {
        var product = Product.Create(
            name: command.Name
        );

        return Task.FromResult(Result.Ok(product.Id));
    }
}