using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Template.Web.Models.Data;

namespace Nano.Template.Web.Data.Mappings;

/// <inheritdoc />
public class SampleMapping : DefaultEntityMapping<Sample>
//        where T : Sample
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