using KProject.AuthenticationService.Contract;
using KProject.AuthenticationService.Managers;

namespace KProject.Api.Middleware
{
    public class BlacklistTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBlacklistedTokensStorage _blacklistedTokensStorage;
        private readonly ITokenManager _tokenManager;

        public BlacklistTokenMiddleware(RequestDelegate next, IBlacklistedTokensStorage blacklistedTokensStorage,
            ITokenManager tokenManager)
        {
            _next = next;
            _blacklistedTokensStorage = blacklistedTokensStorage;
            _tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await _next(context);
                return;
            }

            var tokenID = _tokenManager.ExtractTokenID(context.User);

            if (_blacklistedTokensStorage.IsTokenBlacklisted(tokenID))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            await _next(context);
        }
    }
}
