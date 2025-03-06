using KProject.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace KProject.DataAccessLayer
{
    public interface IKProjectDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task ExecuteFinTransaction(int userID, Currency currency, bool isIncomeTransaction, decimal summ);
    }
}
