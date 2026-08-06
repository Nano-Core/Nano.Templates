using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Reset Password Request.
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// User Id.
    /// </summary>
    [Required]
    public virtual required Guid UserId { get; set; }

    /// <summary>
    /// Token.
    /// </summary>
    [Required]
    public virtual required string Token { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string Password { get; set; }
}