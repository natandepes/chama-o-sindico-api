using ChamaOSindico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamaOSindico.Infra.EntitiesConfiguration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255); // Adjust length as needed

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)"); // Match Supabase decimal type

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()"); // PostgreSQL default timestamp
        }
    }
}
