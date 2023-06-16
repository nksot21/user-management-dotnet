using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using user_management_api.Entities;
using user_management_api.Helpers;
using user_management_api.Models;
using user_management_api.Services;

namespace user_management_api.Middlewares
{
    public class RecordRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RecordRequestMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        //public RecordRequestMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}
        async public Task InvokeAsync(HttpContext context, IUserServices userServices, IRecordRequestService recordRequestService)
        {
            // Get Token 
            var token = await context.GetTokenAsync("access_token");
            if (token != null)
            {
                // Get Payload
                var username = JWTHelper.GetPayload(token);
                // Get User Info
                IndividualUser individualUser = await userServices.FindByUsername(username);
                if (individualUser == null)
                {
                    throw new Exception("Get User Failed");
                }
                // Save Request 
                RequestHistory requestHistory = new RequestHistory()
                {
                    Method = context.Request.Method,
                    Url = context.Request.GetDisplayUrl(),
                    UserId = individualUser.Id
                };
                await recordRequestService.SaveRequest(requestHistory);
            }


            await _next(context);
        }
    }
}
