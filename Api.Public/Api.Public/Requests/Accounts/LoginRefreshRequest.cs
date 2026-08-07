using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Login Refresh Request.
/// </summary>
public class LogInRefreshRequest
{
    /// <summary>
    /// Refresh Token.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string RefreshToken { get; set; }
}