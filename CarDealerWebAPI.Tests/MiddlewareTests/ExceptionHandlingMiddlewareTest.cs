using CarDealerAPIService.App.Exception.ExceptionHandlingMiddleware;
using CarDealerAPIService.App.Exception.ExceptionModel;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace CarDealerWebAPI.Tests.MiddlewareTests
{
    public class ExceptionHandlingMiddlewareTest
    {
        [Fact]
        public void ExceptionMiddleware_ShouldReturnAnErrorModelWithTypeEqualToException_WhenExceptionErrorIsThrown()
        {
            //Given
            var middleware = new ExceptionMiddleware((innerHttpContext) => throw new Exception("Test"));
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            //When
            middleware.Invoke(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<ErrorDetails>(streamText);

            //Then
            objResponse
                .Should()
                .BeEquivalentTo(new ErrorDetails() { Message = "Test", Type = "Exception", StatusCode = 400 });
        }

        [Fact]
        public void
            ExceptionMiddleware_ShouldReturnAnErrorModelWithTypeEqualToArgumentException_WhenArgumentExceptionIsThrown()
        {
            //Given
            var middleware = new ExceptionMiddleware((innerHttpContext) => throw new ArgumentException("Test"));
            //When
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<ErrorDetails>(streamText);

            //Then
            objResponse
                .Should()
                .BeEquivalentTo(new ErrorDetails() { Message = "Test", Type = "ArgumentException", StatusCode = 400 });
        }

        [Fact]
        public void
            ExceptionMiddleware_ShouldReturnAnErrorModelWithTypeEqualToArgumentNullException_WhenArgumentNullExceptionIsThrown()
        {
            //Given
            var middleware = new ExceptionMiddleware((innerHttpContext) =>
                throw new ArgumentNullException("testing", "Argument can't be null"));
            //When
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<ErrorDetails>(streamText);

            //Then
            objResponse
                .Should()
                .BeEquivalentTo(new ErrorDetails()
                {
                    Message = "Argument can't be null (Parameter 'testing')",
                    Type = "ArgumentNullException",
                    StatusCode = 400
                });
        }
    }
}