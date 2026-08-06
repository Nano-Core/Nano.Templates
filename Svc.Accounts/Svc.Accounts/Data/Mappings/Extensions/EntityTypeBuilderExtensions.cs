using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Abstractions.Models.Abstractions;
using Svc.Accounts.Models.Data.Types;

namespace Svc.Accounts.Data.Mappings.Extensions;

internal static class EntityTypeBuilderExtensions
{
    internal static void MapType<TEntity>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, City?>> expression)
        where TEntity : class, IEntity
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        builder
            .OwnsOne(expression)
            .Property(x => x.Name)
            .HasMaxLength(128);

        builder
            .OwnsOne(expression)
            .Property(x => x.Name);

        builder
            .OwnsOne(expression)
            .Property(x => x.NameNormalized)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .OwnsOne(expression)
            .HasIndex(x => x.NameNormalized);

        builder
            .OwnsOne(expression)
            .Property(x => x.ZipCode)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .OwnsOne(expression)
            .HasIndex(x => x.ZipCode);
    }
}