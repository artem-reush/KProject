using KProject.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KProject.DataAccessLayer.EntityTypeConfiguration
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => new { t.UserID, t.Currency });
        }
    }
}
