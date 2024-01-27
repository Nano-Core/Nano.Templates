using System;
using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Reset Password Request.
/// </summary>
public class UserResetPasswordRequest
{
    /// <summary>
    /// User Id.
    /// </summary>
    [Required]
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// Token.
    /// </summary>
    [Required]
    public virtual string Token { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string Password { get; set; }
}