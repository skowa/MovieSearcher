using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Space.MovieSearcher.Application.Exceptions;

namespace Space.MovieSearcher.Presentation.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseExceptionResponsesHander(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(err => err.Use(HandleError));
    }

    private static async Task HandleError(HttpContext context, Func<Task> next)
    {
        context.Response.ContentType = "application/json";
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        if (feature is not null)
        {
            switch (feature.Error)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails { Status = (int)HttpStatusCode.NotFound }));

                    break;
                case ArgumentException argumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails { Status = (int)HttpStatusCode.NotFound, Detail = argumentException.Message }));

                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    break;
            }
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
