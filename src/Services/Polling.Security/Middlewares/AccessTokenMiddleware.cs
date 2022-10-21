using Polling.Security.Services;
using System.Net;

namespace Polling.Security.Middlewares
{
    public class AccessTokenMiddleware : IMiddleware
    {
        private readonly IAccessTokenService _accessTokenService;

        public AccessTokenMiddleware(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _accessTokenService.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
