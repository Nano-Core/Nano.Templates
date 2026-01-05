using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nano.Data;
using Nano.Data.Abstractions.Config;
using Nano.Data.Mappings.Extensions;
using Nano.Eventing.Abstractions;
using Nano.Template.Service.Data.Mappings;
using Nano.Template.Service.Models.Data;

namespace Nano.Template.Service.Data;

/// <inheritdoc />
public class ServiceDbContext : DefaultDbContext
{
    /// <inheritdoc />
    public ServiceDbContext(DbContextOptions dbContextOptions, IOptionsMonitor<DataOptions> dataOptions, IEventing eventing = null)
        : base(dbContextOptions, dataOptions, eventing)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
            throw new ArgumentNullException(nameof(modelBuilder));

        base.OnModelCreating(modelBuilder);

        modelBuilder
            .AddMapping<Sample, SampleMapping>()
            .AddMapping<DerivedSample1, DerivedSample1Mapping>()
            .AddMapping<DerivedSample2, DerivedSample2Mapping>()
            .AddMapping<User, UserMapping>();
    }
}