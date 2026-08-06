using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nano.Data.Abstractions.Models;
using NetTopologySuite.Geometries;

namespace Svc.Locations.Models.Data;

/// <summary>
/// User Location.
/// </summary>
public class UserLocation : BaseEntity
{
    /// <summary>
    /// User Id.
    /// </summary>
    [Required]
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// User.
    /// </summary>
    public virtual User? User { get; set; }

    /// <summary>
    /// Coordinate.
    /// </summary>
    [Required]
    public virtual Point Coordinate { get; set; } = null!;

    /// <summary>
    /// Latitude.
    /// </summary>
    [NotMapped]
    public virtual double Latitude => this.Coordinate.Y;

    /// <summary>
    /// Longitude.
    /// </summary>
    [NotMapped]
    public virtual double Longitude => this.Coordinate.X;

    /// <summary>
    /// Altitude.
    /// </summary>
    [NotMapped]
    public virtual double Altitude => this.Coordinate.Z;

    /// <summary>
    /// Horizontal Accuracy.
    /// </summary>
    [Required]
    [DefaultValue(0)]
    public virtual double HorizontalAccuracy { get; set; } = 0;

    /// <summary>
    /// Vertical Accuracy.
    /// </summary>
    public virtual double? VerticalAccuracy { get; set; }

    /// <summary>
    /// Speed.
    /// </summary>
    public virtual double? Speed { get; set; }

    /// <summary>
    /// Course.
    /// </summary>
    public virtual double? Course
    {
        get;
        set
        {
            if (value < 0)
            {
                field = null;
                return;
            }

            field = value;
        }
    }
}