using System;
using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Confirm Email Request.
/// </summary>
public class UserConfirmEmailRequest
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
}