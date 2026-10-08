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
    protected override void ConfigureEntity(EntityTypeBuilder<OpeningHour> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

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