using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Data.Mappings;

/// <inheritdoc />
public class TenantMapping : BaseEntityMapping<Tenant>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Tenant> builder)
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
            .HasMany(x => x.Users)
            .WithOne(x => x.Tenant)
            .IsRequired();
    }
}