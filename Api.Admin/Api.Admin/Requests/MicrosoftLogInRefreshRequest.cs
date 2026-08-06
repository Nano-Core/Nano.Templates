using System.ComponentModel.DataAnnotations;

namespace Api.Admin.Requests;

/// <summary>
/// Microsoft Log In Refresh Request.
/// </summary>
public class MicrosoftLogInRefreshRequest
{
    /// <summary>
    /// Refresh Token.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual required string RefreshToken { get; set; }
}