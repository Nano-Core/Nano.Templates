using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Templates.Spatial.Models;

namespace Nano.Templates.Spatial.Data.Mappings
{
    /// <inheritdoc />
    public class SpatialEntityMapping : DefaultEntitySpatialMapping<SpatialEntity>
    {
        /// <inheritdoc />
        public override void Map(EntityTypeBuilder<SpatialEntity> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.Map(builder);

            builder
                .Property(x => x.PropertyOne);
        }
    }
}