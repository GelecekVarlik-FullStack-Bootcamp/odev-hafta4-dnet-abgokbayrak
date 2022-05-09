using Microsoft.AspNetCore.Http;

namespace BMS.Entity.Base
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(string message, int statusCode = StatusCodes.Status500InternalServerError, object data = null) : base(message, statusCode, data)
        {
        }

    }


    public class ErrorResponse<T> : Response<T>
    {

        public ErrorResponse(string message, int statusCode = StatusCodes.Status500InternalServerError) : base(message, statusCode)
        {
        }

        public ErrorResponse(string message, int statusCode, T data) : base(message, statusCode, data)
        {
        }
    }
}