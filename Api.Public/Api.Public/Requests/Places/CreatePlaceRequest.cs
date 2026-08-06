using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Responses.Places;

/// <summary>
/// Create Place Request.
/// </summary>
public class CreatePlaceRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string Name { get; set; }

    /// <summary>
    /// Description.
    /// </summary>
    [Required]
    [MaxLength(8192)]
    public virtual required string Description { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public virtual required string Address { get; set; }

    /// <summary>
    /// Area.
    /// </summary>
    [Required]
    public virtual required Polygon Area { get; set; }

    /// <summary>
    /// Opening Hours.
    /// </summary>
    public IEnumerable<OpeningHourRequest> OpeningHours { get; set; } = [];
}