
using SalesFlowAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesFlowAutomation.Infrastructure.Persistence.Configurations
{
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasKey(sd => sd.Id);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(sd => sd.ProductId);

            builder.Property(sd => sd.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(sd => sd.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.ToTable(sd => sd.HasCheckConstraint("CK_SaleDetail_UnitPrices", "[UnitPrice] > 0"));

            builder.ToTable(sd => sd.HasCheckConstraint("CK_SaleDetail_Quantity", "[Quantity] > 0"));
                
        }
    }
}
