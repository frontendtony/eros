using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Eros.Api.Models;
using Eros.Application.Exceptions;

namespace Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new SingleResponseModel<string>
            {
                Message = error?.Message ?? string.Empty,
                Data = null
            };

            _logger.LogError(error, error?.Message);

            switch (error)
            {
                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    responseModel.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case UnauthorizedException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    responseModel.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case ForbiddenException:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    responseModel.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;

                case ConflictException:
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    responseModel.StatusCode = (int)HttpStatusCode.Conflict;
                    break;

                case CustomValidationException validationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(responseModel, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await response.WriteAsync(result).ConfigureAwait(false);
        }
    }
}
