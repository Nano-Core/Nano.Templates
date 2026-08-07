using System.Collections.Generic;
using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Models.Criterias;

/// <inheritdoc />
public class CountryQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// Name.
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Code.
    /// </summary>
    public virtual string? Code { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (!string.IsNullOrEmpty(this.Name))
        {
            expression
                .StartsWith(nameof(Country.NameNormalized), this.Name.ToUpper());
        }

        if (!string.IsNullOrEmpty(this.Code))
        {
            expression
                .Equal(nameof(Country.Code), this.Code);
        }

        expressions
            .Add(expression);

        return expressions;
    }
}