using System.ComponentModel.DataAnnotations;
using Nano.Models.Types;

namespace Nano.Template.Api.Models.Requests.Profiles;

/// <summary>
/// Sign Up Request.
/// </summary>
public class SignUpRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string Name { get; set; }

    /// <summary>
    /// Email Address.
    /// </summary>
    [Required]
    public virtual EmailAddress EmailAddress { get; set; } = new();

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string Password { get; set; }
}