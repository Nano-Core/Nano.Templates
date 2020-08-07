using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Template.Web.Models;

namespace Nano.Template.Web.Data.Mappings
{
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
                .Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder
                .HasIndex(x => x.Name);
        }
    }
}