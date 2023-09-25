using Film.Application.Base;
using Film.Application.Contract.Base.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Film.WebAPI.Middelwares
{
    public static class ExceptionMiddleware
    {

        public static async Task ExceptionMiddle(HttpContext httpContext)
        {
            Exception exception = httpContext.Features.Get<IExceptionHandlerPathFeature>().Error;
            var result = GetBusinessException(exception);
            if (result is null)
            {
                return;
            }
            var resultType = result.ExceptionType;
            if (resultType == BusinessExceptionType.NotFound)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (resultType == BusinessExceptionType.BadRequest)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            var response = new ResponseDto
            {
                Message = result.Message,

            };
           await httpContext.Response.WriteAsJsonAsync(response);
            //  var exception=httpContext.e
            // .. to implement
        }
        private static BusinessException? GetBusinessException(Exception exception)
        {
            if (exception.GetType() == typeof(BusinessException))
            {
                return (BusinessException)exception;
            }
            else if (exception.InnerException is not null)
            {
                return GetBusinessException(exception.InnerException);
            }
            return null;
        }
    }

}
