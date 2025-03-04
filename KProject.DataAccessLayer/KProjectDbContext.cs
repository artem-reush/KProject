using KProject.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace KProject.DataAccessLayer
{
    internal class KProjectDbContext : DbContext, IKProjectDbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
