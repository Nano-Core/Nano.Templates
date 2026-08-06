using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nano.Data.Abstractions.Models;

namespace Svc.Places.Models.Data;

/// <summary>
/// Opening Hour.
/// </summary>
public class OpeningHour : BaseEntity
{
    /// <summary>
    /// Place Id.
    /// </summary>
    [Required]
    public virtual Guid PlaceId { get; set; }

    /// <summary>
    /// Place.
    /// </summary>
    public virtual Place? Place { get; set; }

    /// <summary>
    /// Day Of Week.
    /// </summary>
    [Required]
    [DefaultValue(DayOfWeek.Sunday)]
    public virtual DayOfWeek DayOfWeek { get; set; } = DayOfWeek.Sunday;

    /// <summary>
    /// Opens At.
    /// </summary>
    [Required]
    public virtual TimeSpan OpensAt { get; set; }

    /// <summary>
    /// Closes At.
    /// </summary>
    [Required]
    public virtual TimeSpan ClosesAt { get; set; }
}