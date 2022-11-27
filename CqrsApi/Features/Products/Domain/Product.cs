namespace CqrsApi.Features.Products.Domain;

public class Product
{
    private Product()
    {
    }
    
    public string Id { get; private set; }
    public string Name { get; private set; }

    public static Product Create(string name) => new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = name
    };
}