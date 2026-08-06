using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using Svc.Accounts.Models.Data;
using System.Collections.Generic;

namespace Svc.Accounts.Models.Criterias;

/// <inheritdoc />
public class TenantQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// Name.
    /// </summary>
    public virtual string? Name { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (!string.IsNullOrEmpty(this.Name))
        {
            expression
                .StartsWith(nameof(Tenant.NameNormalized), this.Name.ToUpper());
        }

        expressions
            .Add(expression);

        return expressions;
    }
}