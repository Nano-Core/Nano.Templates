using System.ComponentModel.DataAnnotations;

namespace Api.Admin.Requests;

/// <summary>
/// Update Tenant Request.
/// </summary>
public class UpdateTenantRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string Name { get; set; }
}