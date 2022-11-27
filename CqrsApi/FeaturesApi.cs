using CqrsApi.Features.Products.Commands;
using CqrsApi.Infrastructure;

namespace CqrsApi;

public static class FeaturesApi
{
    public static void MapFeatures(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("products")
            .MapCommand<AddProduct>();
    }
}