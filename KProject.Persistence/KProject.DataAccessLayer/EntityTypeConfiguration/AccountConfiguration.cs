using KProject.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KProject.DataAccessLayer.EntityTypeConfiguration
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(t => new { t.UserID, t.Currency });

            builder.HasOne<User>()
                .WithMany(t => t.Accounts)
                .HasForeignKey(t => t.UserID);
        }
    }
}
