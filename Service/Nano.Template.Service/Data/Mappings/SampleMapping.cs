using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Template.Service.Models.Data;

namespace Nano.Template.Service.Data.Mappings;

/// <inheritdoc />
public class SampleMapping : DefaultEntityMapping<Sample>
{
    /// <inheritdoc />
    public override void Map(EntityTypeBuilder<Sample> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Map(builder);

        builder
            .HasDiscriminator<string>("type")
            .HasValue<DerivedSample1>(nameof(DerivedSample1))
            .HasValue<DerivedSample2>(nameof(DerivedSample2));

        builder
            .Property(x => x.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .HasIndex(x => x.Name);

        builder
            .Ignore(x => x.HasName);
    }
}