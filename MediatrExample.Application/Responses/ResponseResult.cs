namespace MediatrExample.Application.Responses
{
    public class ResponseResult
    {
        private ResponseResult(object data, int statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }

        private ResponseResult(Error error, int statusCode)
        {
            Error = error;
            StatusCode = statusCode;
        }

        public object Data { get; set; }
        public int StatusCode { get; }
        public Error? Error { get; }

        public static ResponseResult Success<T>(T data, int statusCode) => new ResponseResult(data, statusCode);
        public static ResponseResult Failure(Error error, int statusCode) => new ResponseResult(error, statusCode);
    }
}
