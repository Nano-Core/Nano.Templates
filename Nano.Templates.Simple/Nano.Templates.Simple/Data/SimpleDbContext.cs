using System;
using Microsoft.EntityFrameworkCore;
using Nano.Data;
using Nano.Data.Models.Mappings.Extensions;
using Nano.Services.Data;
using Nano.Templates.Simple.Data.Mappings;
using Nano.Templates.Simple.Models;

namespace Nano.Templates.Simple.Data
{
    /// <inheritdoc />
    public class SimpleDbContext : DefaultDbContext
    {
        /// <inheritdoc />
        public SimpleDbContext(DbContextOptions contextOptions, DataOptions dataOptions)
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
                .AddMapping<SimpleEntity, SimpleEntityMapping>();
        }
    }
}
