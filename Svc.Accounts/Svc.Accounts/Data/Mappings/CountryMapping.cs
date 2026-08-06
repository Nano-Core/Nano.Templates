using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Data.Mappings;

/// <inheritdoc />
public class CountryMapping : BaseEntityMapping<Country>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

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
            .Property(x => x.Code)
            .HasMaxLength(5)
            .IsRequired();

        builder
            .HasIndex(x => x.Code)
            .IsUnique();

        builder
            .Property(x => x.PhonePrefix)
            .HasMaxLength(5)
            .IsRequired();

        builder
            .HasIndex(x => x.PhonePrefix)
            .IsUnique();

        builder
            .HasMany(x => x.Addresses)
            .WithOne(x => x.Country)
            .IsRequired();
    }
}