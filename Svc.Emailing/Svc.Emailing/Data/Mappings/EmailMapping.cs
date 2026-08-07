using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings;
using Newtonsoft.Json;
using Svc.Emailing.Models.Data;
using Svc.Emailing.Models.Data.Enums;

namespace Svc.Emailing.Data.Mappings;

/// <inheritdoc />
public class EmailMapping : BaseEntityMapping<Email>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Email> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Emails)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .Property(x => x.Type)
            .HasDefaultValue(EmailType.None)
            .IsRequired();

        builder
            .HasIndex(x => x.Type);

        builder
            .Property(x => x.Data)
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<IDictionary<string, string>>(x) ?? new Dictionary<string, string>())
            .Metadata.SetValueComparer(
                new ValueComparer<IDictionary<string, string>>(
                    (a, b) => a!.SequenceEqual(b!),
                    x => x.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.Key, item.Value)),
                    x => x.ToDictionary(x => x.Key, x => x.Value)));
    }
}