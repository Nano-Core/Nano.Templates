using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Login Request.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Email Address.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string EmailAddress { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string Password { get; set; }
}