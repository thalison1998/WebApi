using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Api.Controllers.BaseController
{
    [ApiController]
    public abstract class Base : ControllerBase
    {
        protected ActionResult CustomControllerResponse(object data = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (data == null)
            {
                return StatusCode((int)statusCode, new ProblemDetails
                {
                    Title = statusCode.ToString(),
                    Status = (int)statusCode,
                    Detail = "The requested resource was not found or there was an issue with the request."
                });
            }

            return Ok(data);
        }

        protected ActionResult CustomControllerError(string errorMessage, HttpStatusCode statusCode)
        {
            return StatusCode((int)statusCode, new ProblemDetails
            {
                Title = statusCode.ToString(),
                Status = (int)statusCode,
                Detail = errorMessage
            });
        }

        protected ActionResult HandleException(Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
            {
                Title = "An unexpected error occurred",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = ex.Message
            });
        }
    }
}


