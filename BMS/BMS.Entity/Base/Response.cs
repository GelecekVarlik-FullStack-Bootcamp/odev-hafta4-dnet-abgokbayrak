using BMS.Entity.IBase;

namespace BMS.Entity.Base
{
    public abstract class Response : IResponse
    {
        public Response(string message, object data)
        {
            Message = message;
            Data = data;
        }

        public Response(string message, int statusCode, object data)
        {
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public string Message { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
    }

    public abstract class Response<T> : IResponse<T>
    {
        public Response(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public Response(string message, T data)
        {
            Message = message;
            Data = data;
        }
        
        public Response(string message, int statusCode, T data)
        {
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}