using System;
using Microsoft.EntityFrameworkCore;
using Nano.Data;
using Nano.Data.Models.Mappings.Extensions;
using Nano.Template.Console.Data.Mappings;
using Nano.Template.Console.Models;

namespace Nano.Template.Console.Data
{
    /// <inheritdoc />
    public class ConsoleDbContext : DefaultDbContext
    {
        /// <inheritdoc />
        public ConsoleDbContext(DbContextOptions dbContextOptions, DataOptions dataOptions)
            : base(dbContextOptions, dataOptions)
        {

        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);

            modelBuilder
                .AddMapping<Sample, SampleMapping>();
        }
    }
}