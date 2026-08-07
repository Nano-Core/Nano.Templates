using System;
using System.ComponentModel.DataAnnotations;
using Nano.Data.Abstractions.Annotations;
using Nano.Data.Abstractions.Models;

namespace Svc.Places.Models.Data;

/// <summary>
/// Place Favorite.
/// </summary>
public class PlaceFavorite : BaseEntity
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
}