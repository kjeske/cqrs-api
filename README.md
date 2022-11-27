# cqrs-api

An example of using CQRS pattern together with minimal api from .NET 7.

## CQRS
CQRS means Command and Query Responsibility Segregation and it sounds more complex than it actually is. The main outcome of using CQRS is cleaner and simpler code that is structured better and is easy to scale.

## Simplicity
I tried to make this example as simple as possible to show the core of CQRS advantages, but I also included here some other most common use cases, which is for example validation or exception handling. 

Here is the result:

Command declaration:
```csharp
public record AddProduct(string Name) : ICommand<string>
{
    public class Validator : RequestValidator<AddProduct>
    {
        public Validator() => RuleFor(c => c.Name).MaximumLength(255).NotEmpty();
    }
}
```

Command's handler:
```csharp
public class AddProductHandler : ICommandHandler<AddProduct>
{
    public async Task<Result<string>> Handle(AddProduct command, CancellationToken ct)
    {
        var product = Product.Create(
            name: command.Name
        );
        // ...
        return Result.Ok(product.Id);
    }
}
```

Mapping command to the API's endpoint:
```csharp
endpoints.MapGroup("products")
    .MapCommand<AddProduct>();
```

Which results in a new route:
`POST /products/addProduct`

It's a live project which I will update with the most common use cases.