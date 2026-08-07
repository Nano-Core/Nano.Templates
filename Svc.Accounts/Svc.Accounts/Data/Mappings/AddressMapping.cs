using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Svc.Accounts.Data.Mappings.Extensions;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Data.Mappings;

/// <inheritdoc />
public class AddressMapping : BaseEntityMapping<Address>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasOne(x => x.Country)
            .WithMany(x => x.Addresses)
            .IsRequired();

        builder
            .MapType(x => x.City);

        builder
            .Property(x => x.StreetName)
            .HasMaxLength(512)
            .IsRequired();

        builder
            .Property(x => x.StreetNameNormalized)
            .HasMaxLength(512)
            .IsRequired();

        builder
            .HasIndex(x => x.StreetNameNormalized);

        builder
            .Property(x => x.HouseNumber)
            .HasMaxLength(16)
            .IsRequired();
    }
}