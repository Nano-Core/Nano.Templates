using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Change Email Token Request.
/// </summary>
public class ChangeEmailTokenRequest
{
    /// <summary>
    /// New Email Address.
    /// </summary>
    [Required]
    [EmailAddress]
    public virtual required string NewEmailAddress { get; set; }
}