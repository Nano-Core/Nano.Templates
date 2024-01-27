using System;
using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Confirm Phone Request.
/// </summary>
public class UserConfirmPhoneRequest
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