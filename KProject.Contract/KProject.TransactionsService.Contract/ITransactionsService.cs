using KProject.DomainModel;

namespace KProject.TransactionsService.Contract
{
    public interface ITransactionsService
    {
        /// <summary>
        /// Проведение транзакции пополнения счета
        /// </summary>
        Task IncomeTransaction(int userID, Currency currency, decimal summ);

        /// <summary>
        /// Проведение транзакции списания со счета
        /// </summary>
        Task WithdrawTransaction(int userID, Currency currency, decimal summ);
    }
}
