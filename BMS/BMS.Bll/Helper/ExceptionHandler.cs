using BMS.Entity.Base;
using BMS.Entity.Exceptions;
using BMS.Entity.IBase;
using Microsoft.AspNetCore.Http;
using System;

namespace BMS.Bll.Helper
{
    public class ExceptionHandler
    {
        public static IResponse<R> Subscribe<R>(Func<R> func, string successMessage)
        {
            try
            {
                var data = func();
                return new SuccessResponse<R>(successMessage, data);
            }
            catch (NotFound404Exception ex)
            {
                return new ErrorResponse<R>(ex.Message, StatusCodes.Status404NotFound);
            }
            catch (BadRequest400Exception ex)
            {
                return new ErrorResponse<R>(ex.Message, StatusCodes.Status400BadRequest);
            }
            catch (NotAcceptable406Exception ex)
            {
                return new ErrorResponse<R>(ex.Message, StatusCodes.Status406NotAcceptable);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<R>($"Bir hata meydana geldi : {ex.Message}");
            }
        }
    }
}
