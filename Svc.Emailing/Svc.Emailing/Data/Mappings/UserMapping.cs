using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Emailing.Models.Data;

namespace Svc.Emailing.Data.Mappings;

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
            .Property(x => x.Email)
            .HasMaxLength(512)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder
            .HasIndex(x => x.IsActive);

        builder
            .HasMany(x => x.Emails)
            .WithOne(x => x.User)
            .IsRequired();
    }
}