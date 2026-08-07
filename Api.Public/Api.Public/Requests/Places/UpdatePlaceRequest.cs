using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Responses.Places;

/// <summary>
/// UpdatePlace Request.
/// </summary>
public class UpdatePlaceRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Required]
    public virtual required Guid Id { get; set; }

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
}
