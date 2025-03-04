using KProject.Api.Contract.Request;
using KProject.Api.Contract.Response;
using KProject.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KProject.Api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
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
    }
}
