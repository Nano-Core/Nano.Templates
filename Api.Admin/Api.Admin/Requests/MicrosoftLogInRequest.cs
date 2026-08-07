using System.ComponentModel.DataAnnotations;

namespace Api.Admin.Requests;

/// <summary>
/// Microsoft Log In Request.
/// </summary>
public class MicrosoftLogInRequest
{
    /// <summary>
    /// The authorization code returned by the external provider.
    /// </summary>
    [Required]
    public virtual required string Code { get; set; }

    /// <summary>
    /// The PKCE code verifier associated with the authorization code.
    /// </summary>
    [Required]
    public virtual required string CodeVerifier { get; set; }

    /// <summary>
    /// The redirect URI used during the authentication flow.
    /// </summary>
    [Required]
    public virtual required string RedirectUri { get; set; }
}