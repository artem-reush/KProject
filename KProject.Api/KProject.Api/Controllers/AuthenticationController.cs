using KProject.Api.Contract.Request;
using KProject.Api.Contract.Response;
using KProject.AuthenticationService;
using KProject.AuthenticationService.Contract;
using KProject.AuthenticationService.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KProject.Api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenManager _tokenManager;
        private readonly IBlacklistedTokensStorage _blacklistedTokensStorage;

        public AuthenticationController(IAuthenticationService authenticationService, ITokenManager tokenManager,
            IBlacklistedTokensStorage blacklistedTokensStorage)
        {
            _authenticationService = authenticationService;
            _tokenManager = tokenManager;
            _blacklistedTokensStorage = blacklistedTokensStorage;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<BaseResponseModel<string>> Login(LoginModel model)
        {
            var result = await _authenticationService.Authenticate(model.Login, model.Password);
            return new BaseResponseModel<string>
            {
                Message = result.Message,
                IsOk = result.IsOk,
                Result = result.Token
            };
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public void Logout()
        {
            var tokenID = _tokenManager.ExtractTokenID(HttpContext.User);
            _blacklistedTokensStorage.AddToken(tokenID);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registration")]
        public async Task Registration(LoginModel model)
        {
            await _authenticationService.Registration(model.Login, model.Password);
        }
    }
}
