using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Sign Up Request.
/// </summary>
public class UserSignUpRequest
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
    [EmailAddress]
    public virtual string EmailAddress { get; set; }

    /// <summary>
    /// Phone Number.
    /// </summary>
    [Phone]
    public virtual string PhoneNumber { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string Password { get; set; }
}