using System;
using System.Collections.Generic;
using DynamicExpression;
using Nano.App.Api.Controllers.Criteria;
using NetTopologySuite.Geometries;
using Svc.Places.Models.Criterias.Types;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Criterias;

/// <inheritdoc />
public class PlaceQueryCriteria : BaseQueryCriteria
{
    /// <summary>
    /// Id.
    /// </summary>
    public virtual Guid? Id { get; set; }

    /// <summary>
    /// Keyword.
    /// </summary>
    public virtual string? Keyword { get; set; }

    /// <summary>
    /// Viewport.
    /// </summary>
    public virtual Viewport? Viewport { get; set; }

    /// <inheritdoc />
    public override IList<CriteriaExpression> GetExpressions()
    {
        var expressions = base.GetExpressions();

        var expression = new CriteriaExpression();

        if (this.Id.HasValue)
        {
            expression
                .Equal(nameof(Place.Id), this.Id);
        }

        if (this.Keyword != null)
        {
            expression
                .StartsWith(nameof(Place.Name), this.Keyword);
        }

        if (this.Viewport != null)
        {
            var polygon = new Polygon(new LinearRing([
                new Coordinate(this.Viewport.SouthWest.Longitude, this.Viewport.SouthWest.Latitude),
                new Coordinate(this.Viewport.NorthEast.Longitude, this.Viewport.SouthWest.Latitude),
                new Coordinate(this.Viewport.NorthEast.Longitude, this.Viewport.NorthEast.Latitude),
                new Coordinate(this.Viewport.SouthWest.Longitude, this.Viewport.NorthEast.Latitude),
                new Coordinate(this.Viewport.SouthWest.Longitude, this.Viewport.SouthWest.Latitude)
            ]));

            expression
                .Intersects(nameof(Place.Name), polygon);
        }

        expressions
            .Add(expression);

        return expressions;
    }
}