using System;
using Svc.Locations.Models.Criterias.Types;
using Svc.Locations.Models.Data;

namespace Api.Public.Responses.Users;

/// <summary>
/// User Location Response.
/// </summary>
public class UserLocationResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Coordinate.
    /// </summary>
    public LatLng Coordinate { get; set; }

    /// <summary>
    /// Created At.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userLocation">The <see cref="UserLocation"/>.</param>
    public UserLocationResponse(UserLocation userLocation)
    {
        ArgumentNullException.ThrowIfNull(userLocation);

        this.Id = userLocation.Id;
        this.Coordinate = new LatLng
        {
            Latitude = userLocation.Latitude,
            Longitude = userLocation.Longitude
        };
        this.CreatedAt = userLocation.CreatedAt;
    }
}