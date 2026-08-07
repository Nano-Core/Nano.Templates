using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using Svc.Locations.Models.Data;
using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Svc.Locations.Models.Criterias;

/// <inheritdoc />
public class UserLocationQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// User Id.
    /// </summary>
    public virtual Guid? UserId { get; set; }

    /// <summary>
    /// Is Close To.
    /// </summary>
    public virtual Point? IsCloseTo { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (this.UserId.HasValue)
        {
            expression
                .Equal(nameof(UserLocation.UserId), this.UserId);
        }

        if (this.IsCloseTo != null)
        {
            expression
                .IsWithinDistance(nameof(UserLocation.Coordinate), this.IsCloseTo, 1000);
        }

        expressions
            .Add(expression);

        return expressions;
    }
}