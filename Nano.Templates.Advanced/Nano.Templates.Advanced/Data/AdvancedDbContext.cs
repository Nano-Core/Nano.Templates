using System;
using Microsoft.EntityFrameworkCore;
using Nano.Data;
using Nano.Data.Models.Mappings.Extensions;
using Nano.Templates.Advanced.Data.Mappings;
using Nano.Templates.Advanced.Models;

namespace Nano.Templates.Advanced.Data
{
    /// <inheritdoc />
    public class AdvancedDbContext : DefaultDbContext
    {
        /// <inheritdoc />
        public AdvancedDbContext(DbContextOptions contextOptions, DataOptions dataOptions)
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
                .AddMapping<AdvancedEntity, AdvancedEntityMapping>()
                .AddMapping<AdvancedNestedEntity, AdvancedNestedEntityMapping>();
        }
    }
}
