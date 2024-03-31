using System;
using Nano.Security.Models;

namespace Nano.Template.Api.Controllers.Responses.Users;

/// <summary>
/// Access Token Response.
/// </summary>
public class AccessTokenResponse
{
    /// <summary>
    /// App Id.
    /// </summary>
    public string AppId { get; set; }

    /// <summary>
    /// User Id.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Token.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Expire At.
    /// </summary>
    public DateTimeOffset ExpireAt { get; set; }

    /// <summary>
    /// Refresh Token.
    /// </summary>
    public RefreshToken RefreshToken { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="accessToken">The <see cref="AccessToken"/>.</param>
    public AccessTokenResponse(AccessToken accessToken)
    {
        if (accessToken == null)
            throw new ArgumentNullException(nameof(accessToken));

        this.AppId = accessToken.AppId;
        this.UserId = accessToken.UserId;
        this.Token = accessToken.Token;
        this.ExpireAt = accessToken.ExpireAt;
        this.RefreshToken = accessToken.RefreshToken == null
            ? null
            : new RefreshToken
            {
                Token = accessToken.RefreshToken.Token,
                ExpireAt = accessToken.RefreshToken.ExpireAt
            };
    }
}