namespace Nano.Template.Service.Models.Data.Types;

/// <summary>
/// City.
/// </summary>
public class City
{
    /// <summary>
    /// Country.
    /// </summary>
    public virtual Country Country { get; set; } = new();
}