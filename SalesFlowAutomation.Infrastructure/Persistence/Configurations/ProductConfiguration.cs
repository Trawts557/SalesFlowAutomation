
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.ToTable(p => p.HasCheckConstraint("CK_Product_UnitPrices", "[UnitPrice] > 0"));

            builder.ToTable(p => p.HasCheckConstraint("CK_Product_Stocks", "[Stock] >= 0"));
            

        }
    }
}
