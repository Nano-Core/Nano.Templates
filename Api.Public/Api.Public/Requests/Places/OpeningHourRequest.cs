using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Responses.Places;

/// <summary>
/// Opening Hour Request.
/// </summary>
public class OpeningHourRequest
{
    /// <summary>
    /// Day Of Week.
    /// </summary>
    [Required]
    public virtual DayOfWeek DayOfWeek { get; set; }

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