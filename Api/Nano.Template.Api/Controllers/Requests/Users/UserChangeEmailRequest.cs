using System;
using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Change Email Request.
/// </summary>
public class UserChangeEmailRequest
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
    /// New Email Address.
    /// </summary>
    [Required]
    [EmailAddress]
    public virtual string NewEmailAddress { get; set; }
}