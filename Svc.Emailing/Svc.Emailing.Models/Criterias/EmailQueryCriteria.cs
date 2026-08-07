using System;
using System.Collections.Generic;
using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using Svc.Emailing.Models.Data;

namespace Svc.Emailing.Models.Criterias;

/// <inheritdoc />
public class EmailQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// User Id.
    /// </summary>
    public virtual Guid? UserId { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (this.UserId.HasValue)
        {
            expression
                .Equal(nameof(Email.UserId), this.UserId);
        }

        expressions
            .Add(expression);

        return expressions;
    }
}