using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Places.Models.Data;

namespace Svc.Places.Data.Mappings;

/// <inheritdoc />
public class PlaceMapping : BaseEntityMapping<Place>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Place> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.NameNormalized)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .HasIndex(x => x.NameNormalized);

        builder
            .Property(x => x.Description)
            .HasMaxLength(8192)
            .IsRequired();

        builder
            .Property(x => x.Address)
            .HasMaxLength(512)
            .IsRequired();

        builder
            .Property(x => x.Area)
            .HasColumnType("POLYGON")
            .HasSrid(4326)
            .IsRequired();

        builder
            .HasIndex(x => x.Area)
            .IsSpatial();

        builder
            .Property(x => x.FavoriteCount)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(x => x.VisitsCount)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(x => x.LatestVisit);

        builder
            .Ignore(x => x.IsOpen);

        builder
            .HasMany(x => x.OpeningHours)
            .WithOne(x => x.Place)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.Pictures)
            .WithOne(x => x.Place)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.Favorites)
            .WithOne(x => x.Place)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.Visits)
            .WithOne(x => x.Place)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}