using System.Collections.Generic;
using System.Linq;
using DynamicExpression;
using Nano.Models.Criterias;

namespace Nano.Template.Service.Models.Criterias;

/// <inheritdoc />
public class SampleQueryCriteria : DefaultQueryCriteria
{
    /// <summary>
    /// Name.
    /// </summary>
    public virtual string Name { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = expressions.FirstOrDefault() ?? new CriteriaExpression();

        if (!string.IsNullOrEmpty(this.Name))
            expression.StartsWith("Name", this.Name);

        expressions
            .Add(expression);

        return expressions;
    }
}