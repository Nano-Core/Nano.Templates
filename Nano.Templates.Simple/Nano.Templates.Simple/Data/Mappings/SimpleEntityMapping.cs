using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Templates.Simple.Models;

namespace Nano.Templates.Simple.Data.Mappings
{
    /// <inheritdoc />
    public class SimpleEntityMapping : DefaultEntityMapping<SimpleEntity>
    {
        /// <inheritdoc />
        public override void Map(EntityTypeBuilder<SimpleEntity> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.Map(builder);

            builder
                .Property(x => x.PropertyOne)
                .IsRequired();

            builder
                .HasIndex(x => x.PropertyOne)
                .IsUnique();

            builder
                .Property(x => x.PropertyTwo);

            builder
                .HasOne(x => x.NestedSimpleEntity)
                .WithMany();
        }
    }
}