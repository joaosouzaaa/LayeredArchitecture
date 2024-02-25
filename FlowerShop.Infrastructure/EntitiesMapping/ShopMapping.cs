using FlowerShop.Business.Constants;
using FlowerShop.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerShop.Infrastructure.EntitiesMapping;
public sealed class ShopMapping : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shops", SchemaConstants.FlowerShopSchema);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");

        builder.Property(s => s.Location)
            .IsRequired(true)
            .HasColumnName("location")
            .HasColumnType("varchar(200)");

        builder.Property(s => s.Email)
            .IsRequired(true)
            .HasColumnName("email")
            .HasColumnType("varchar(150)");

        builder.Property(s => s.CreationDate)
            .IsRequired(true)
            .HasColumnName("creation_date")
            .HasColumnType("datetime2");
    }
}
