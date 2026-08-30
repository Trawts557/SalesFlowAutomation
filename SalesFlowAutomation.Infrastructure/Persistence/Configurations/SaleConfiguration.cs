
using SalesFlowAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesFlowAutomation.Infrastructure.Persistence.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Details)
                .WithOne()
                .HasForeignKey(sd => sd.SaleId);

            builder.Property(s => s.CashierId)
                .IsRequired();

            builder.Property(s => s.DiscountAmount)
                .HasPrecision(18, 2);
        }
    }
}
