using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Set Password Request.
/// </summary>
public class SetPasswordRequest
{
    /// <summary>
    /// New Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string NewPassword { get; set; }
}