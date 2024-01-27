using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Change Password Request.
/// </summary>
public class UserChangePasswordRequest
{
    /// <summary>
    /// Old Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string OldPassword { get; set; }

    /// <summary>
    /// New Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string NewPassword { get; set; }
}