using Microsoft.AspNetCore.Authentication;
using user_management_api.Helpers;
using user_management_api.Models;
using user_management_api.Services;

namespace user_management_api.Middlewares
{
    public class ValidTokenVerifyMiddleware
    {

        private readonly RequestDelegate _next;

        public ValidTokenVerifyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ICacheService cacheService)
        {

            // Get Payload / Check cache token
            var token = await context.GetTokenAsync("access_token");
            if(token != null) {
                var username = JWTHelper.GetPayload(token);
                if (username == null)
                {
                    throw new Exception("Invalid token");
                }
                var result = await cacheService.Get(username);
                if (result == null)
                {
                    throw new Exception("Expired Tokens");
                }
            }
            await _next(context);
        }
    }
}


