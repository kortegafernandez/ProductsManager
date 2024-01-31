using System.Net;

namespace ProductsManager.API.Responses
{
    public class ProductsManagerResponse
    {
        public static ProductsManagerResponse Create(Guid requestId, HttpStatusCode statusCode, object? result = null, List<string>? errors = null)
        {
            return new ProductsManagerResponse(requestId, statusCode, result, errors);
        }

        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; }
        public List<string>? Errors { get; set; }
        public object? Result { get; set; }

        protected ProductsManagerResponse(Guid requestId, HttpStatusCode statusCode, object? result = null, List<string>? errors = null)
        {
            RequestId = requestId.ToString();
            StatusCode = (int)statusCode;
            Result = result;
            Errors = errors;
            IsSuccess = (int)statusCode >= 200 && (int)statusCode <= 299;
        }
    }
}
