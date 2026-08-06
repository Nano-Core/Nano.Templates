using Svc.Places.Models.Data;
using System;

namespace Api.Public.Responses.Places;

/// <summary>
/// Opening Hour Response.
/// </summary>
public class OpeningHourResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Place Id.
    /// </summary>
    public virtual Guid PlaceId { get; set; }

    /// <summary>
    /// Day Of Week.
    /// </summary>
    public virtual DayOfWeek DayOfWeek { get; set; }

    /// <summary>
    /// Opens At.
    /// </summary>
    public virtual TimeSpan OpensAt { get; set; }

    /// <summary>
    /// Closes At.
    /// </summary>
    public virtual TimeSpan ClosesAt { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="openingHour">The <see cref="OpeningHour"/>.</param>
    public OpeningHourResponse(OpeningHour openingHour)
    {
        ArgumentNullException.ThrowIfNull(openingHour);

        this.Id = openingHour.Id;
        this.PlaceId = openingHour.PlaceId;
        this.DayOfWeek = openingHour.DayOfWeek;
        this.OpensAt = openingHour.OpensAt;
        this.ClosesAt = openingHour.ClosesAt;
    }
}