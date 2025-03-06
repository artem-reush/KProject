using KProject.AuthenticationService.Contract;

namespace KProject.AuthenticationService
{
    public class InMemoryBlacklistedTokensStorage : IBlacklistedTokensStorage
    {
        private HashSet<Guid> _blacklistedTokens;
        private readonly ReaderWriterLockSlim _lock;

        public InMemoryBlacklistedTokensStorage()
        {
            _blacklistedTokens = new HashSet<Guid>();
            _lock = new ReaderWriterLockSlim();
        }

        public void AddToken(Guid tokenID)
        {
            _lock.EnterWriteLock();
            try
            {
                _blacklistedTokens.Add(tokenID);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool IsTokenBlacklisted(Guid tokenID)
        {
            _lock.EnterReadLock();
            try
            {
                return _blacklistedTokens.Contains(tokenID);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        // TODO: Очистка хранилища токенов после их истечения
    }
}
