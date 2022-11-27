using System.Net;
using BildMlue.API.Responses;
using FluentValidation;
using Newtonsoft.Json;

namespace BildMlue.API.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            object? errorResponse = null;

            switch (error)
            {
                case ValidationException e:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    errorResponse = new ValidationErrorResponse(
                        _httpContextAccessor.HttpContext?.TraceIdentifier ?? "",
                        e.Errors
                            .GroupBy(x => x.PropertyName)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Select(y => y.ErrorMessage).ToList() as IReadOnlyList<string>
                            )
                    );
                    break;
                default:
                    // unhandled error
                    _logger.LogError(error, "Unhandled exception occured");
#if DEBUG
                    throw;
#else
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
#endif
            }

            errorResponse ??= new ErrorResponse(
                error.Message,
                null,
                _httpContextAccessor.HttpContext?.TraceIdentifier);
            await response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}