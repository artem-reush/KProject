using KProject.AuthenticationService.Managers;
using KProject.DataAccessLayer;
using KProject.DomainModel;
using KProject.AuthenticationService.Contract;
using KProject.AuthenticationService.Contract.Models;
using KProject.TransactionsService.Contract;

namespace KProject.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IKProjectDbContext _dbContext;
        private readonly ITokenManager _tokenManager;
        private readonly ITransactionsService _transactionsService;

        public AuthenticationService(IKProjectDbContext dbContext, ITokenManager tokenManager, 
            ITransactionsService transactionsService)
        {
            _dbContext = dbContext;
            _tokenManager = tokenManager;
            _transactionsService = transactionsService;
        }

        public async Task<AuthenticationResult> Authenticate(string login, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Login == login);

            if (user is null)
                return AuthenticationResult.Error("Пользователь не найден");

            if (user.IsBlocked)
                return AuthenticationResult.Error("Пользователь заблокирован");

            if (!BcryptHashProvider.Verify(password, user.PasswordHash))
            {
                user.IncreaseLoginAttempts();
                await _dbContext.SaveChangesAsync();
                return AuthenticationResult.Error("Не верный пароль");
            }

            user.ResetLoginAttempts();
            await _dbContext.SaveChangesAsync();

            var token = _tokenManager.GenerateToken(user);

            return AuthenticationResult.Success(token);
        }

        public async Task Registration(string login, string password)
        {
            var newUser = User.GetUserWithAccount(login, BcryptHashProvider.Hash(password), Currency.USD);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
            await _transactionsService.IncomeTransaction(newUser.UserID, Currency.USD, 8.0M);
        }
    }
}
