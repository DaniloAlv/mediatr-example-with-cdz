namespace MediatrExample.API.Models
{
    public class ResponseResult
    {
        public object data { get; set; }
        public int statusCode { get; set; }

        public ResponseResult(object data, int statusCode)
        {
            this.data = data;
            this.statusCode = statusCode;
        }
    }
}
