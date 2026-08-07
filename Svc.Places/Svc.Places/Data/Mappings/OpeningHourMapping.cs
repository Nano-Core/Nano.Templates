using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Places.Models.Data;

namespace Svc.Places.Data.Mappings;

/// <inheritdoc />
public class OpeningHourMapping : BaseEntityMapping<OpeningHour>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<OpeningHour> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasOne(x => x.Place)
            .WithMany(x => x.OpeningHours)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .Property(x => x.DayOfWeek)
            .HasDefaultValue(DayOfWeek.Sunday)
            .IsRequired();

        builder
            .Property(x => x.OpensAt)
            .IsRequired();

        builder
            .Property(x => x.ClosesAt)
            .IsRequired();
    }
}