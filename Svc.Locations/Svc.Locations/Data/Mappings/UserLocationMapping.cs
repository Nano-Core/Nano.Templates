using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Locations.Models.Data;

namespace Svc.Locations.Data.Mappings;

/// <inheritdoc />
public class UserLocationMapping : BaseEntityMapping<UserLocation>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<UserLocation> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Locations)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .Property(x => x.Coordinate)
            .HasColumnType("POINT")
            .HasSrid(4326);

        builder
            .HasIndex(x => x.Coordinate)
            .IsSpatial();

        builder
            .Ignore(x => x.Latitude);

        builder
            .Ignore(x => x.Longitude);

        builder
            .Ignore(x => x.Altitude);

        builder
            .Property(x => x.HorizontalAccuracy)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .HasIndex(x => x.HorizontalAccuracy);

        builder
            .Property(x => x.VerticalAccuracy);

        builder
            .Property(x => x.Speed);

        builder
            .Property(x => x.Course);
    }
}