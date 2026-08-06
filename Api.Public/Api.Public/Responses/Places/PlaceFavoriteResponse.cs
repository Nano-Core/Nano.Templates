using System;
using Svc.Places.Models.Data;

namespace Api.Public.Responses.Places;

/// <summary>
/// Place Favorite Response.
/// </summary>
public class PlaceFavoriteResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Place Id.
    /// </summary>
    public Guid PlaceId { get; set; }

    /// <summary>
    /// Place Name.
    /// </summary>
    public string PlaceName { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="placeFavorite">The <see cref="PlaceFavorite"/>.</param>
    public PlaceFavoriteResponse(PlaceFavorite placeFavorite)
    {
        ArgumentNullException.ThrowIfNull(placeFavorite);

        this.Id = placeFavorite.Id;
        this.PlaceId = placeFavorite.PlaceId;
        this.PlaceName = placeFavorite.Place!.Name;
    }
}