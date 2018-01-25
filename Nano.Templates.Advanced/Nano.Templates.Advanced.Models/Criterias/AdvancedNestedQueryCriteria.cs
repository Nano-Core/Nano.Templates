﻿using DynamicExpression;
using Nano.Models.Criterias;

namespace Nano.Templates.Advanced.Models.Criterias
{
    /// <inheritdoc />
    public class AdvancedNestedQueryCriteria : DefaultQueryCriteria
    {
        /// <inheritdoc />
        public override CriteriaExpression GetExpression<TEntity>()
        {
            var filter = base.GetExpression<TEntity>();

            return filter;
        }
    }
}