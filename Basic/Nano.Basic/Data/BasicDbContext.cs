using System;
using Microsoft.EntityFrameworkCore;
using Nano.Basic.Data.Mappings;
using Nano.Basic.Models;
using Nano.Data;
using Nano.Data.Models.Mappings.Extensions;

namespace Nano.Basic.Data
{
    /// <inheritdoc />
    public class BasicDbContext : DefaultDbContext
    {
        /// <inheritdoc />
        public BasicDbContext(DbContextOptions contextOptions, DataOptions dataOptions)
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
                .AddMapping<Person, PersonEntityMapping>();
        }
    }
}
