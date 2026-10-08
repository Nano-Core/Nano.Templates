using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Places.Models.Data;

namespace Svc.Places.Data.Mappings;

/// <inheritdoc />
public class PlacePictureMapping : BaseEntityMapping<PlacePicture>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<PlacePicture> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder
            .HasOne(x => x.Place)
            .WithMany(x => x.Pictures)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .Property(x => x.OrderIndex)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .HasIndex(x => new
            {
                x.PlaceId,
                x.OrderIndex
            })
            .IsUnique();
    }
}