using KProject.DataAccessLayer;
using KProject.DomainModel;
using KProject.TransactionsService.Contract;

namespace KProject.TransactionsService
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IKProjectDbContext _dbContext;

        public TransactionsService(IKProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task IncomeTransaction(int userID, Currency currency, decimal summ)
        {
            await _dbContext.ExecuteFinTransaction(userID, currency, true, summ);
        }

        public async Task WithdrawTransaction(int userID, Currency currency, decimal summ)
        {
            await _dbContext.ExecuteFinTransaction(userID, currency, false, summ);
        }
    }
}
