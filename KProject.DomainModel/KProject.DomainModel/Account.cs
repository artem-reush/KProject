using Microsoft.EntityFrameworkCore;

namespace KProject.DomainModel
{
    /// <summary>
    /// Доменная модель счёта
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Идент. пользоваеля
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// Валюта счёта
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// Баланс счёта в валюте
        /// </summary>
        [Precision(15, 2)]
        public decimal CurrentBalance { get; private set; }

        public Account(int userID, Currency currency)
            : this(currency)
        {
            UserID = userID;
        }

        internal Account(Currency currency)
        {
            Currency = currency;
            CurrentBalance = 0;
        }
    }
}
