using KProject.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace KProject.DataAccessLayer
{
    public interface IKProjectDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
