using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Places.Models.Data;

namespace Svc.Places.Data.Mappings;

/// <inheritdoc />
public class PlaceFavoriteMapping : BaseEntityMapping<PlaceFavorite>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<PlaceFavorite> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder
            .HasQueryFilter(x => x.User!.IsActive);

        builder
            .HasOne(x => x.Place)
            .WithMany(x => x.Favorites)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.PlaceFavorites)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}