using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nano.Data.Abstractions.Models;

namespace Svc.Places.Models.Data;

/// <summary>
/// Place Picture.
/// </summary>
public class PlacePicture : BaseEntity
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
    /// Order Index.
    /// </summary>
    [Required]
    [DefaultValue(0)]
    public virtual int OrderIndex { get; set; } = 0;
}