using Microsoft.AspNetCore.Mvc;
using MillionLuxury.TechhicalTest.Resources.Validations;
using MillionLuxury.TechhicalTest.Domain.Exceptions;
using MillionLuxury.TechhicalTest.Domain.Values;
using MillionLuxury.TechhicalTest.Domain.Enums;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MillionLuxury.TechhicalTest.ApiRest.Controllers
{
    public abstract class ControllerMillionBase : ControllerBase
    {
        protected ILogger _Logger { get; set; }

        public ControllerMillionBase(ILoggerFactory loggerFactory)
        {
            _Logger = loggerFactory.CreateLogger(GetType());
        }

        protected virtual IActionResult HandleException(Exception exception)
        {
            if (exception is ValidationErrorException ex)
            {
                Error error = new()
                {
                    ErrorType = ErrorType.ValidationError,
                    Message = ex.Message,
                    Errors = ex.ErrorMessages
                };
                string message = JsonSerializer.Serialize(error, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                LogError(exception, message);
                return BadRequest(error);
            }

            if (exception is ApplicationException)
            {
                Error errorEntity = new()
                {
                    ErrorType = ErrorType.Application,
                    Message = exception.Message,
                };
                LogError(exception, exception.Message);
                return BadRequest(errorEntity);
            }

            LogError(exception, CommonValidationMessages.ExceptionMessage);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        protected virtual IActionResult ValidateModel<TModel>(TModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        protected virtual void LogError(Exception exception, string message, params object[] arg)
        {
            _Logger.LogError(exception, message, arg);
        }
    }
}
