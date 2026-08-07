using System;
using System.Collections.Generic;
using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Criterias;

/// <inheritdoc />
public class PlacePictureQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// Place Id.
    /// </summary>
    public virtual Guid? PlaceId { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (this.PlaceId.HasValue)
        {
            expression
                .Equal(nameof(PlacePicture.PlaceId), this.PlaceId);
        }

        expressions
            .Add(expression);

        return expressions;
    }
}