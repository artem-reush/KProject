using KProject.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KProject.DataAccessLayer
{
    internal class KProjectDbContext(DbContextOptions<KProjectDbContext> options)
        : DbContext(options), IKProjectDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public async Task ExecuteFinTransaction(int userID, Currency currency, bool isIncomeTransaction, decimal summ)
        {
            await this.Database.ExecuteSqlAsync(
                $"EXEC dbo.ExecuteTransaction @UserID = {userID}, @Currency = {currency}, @IsIncomeTransaction = {isIncomeTransaction}, @Summ = {summ}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
