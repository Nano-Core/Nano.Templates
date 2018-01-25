using DynamicExpression;
using Nano.Models.Criterias;

namespace Nano.Templates.Spatial.Models.Criterias
{
    /// <inheritdoc />
    public class SpatialQueryCriteria : DefaultQueryCriteriaSpatial
    {
        /// <inheritdoc />
        public override CriteriaExpression GetExpression<TEntity>()
        {
            var filter = base.GetExpression<TEntity>();

            return filter;
        }
    }
}