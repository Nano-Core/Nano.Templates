using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Change Password Request.
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// Old Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string OldPassword { get; set; }

    /// <summary>
    /// New Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string NewPassword { get; set; }
}