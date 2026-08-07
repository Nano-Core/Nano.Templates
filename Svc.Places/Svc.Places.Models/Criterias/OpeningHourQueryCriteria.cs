using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using Svc.Places.Models.Data;
using System;
using System.Collections.Generic;

namespace Svc.Places.Models.Criterias;

/// <inheritdoc />
public class OpeningHourQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// Place Id.
    /// </summary>
    public virtual Guid? PlaceId { get; set; }

    /// <summary>
    /// Day Of Week.
    /// </summary>
    public virtual DayOfWeek? DayOfWeek { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (this.PlaceId.HasValue)
        {
            expression
                .Equal(nameof(OpeningHour.PlaceId), this.PlaceId);
        }

        if (this.DayOfWeek.HasValue)
        {
            expression
                .Equal(nameof(OpeningHour.DayOfWeek), this.DayOfWeek);
        }

        expressions
            .Add(expression);

        return expressions;
    }
}