using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Change Email Request.
/// </summary>
public class ChangeEmailRequest
{
    /// <summary>
    /// Token.
    /// </summary>
    [Required]
    public virtual required string Token { get; set; }
}