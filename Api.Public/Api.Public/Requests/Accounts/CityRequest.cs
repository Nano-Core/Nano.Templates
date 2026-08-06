using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// City Request.
/// </summary>
public class CityRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string Name { get; set; }

    /// <summary>
    /// Zip Code.
    /// </summary>
    [Required]
    [MaxLength(32)]
    public virtual required string ZipCode { get; set; }
}