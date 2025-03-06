using KProject.AuthenticationService.Managers;
using KProject.DomainModel;
using KProject.TransactionsService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KProject.Api.Controllers
{
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly ITransactionsService _transactionsService;

        public PaymentController(ITokenManager tokenManager, ITransactionsService transactionsService)
        {
            _tokenManager = tokenManager;
            _transactionsService = transactionsService;
        }

        [HttpPost]
        [Route("payment")]
        public async Task Payment()
        {
            var userID = _tokenManager.ExtractUserID(HttpContext.User);

            await _transactionsService.WithdrawTransaction(userID, Currency.USD, 1.1M);
        }
    }
}
