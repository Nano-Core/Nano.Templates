using System;
using Microsoft.EntityFrameworkCore;
using Nano.Data;
using Nano.Data.Models.Mappings.Extensions;
using Nano.Services.Data;
using Nano.Templates.Spatial.Data.Mappings;
using Nano.Templates.Spatial.Models;

namespace Nano.Templates.Spatial.Data
{
    /// <inheritdoc />
    public class SpatialDbContext : DefaultDbContext
    {
        /// <inheritdoc />
        public SpatialDbContext(DbContextOptions contextOptions, DataOptions dataOptions)
            : base(contextOptions, dataOptions)
        {

        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.OnModelCreating(builder);

            builder
                .AddMapping<SpatialEntity, SpatialEntityMapping>();
        }
    }
}
