using System.ComponentModel.DataAnnotations;

namespace Svc.Places.Models.Criterias.Types;

/// <summary>
/// Viewport.
/// </summary>
public class Viewport
{
    /// <summary>
    /// South West.
    /// </summary>
    [Required]
    public virtual LatLng SouthWest { get; set; } = null!;

    /// <summary>
    /// North East.
    /// </summary>
    [Required]
    public virtual LatLng NorthEast { get; set; } = null!;
}