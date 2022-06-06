using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ski.Base.Util.Services;

namespace WebApiDemo.Controllers
{
    [ApiController]
    public class ErrorController : Controller
    {
        [Route("/error-development")]
        [HttpGet]
        public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            ApiError apiError = ErrorLog();

            return Problem(
                type: apiError.TraceId,
                detail: apiError.StackTrace,
                title: apiError.Message);
        }

        [Route("/error")]
        [HttpGet]
        public IActionResult HandleError()
        {
            ApiError apiError = ErrorLog();

            return Problem(
                type: apiError.TraceId);
        }

        private ApiError ErrorLog()
        {
            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            var result = new ApiError()
            {
                TraceId = HttpContext?.TraceIdentifier,
                Message = exceptionHandlerFeature.Error.Message,
                StackTrace = exceptionHandlerFeature.Error.StackTrace
            };

            _Log.ErrorAsync(result.TraceId + result.StackTrace);

            return result;
        }

        public class ApiError
        {
            public string TraceId { get; set; }
            public string Message { get; set; }
            public string StackTrace { get; set; }
        }
    }
}
