using System;
using Microsoft.EntityFrameworkCore;
using Nano.Data;

namespace Nano.Template.Data
{
    /// <inheritdoc />
    public class WebDbContext : DefaultDbContext
    {
        /// <inheritdoc />
        public WebDbContext(DbContextOptions contextOptions, DataOptions dataOptions)
            : base(contextOptions, dataOptions)
        {

        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.OnModelCreating(builder);
        }
    }
}
