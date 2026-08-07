using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Confirm Email Request.
/// </summary>
public class ConfirmEmailRequest
{
    /// <summary>
    /// Token.
    /// </summary>
    [Required]
    public virtual required string Token { get; set; }
}