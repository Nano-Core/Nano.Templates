using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Models.Interfaces;
using Nano.Template.Service.Models.Data.Types;

namespace Nano.Template.Service.Data.Mappings.Extensions;

/// <summary>
/// Entity Type Builder Extensions.
/// </summary>
public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// Maps <see cref="City"/> as owned by the entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
    /// <param name="expression">The property expression.</param>
    public static void MapType<TEntity>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, City>> expression)
        where TEntity : class, IEntity
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        builder
            .OwnsOne(expression)
            .MapType(x => x.Country);
    }
}