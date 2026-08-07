using System;
using System.Collections.Generic;
using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Criterias;

/// <inheritdoc />
public class PlaceFavoriteQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// Place Id.
    /// </summary>
    public virtual Guid? PlaceId { get; set; }

    /// <summary>
    /// User Id.
    /// </summary>
    public virtual Guid? UserId { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (this.PlaceId.HasValue)
        {
            expression
                .Equal(nameof(PlaceFavorite.PlaceId), this.PlaceId);
        }

        if (this.UserId.HasValue)
        {
            expression
                .Equal(nameof(PlaceFavorite.UserId), this.UserId);
        }
 
        expressions
            .Add(expression);

        return expressions;
    }
}