using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Change Phone Token Request.
/// </summary>
public class UserChangePhoneTokenRequest
{
    /// <summary>
    /// New Phone Number.
    /// </summary>
    [Required]
    [Phone]
    public virtual string NewPhoneNumber { get; set; }
}