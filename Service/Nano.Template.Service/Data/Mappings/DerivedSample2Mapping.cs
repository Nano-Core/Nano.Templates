using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Template.Service.Models.Data;

namespace Nano.Template.Service.Data.Mappings;

/// <inheritdoc />
public class DerivedSample2Mapping : BaseEntityMapping<DerivedSample2>
{
    /// <inheritdoc />
    public override void Map(EntityTypeBuilder<DerivedSample2> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder
            .Property(x => x.Summary)
            .HasMaxLength(256)
            .IsRequired();
    }
}