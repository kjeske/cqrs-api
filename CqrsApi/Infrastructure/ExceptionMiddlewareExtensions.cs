using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace CqrsApi.Infrastructure;

public static class ApplicationExceptionsExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app) =>
        app.UseExceptionHandler(builder => builder.Run(async context =>
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            switch (contextFeature?.Error)
            {
                case ValidationException validationException:
                    await HandleValidationException(context, validationException);
                    return;

                default:
                    await HandleGenericException(context, contextFeature?.Error);
                    return;
            }
        }));

    private static Task HandleValidationException(HttpContext context, ValidationException validationException) =>
        context.WriteError(HttpStatusCode.BadRequest, new ValidationError(validationException.Errors));

    private static Task HandleGenericException(HttpContext context, Exception exception) =>
        context.WriteError(
            httpStatusCode: HttpStatusCode.InternalServerError,
            error: new Error("Unexpected error")
        );

    private static async Task WriteError(this HttpContext context, HttpStatusCode httpStatusCode, Error error)
    {
        context.Response.StatusCode = (int)httpStatusCode;
        await context.Response.WriteAsJsonAsync(Result.Fail(error));
    }
}