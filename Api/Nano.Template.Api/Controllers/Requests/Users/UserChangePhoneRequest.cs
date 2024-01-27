using System;
using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Change Phone Request.
/// </summary>
public class UserChangePhoneRequest
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
    /// New Phone Number.
    /// </summary>
    [Required]
    [Phone]
    public virtual string NewPhoneNumber { get; set; }
}