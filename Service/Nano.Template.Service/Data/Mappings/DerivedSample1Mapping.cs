﻿using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Template.Service.Models.Data;

namespace Nano.Template.Service.Data.Mappings;

/// <inheritdoc />
public class DerivedSample1Mapping : BaseEntityMapping<DerivedSample1>
{
    /// <inheritdoc />
    public override void Map(EntityTypeBuilder<DerivedSample1> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder
            .Property(x => x.Description)
            .HasMaxLength(256)
            .IsRequired();
    }
}