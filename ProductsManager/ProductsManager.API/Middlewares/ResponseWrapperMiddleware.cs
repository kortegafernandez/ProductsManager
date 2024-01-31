using FluentValidation;
using ProductsManager.API.Responses;
using ProductsManager.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductsManager.API.Middlewares
{
    public class ResponseWrapperMiddleware(RequestDelegate next,
        ILogger<ResponseWrapperMiddleware> logger,
        IWebHostEnvironment environment)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ResponseWrapperMiddleware> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;
            var requestId = Guid.NewGuid();

            using (var memoryStream = new MemoryStream())
            {
                //set the current response to the memorystream.
                context.Response.Body = memoryStream;
                ProductsManagerResponse? result = null;
                try
                {
                    await _next(context);

                    //reset the body 
                    context.Response.Body = currentBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                    object? contentText = null;
                    try
                    {
                        contentText = string.IsNullOrEmpty(readToEnd) ? null : JsonSerializer.Deserialize<object>(readToEnd);
                    }
                    catch (Exception)
                    {
                        contentText = readToEnd;
                    }

                    var objResult = contentText ?? readToEnd;
                    result = ProductsManagerResponse.Create(requestId, (HttpStatusCode)context.Response.StatusCode, objResult, null);
                }
                catch (Exception error)
                {
                    context.Response.Body = currentBody;
                    var response = context.Response;
                    response.ContentType = "application/json";
                    var errorMessages = new List<string>();

                    switch (error)
                    {
                        case ProductNotFoundException e:
                            // not found error
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            errorMessages.Add($"Product {e.Message} not found");
                            _logger.LogWarning(e, "Product not found.");
                            break;

                        case ValidationException ve:
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            errorMessages.AddRange(ve.Errors.Select(e => e.ErrorMessage));
                            _logger.LogWarning(ve, "Validation error.");
                            break;

                        default:
                            // unhandled error
                            _logger.LogError(error, "Unhandled error!!!");
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            if (_environment.IsDevelopment())
                            {
                                errorMessages.Add(error.Message);
                                errorMessages.Add($"InnerException:{error.InnerException?.Message}");
                                errorMessages.Add($"Trace:{error.StackTrace}");
                            }
                            else
                            {
                                errorMessages.Add("Ha ocurrido un error.");
                            }

                            break;
                    }

                    result = ProductsManagerResponse.Create(requestId, (HttpStatusCode)context.Response.StatusCode, errors: errorMessages);
                }
                finally
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(result, new JsonSerializerOptions(JsonSerializerDefaults.Web)));
                }
            }
        }
    }
}
