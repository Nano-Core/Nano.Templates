using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Service.Models.Data.Types;

/// <summary>
/// City.
/// </summary>
public class City
{
    /// <summary>
    /// Country.
    /// </summary>
    [Required]
    public virtual Country Country { get; set; } = new();
}