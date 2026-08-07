using Nano.Data.Abstractions.Eventing.Annotations;
using Nano.Data.Abstractions.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Svc.Places.Models.Data;

/// <summary>
/// User.
/// </summary>
[Subscribe]
public class User : BaseEntity
{
    /// <summary>
    /// Full Name.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string FullName { get; set; } = null!;

    /// <summary>
    /// Is Active.
    /// </summary>
    [Required]
    [DefaultValue(true)]
    public virtual bool IsActive { get; set; } = true;

    /// <summary>
    /// Place Favorites.
    /// </summary>
    public virtual ICollection<PlaceFavorite> PlaceFavorites { get; set; } = [];

    /// <summary>
    /// Place Visits.
    /// </summary>
    public virtual ICollection<PlaceVisit> PlaceVisits { get; set; } = [];
}