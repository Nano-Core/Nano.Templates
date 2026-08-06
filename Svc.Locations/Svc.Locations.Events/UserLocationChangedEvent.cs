using System;
using NetTopologySuite.Geometries;

namespace Svc.Locations.Events;

/// <summary>
/// User Location Event
/// </summary>
public class UserLocationChangedEvent
{
    /// <summary>
    /// User Id.
    /// </summary>
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// Created At.
    /// </summary>
    public virtual DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Coordinate.
    /// </summary>
    public virtual Point Coordinate { get; set; } = null!;
}