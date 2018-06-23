using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Data.Models.Mappings.Extensions;
using Nano.Templates.Advanced.Models;

namespace Nano.Templates.Advanced.Data.Mappings
{
    /// <inheritdoc />
    public class AdvancedNestedEntityMapping : DefaultEntityMapping<AdvancedNestedEntity>
    {
        /// <inheritdoc />
        public override void Map(EntityTypeBuilder<AdvancedNestedEntity> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.Map(builder);

            builder
                .OwnsOne(x => x.Nested)
                .MapType(x => x.Distance);

            builder
                .OwnsOne(x => x.Nested)
                .MapType(x => x.Duration);

            builder
                .OwnsOne(x => x.Nested)
                .MapType(x => x.EmailAddress);

            builder
                .OwnsOne(x => x.Nested)
                .MapType(x => x.Location);

            builder
                .OwnsOne(x => x.Nested)
                .MapType(x => x.Percentage);

            builder
                .OwnsOne(x => x.Nested)
                .MapType(x => x.PhoneNumber);
        }
    }
}