using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Forgot Password Request.
/// </summary>
public class UserForgotPasswordRequest
{
    /// <summary>
    /// Email Address.
    /// </summary>
    [Required]
    [EmailAddress]
    public virtual string EmailAddress { get; set; }
}