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
            catch (ArgumentException error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new ErrorDetails {Type = "ArgumentException",StatusCode = response.StatusCode, Message = error?.Message});
                await response.WriteAsync(result);
            }
            catch (System.Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new ErrorDetails {Type = "Exception",StatusCode = response.StatusCode, Message = error?.Message});
                await response.WriteAsync(result);
            }
        }
    }
}