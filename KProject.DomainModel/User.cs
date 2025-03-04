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

        public int UserID { get; private set; }
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public int LoginAttempts { get; private set; }

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
    }
}
