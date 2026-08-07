using NetTopologySuite.Geometries;
using Svc.Calendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Place = Svc.Places.Models.Data.Place;

namespace Api.Public.Responses.Places;

/// <summary>
/// Place Response.
/// </summary>
public class PlaceResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Area.
    /// </summary>
    public Polygon Area { get; set; }

    /// <summary>
    /// Area.
    /// </summary>
    public IEnumerable<OpeningHourResponse> OpeningHours { get; set; } = [];

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="place">The <see cref="Place"/>.</param>
    public PlaceResponse(Place place)
    {
        ArgumentNullException.ThrowIfNull(place);

        this.Id = place.Id;
        this.Name = place.Name;
        this.Description = place.Description;
        this.Address = place.Address;
        this.Area = place.Area;
        this.OpeningHours = place.OpeningHours
            .Select(x => new OpeningHourResponse(x));
    }
}