using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Data.Models.Mappings.Extensions;
using Nano.Templates.Advanced.Models;

namespace Nano.Templates.Advanced.Data.Mappings
{
    /// <inheritdoc />
    public class AdvancedEntityMapping : DefaultEntityMapping<AdvancedEntity>
    {
        /// <inheritdoc />
        public override void Map(EntityTypeBuilder<AdvancedEntity> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.Map(builder);

            builder
                .MapType(x => x.Distance);

            builder
                .MapType(x => x.Duration);

            builder
                .MapType(x => x.EmailAddress);

            builder
                .MapType(x => x.Location);

            builder
                .MapType(x => x.Percentage);

            builder
                .MapType(x => x.PhoneNumber);
        }
    }
}