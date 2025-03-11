using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Models.Serialization.Json.Const;
using Nano.Template.Service.Models.Data;
using Newtonsoft.Json;
using Nano.Template.Service.Data.Mappings.Extensions;

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
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .Ignore(x => x.HasName);

        var jsonSerializerSettings = Globals.GetDefaultJsonSerializerSettings();

        builder
            .Property(x => x.JsonMapped)
            .HasConversion(
                x => JsonConvert.SerializeObject(x, jsonSerializerSettings),
                x => JsonConvert.DeserializeObject<IEnumerable<string>>(x, jsonSerializerSettings),
                ValueComparer.CreateDefault<IEnumerable<string>>(false));

        builder
            .MapType(x => x.City);
    }
}