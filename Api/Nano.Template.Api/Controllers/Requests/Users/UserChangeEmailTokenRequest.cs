using System;
using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Change Email Token Request.
/// </summary>
public class UserChangeEmailTokenRequest
{
    /// <summary>
    /// New Email Address.
    /// </summary>
    [Required]
    [EmailAddress]
    public virtual string NewEmailAddress { get; set; }
}