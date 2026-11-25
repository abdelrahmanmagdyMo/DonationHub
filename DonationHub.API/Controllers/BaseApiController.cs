using Asp.Versioning;
using DonationHub.Domain.Common.Results;
using DonationHub.Domain.Common.Results.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DonationHub.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ActionResult ToActionResult(Error error)
        {
            return ToActionResult(new List<Error> { error });
        }
        protected ActionResult ToActionResult(Result result)
        {
            if (result.IsSuccess) return NoContent();
            return ToActionResult(result.Error);
        }


        protected ActionResult ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess) return Ok(result.Value);
            return ToActionResult(result.Error);
        }
        private ActionResult ToActionResult(List<Error> errors)
        {
            if (errors.Count == 0) return Problem();
            if (errors.All(e => e.ErrorType == ErrorType.Validation))
            {
                var modelState = new ModelStateDictionary();
                foreach (var err in errors)
                {
                    modelState.AddModelError(err.Code, err.Message);

                }
                return ValidationProblem(modelState);
            }
            var error = errors[0];
            var statusCode = error.ErrorType switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.PayloadTooLarge => StatusCodes.Status413PayloadTooLarge,
                ErrorType.TooManyRequests => StatusCodes.Status429TooManyRequests,
                ErrorType.UnprocessableEntity => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
            return Problem(statusCode: statusCode, title: error.Message);
        }

    }

}
