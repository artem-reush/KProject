using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KProject.DomainModel
{
    /// <summary>
    /// Доменная модель пользователя
    /// </summary>
    public class User
    {
        /// <param name="login">Логин пользователя</param>
        /// <param name="passwordHash">Пароль в хешированном виде</param>
        public User(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; private set; }

        [MaxLength(50)]
        public string Login { get; private set; }

        [MaxLength(60)]
        public string PasswordHash { get; private set; }

        public int LoginAttempts { get; private set; }

        public ICollection<Account> Accounts { get; private set; }

        /// <summary>
        /// Признак блокировки пользователя
        /// </summary>
        [NotMapped]
        public bool IsBlocked => LoginAttempts >= 3;

        /// <summary>
        /// Увеличивает количество неудачных попыток входа
        /// </summary>
        public void IncreaseLoginAttempts() => LoginAttempts++;

        /// <summary>
        /// Сбрасывает количество неудачных попыток входа
        /// </summary>
        public void ResetLoginAttempts() => LoginAttempts = 0;

        /// <summary>
        /// Создание пользователя с одним счётом указанной валюты
        /// </summary>
        public static User GetUserWithAccount(string login, string passwordHash, Currency accountCurrency)
        {
            return new User(login, passwordHash)
            {
                Accounts = new HashSet<Account>() { new Account(accountCurrency) }
            };
        }
    }
}
