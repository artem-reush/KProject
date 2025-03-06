using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KProject.DomainModel
{
    /// <summary>
    /// Доменная модель финансовой операции
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Идент. операции
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionID { get; private set; }

        /// <summary>
        /// Владелец счёта
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// Валюта счёта
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// Нав. свойство счёта
        /// </summary>
        public Account Account { get; private set; }

        /// <summary>
        /// Приход
        /// </summary>
        [Precision(15, 2)]
        public decimal Income { get; private set; }

        /// <summary>
        /// Расход
        /// </summary>
        [Precision(15, 2)]
        public decimal Withdraw { get; private set; }
    }
}
