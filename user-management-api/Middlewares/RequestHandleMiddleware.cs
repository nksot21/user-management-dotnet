using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using user_management_api.Models;

namespace user_management_api.Middlewares
{
    public class RequestHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
           
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var appError = new AppError { Message = ex.Message , StatusCode = 400};
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = appError.StatusCode;

            return context.Response.WriteAsJsonAsync(appError);
        }

    }
}
