using System.ComponentModel.DataAnnotations;

namespace Api.Admin.Requests;

/// <summary>
/// Update Country Request.
/// </summary>
public class UpdateCountryRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string Name { get; set; }
}