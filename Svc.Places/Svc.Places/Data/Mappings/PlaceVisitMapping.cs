using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Abstractions.Models.Abstractions;
using Nano.Data.Mappings;
using Nano.Data.Mappings.Extensions;
using Svc.Places.Data.Mappings.Triggers;
using Svc.Places.Models.Data;
using System;

namespace Svc.Places.Data.Mappings;

/// <inheritdoc />
public class PlaceVisitMapping : BaseEntityMapping<PlaceVisit>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<PlaceVisit> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasQueryFilter(x => x.User!.IsActive);

        builder
            .HasOne(x => x.Place)
            .WithMany(x => x.Visits)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.PlaceVisits)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .Property(x => x.EnteredAt)
            .IsRequired();

        builder
            .Property(x => x.LeftAt);

        builder
            .HasIndex(x => new
            {
                x.UserId,
                x.PlaceId,
                x.LeftAt
            });

        builder
            .Ignore(x => x.Duration);

        builder
            .OnInserted(UpdatePlaceVisitCount.Configure);
    }
}
