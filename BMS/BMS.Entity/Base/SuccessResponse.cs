using Microsoft.AspNetCore.Http;

namespace BMS.Entity.Base
{
    public class SuccessResponse : Response
    {
        public SuccessResponse(string message, object data) : base(message, data)
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }

    public class SuccessResponse<T> : Response<T>
    {
        public SuccessResponse(string message, T data) : base(message, data)
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
