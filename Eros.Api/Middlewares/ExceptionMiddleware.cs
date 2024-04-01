using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Eros.Api.Models;
using Eros.Application.Exceptions;

namespace Eros.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context).ConfigureAwait(false);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            
            var responseModel = new ErrorResponseModel()
            {
                Message = error?.Message ?? string.Empty
            };
            
            if (error is CustomValidationException validationException)
            {
                responseModel.ValidationErrors = validationException.Errors.Select(e => new ValidationErrorModel(e.PropertyName, e.ErrorMessage));
            }

            logger.LogError(error, error?.Message);

            response.StatusCode = error switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                BadRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                ForbiddenException => (int)HttpStatusCode.Forbidden,
                ConflictException => (int)HttpStatusCode.Conflict,
                CustomValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonConvert.SerializeObject(responseModel, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await response.WriteAsync(result).ConfigureAwait(false);
        }
    }
}
