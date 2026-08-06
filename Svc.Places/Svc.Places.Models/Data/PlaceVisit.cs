using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nano.Data.Abstractions.Annotations;
using Nano.Data.Abstractions.Models;

namespace Svc.Places.Models.Data;

/// <summary>
/// Place Visit.
/// </summary>
public class PlaceVisit : BaseEntity
{
    /// <summary>
    /// Place Id.
    /// </summary>
    [Required]
    public virtual Guid PlaceId { get; set; }

    /// <summary>
    /// Place.
    /// </summary>
    [Include]
    public virtual Place? Place { get; set; }

    /// <summary>
    /// User Id.
    /// </summary>
    [Required]
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// User.
    /// </summary>
    [Include]
    public virtual User? User { get; set; }

    /// <summary>
    /// Entered At.
    /// </summary>
    [Required]
    public virtual DateTimeOffset EnteredAt { get; set; }

    /// <summary>
    /// Left At.
    /// </summary>
    public virtual DateTimeOffset? LeftAt { get; set; }

    /// <summary>
    /// Duration.
    /// </summary>
    [NotMapped]
    public virtual TimeSpan Duration => (this.LeftAt ?? DateTimeOffset.UtcNow) - this.EnteredAt;
}