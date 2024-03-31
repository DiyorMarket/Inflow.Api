using Inflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Infrastructure.Configurations
{
    internal class SupplyItemEntityConfiguration : IEntityTypeConfiguration<SupplyItem>
    {
        public void Configure(EntityTypeBuilder<SupplyItem> builder)
        {
            builder.ToTable(nameof(SupplyItem));

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Quantity)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(si => si.UnitPrice)
                .HasColumnType("money");

            builder.HasOne(si => si.Supply)
                .WithMany(s => s.SupplyItems)
                .HasForeignKey(si => si.SupplyId);

            builder.HasOne(si => si.Product)
                .WithMany(p => p.SupplyItems)
                .HasForeignKey(si => si.ProductId);
        }
    }
}
