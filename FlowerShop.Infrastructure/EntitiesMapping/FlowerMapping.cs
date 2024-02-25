using FlowerShop.Business.Constants;
using FlowerShop.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerShop.Infrastructure.EntitiesMapping;
public sealed class FlowerMapping : IEntityTypeConfiguration<Flower>
{
    public void Configure(EntityTypeBuilder<Flower> builder)
    {
        builder.ToTable("Flowers", SchemaConstants.FlowerShopSchema);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");

        builder.Property(f => f.Color)
            .IsRequired(true)
            .HasColumnName("color")
            .HasColumnType("varchar(100)");

        builder.Property(f => f.Species)
            .IsRequired(true)
            .HasColumnName("species")
            .HasColumnType("varchar(100)");

        builder.Property(f => f.BloomingSeason)
            .IsRequired(true)
            .HasColumnName("blooming_season");

        builder.HasOne(f => f.Shop)
            .WithMany(s => s.Flowers)
            .HasForeignKey(f => f.ShopId)
            .HasConstraintName("FK_Flower_Shop")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
