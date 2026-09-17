using Api.Admin.Consts;
using Api.Admin.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Mvc.Authentication.Abstractions;
using Nano.Common.Consts;
using Nano.Data.Abstractions.Identity.Authentication.Consts;
using Nano.Data.Abstractions.Identity.Authentication.Models;
using Nano.Data.Abstractions.Identity.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Admin.Controllers;

/// <inheritdoc />
public class AccountsController(ILogger<AccountsController> logger, IAuthTransientRepository authTransientRepository)
    : BaseAdminController(logger)
{
    /// <summary>
    /// Logs in a user with Microsoft external authentication. Access is controlled entirely by the Entra ID
    /// app registration's user/group assignment - anyone who completes this flow is treated as an admin, so
    /// only assigned users/groups may sign in to the app registration itself.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response, containing the jwt bearer token.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("login/microsoft")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(AccessToken), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> LogInAsync([FromBody][Required] MicrosoftLogInRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var accessToken = await authTransientRepository
                .LogInExternalAsync(BuiltInExternalLogInProviderNames.MICROSOFT, new LogInExternal<AuthCodeFlow>
                {
                    Flow = new AuthCodeFlow
                    {
                        Code = request.Code,
                        CodeVerifier = request.CodeVerifier,
                        RedirectUri = request.RedirectUri
                    },
                    TransientClaims = GetLoginTransientClaims(),
                    TransientRoles = []
                }, cancellationToken);

            if (accessToken == null)
            {
                throw new NullReferenceException(nameof(accessToken));
            }

            return this.Ok(accessToken);
        }
        catch (Exception ex)
        {
            this.Logger
                .LogError(ex, ex.Message);

            return this.Unauthorized();
        }
    }

    /// <summary>
    /// Refresh Login of a user.
    /// </summary>
    /// <param name="request">The login refresh request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The access token.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [AllowAnonymous]
    [Route("login/refresh/microsoft")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(AccessToken), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> LoginRefreshAsync([FromBody][Required] MicrosoftLogInRefreshRequest request, CancellationToken cancellationToken = default)
    {
        var jwtToken = this.HttpContext
            .GetJwtToken();

        if (jwtToken == null)
        {
            return this.Unauthorized();
        }

        try
        {
            var accessToken = await authTransientRepository
                .LogInExternalRefreshAsync(BuiltInExternalLogInProviderNames.MICROSOFT, new LogInRefresh
                {
                    Token = jwtToken,
                    RefreshToken = request.RefreshToken,
                    TransientClaims = GetLoginTransientClaims(),
                    TransientRoles = []
                }, cancellationToken);

            return this.Ok(accessToken);
        }
        catch (Exception ex)
        {
            this.Logger
                .LogError(ex, ex.Message);

            return this.Unauthorized();
        }
    }

    /// <summary>
    /// Log Out.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Void.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("logout")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> LogOutAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        return this.Ok();
    }


    private static IDictionary<string, string> GetLoginTransientClaims()
    {
        return new Dictionary<string, string>
        {
            { CustomClaimTypes.IS_ADMIN, "true" }
        };
    }
}
