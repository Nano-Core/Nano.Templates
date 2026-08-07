using System.ComponentModel.DataAnnotations;

namespace Api.Admin.Requests;

/// <summary>
/// Create Country Request.
/// </summary>
public class CreateCountryRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string Name { get; set; }

    /// <summary>
    /// Code.
    /// ISO 3166-1 alpha-2.
    /// </summary>
    [Required]
    [MaxLength(5)]
    public virtual required string Code { get; set; }

    /// <summary>
    /// Phone Prefix.
    /// </summary>
    [Required]
    [MaxLength(5)]
    public virtual required string PhonePrefix { get; set; }
}