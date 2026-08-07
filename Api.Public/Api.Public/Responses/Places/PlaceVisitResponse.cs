using System;
using Svc.Places.Models.Data;

namespace Api.Public.Responses.Places;

/// <summary>
/// Place Visit Response.
/// </summary>
public class PlaceVisitResponse
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
    /// <param name="placeVisit">The <see cref="PlaceVisit"/>.</param>
    public PlaceVisitResponse(PlaceVisit placeVisit)
    {
        ArgumentNullException.ThrowIfNull(placeVisit);

        this.Id = placeVisit.Id;
        this.PlaceId = placeVisit.PlaceId;
        this.PlaceName = placeVisit.Place!.Name;
    }
}