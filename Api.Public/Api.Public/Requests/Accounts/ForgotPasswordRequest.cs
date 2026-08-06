using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Forgot Password Request.
/// </summary>
public class ForgotPasswordRequest
{
    /// <summary>
    /// Email Address.
    /// </summary>
    [Required]
    [EmailAddress]
    public virtual required string EmailAddress { get; set; }
}