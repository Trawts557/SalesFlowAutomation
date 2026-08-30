
using SalesFlowAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesFlowAutomation.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne<Sale>()
                .WithOne()
                .HasForeignKey<Payment>(p => p.SaleId)
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.ToTable(p => p.HasCheckConstraint("CK_Payment_Amount", "[Amount] > 0"));
        }
    }
}
