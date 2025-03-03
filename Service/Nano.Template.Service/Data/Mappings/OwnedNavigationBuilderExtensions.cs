using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Template.Service.Models.Data.Types;

namespace Nano.Template.Service.Data.Mappings;

/// <summary>
/// Owned Navigation Builder Extensions.
/// </summary>
public static class OwnedNavigationBuilderExtensions
{
    /// <summary>
    /// Maps <see cref="Country"/> for the <typeparamref name="TRelatedEntity"/> owned by <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TRelatedEntity">The related entity type.</typeparam>
    /// <param name="builder">The <see cref="OwnedNavigationBuilder{TEntity,TRelatedEntity}"/>.</param>
    /// <param name="expression">The <see cref="Expression"/>.</param>
    public static void MapType<TEntity, TRelatedEntity>(this OwnedNavigationBuilder<TEntity, TRelatedEntity> builder, Expression<Func<TRelatedEntity, Country>> expression)
        where TEntity : class
        where TRelatedEntity : class
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        if (expression == null)
            throw new ArgumentNullException(nameof(expression));

        builder
            .OwnsOne(expression)
            .Property(x => x.Code);
    }
}