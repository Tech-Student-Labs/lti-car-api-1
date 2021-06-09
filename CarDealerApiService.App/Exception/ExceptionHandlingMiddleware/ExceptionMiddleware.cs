using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CarDealerAPIService.App.Exception.ExceptionModel;
using Microsoft.AspNetCore.Http;

namespace CarDealerAPIService.App.Exception.ExceptionHandlingMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        
        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            // Log issues and handle exception response
            if (exception.GetType() == typeof(ArgumentNullException))
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new ErrorDetails {Type = "ArgumentNullException",StatusCode = response.StatusCode, Message = exception?.Message});
                return response.WriteAsync(result);
            }
            else if (exception.GetType() == typeof(ArgumentException))
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new ErrorDetails {Type = "ArgumentException",StatusCode = response.StatusCode, Message = exception?.Message});
                return response.WriteAsync(result);
            }
            else
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new ErrorDetails {Type = "Exception",StatusCode = response.StatusCode, Message = exception?.Message});
                return response.WriteAsync(result);
            }
        }
        
    }
}