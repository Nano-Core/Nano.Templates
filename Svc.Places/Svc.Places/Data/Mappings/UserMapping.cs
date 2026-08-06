using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Places.Models.Data;

namespace Svc.Places.Data.Mappings;

/// <inheritdoc />
public class UserMapping : BaseEntityMapping<User>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasQueryFilter(x => x.IsActive);

        builder
            .Property(x => x.FullName)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder
            .HasIndex(x => x.IsActive);

        builder
            .HasMany(x => x.PlaceFavorites)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.PlaceVisits)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}