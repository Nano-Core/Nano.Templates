using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Nano.Common.Consts;
using Nano.Data.Abstractions.Annotations;
using Nano.Data.Abstractions.Config.Enums;
using Nano.Data.Abstractions.Models;
using NetTopologySuite.Geometries;
using Z.EntityFramework.Plus;

namespace Svc.Places.Models.Data;

/// <summary>
/// Place.
/// </summary>
public class Place : BaseEntity
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string Name
    {
        get;
        set
        {
            field = value;
            this.NameNormalized = value.ToUpper();
        }
    } = null!;

    /// <summary>
    /// Name Normalized.
    /// </summary>
    [Required]
    [MaxLength(128)]
    [AuditExclude]
    public virtual string NameNormalized { get; internal set; } = null!;

    /// <summary>
    /// Promotional Text.
    /// </summary>
    [Required]
    [MaxLength(8192)]
    public virtual string Description { get; set; } = null!;

    /// <summary>
    /// Address.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public virtual string Address { get; set; } = null!;

    /// <summary>
    /// Area.
    /// </summary>
    [Required]
    public virtual Polygon Area { get; set; } = null!;

    /// <summary>
    /// Favorite Count.
    /// </summary>
    [Required]
    [DefaultValue(0)]
    public virtual int FavoriteCount { get; set; } = 0;

    /// <summary>
    /// Visits Count.
    /// </summary>
    [Required]
    [DefaultValue(0)]
    public virtual int VisitsCount { get; set; } = 0;

    /// <summary>
    /// Latest Visit.
    /// </summary>
    public virtual DateTimeOffset? LatestVisit { get; set; }

    /// <summary>
    /// Is Open.
    /// </summary>
    [NotMapped]
    public virtual bool? IsOpen
    {
        get
        {
            var now = DateTimeOffset.UtcNow;

            return this.OpeningHours.Any()
                ? this.OpeningHours
                    .Any(y => y.DayOfWeek == now.DayOfWeek && y.OpensAt <= now.TimeOfDay && y.ClosesAt > now.TimeOfDay)
                : null;
        }
    }

    /// <summary>
    /// Opening Hours.
    /// </summary>
    [Include(QuerySplitBehavior.SplitQuery)]
    public virtual ICollection<OpeningHour> OpeningHours { get; set; } = [];

    /// <summary>
    /// Pictures.
    /// </summary>
    public virtual ICollection<PlacePicture> Pictures { get; set; } = [];

    /// <summary>
    /// Favorites.
    /// </summary>
    public virtual ICollection<PlaceFavorite> Favorites { get; set; } = [];

    /// <summary>
    /// Visits.
    /// </summary>
    public virtual ICollection<PlaceVisit> Visits { get; set; } = [];

    /// <summary>
    /// Has Overlapping Opening Hours.
    /// </summary>
    /// <param name="overlappingPairs"></param>
    /// <returns></returns>
    public virtual bool HasOverlappingOpeningHours(out List<(OpeningHour, OpeningHour)> overlappingPairs)
    {
        overlappingPairs = [];

        var groupedByDay = this.OpeningHours
            .GroupBy(x => x.DayOfWeek);

        foreach (var group in groupedByDay)
        {
            var sortedHours = group
                .OrderBy(o => o.OpensAt)
                .ToArray();

            for (var i = 0; i < sortedHours.Length - 1; i++)
            {
                var current = sortedHours[i];
                var next = sortedHours[i + 1];

                if (current.ClosesAt > next.OpensAt)
                {
                    overlappingPairs
                        .Add((current, next));
                }
            }
        }

        return overlappingPairs
            .Any();
    }

    /// <summary>
    /// Get Logo Filename.
    /// </summary>
    /// <returns>The filename</returns>
    public virtual string GetLogoFilename()
    {
        return $"{this.Id}_logo_{FileExtensions.PNG}";
    }
}