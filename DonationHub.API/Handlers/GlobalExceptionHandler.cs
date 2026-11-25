using DonationHub.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DonationHub.API.Handlers
{
    public sealed class GlobalExceptionHandler
        (IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken ct)
        {

            var statusCode = exception switch
            {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                ConflictException => StatusCodes.Status409Conflict,
                FailureException => StatusCodes.Status500InternalServerError,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                NotFoundException => StatusCodes.Status404NotFound,
                PayloadTooLargeException => StatusCodes.Status413PayloadTooLarge,
                TooManyRequestsException => StatusCodes.Status429TooManyRequests,
                UnprocessableEntityException => StatusCodes.Status422UnprocessableEntity,
                ValidationException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = statusCode;
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {

                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "Error Has Occurred",
                    Detail = exception.Message,
                    Status = statusCode
                }
            });
        }
    }
}
