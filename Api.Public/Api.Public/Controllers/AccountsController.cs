using Api.Public.Extensions;
using Api.Public.Requests.Accounts;
using Api.Public.Responses.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Annotations;
using Nano.App.ApiClient.Models;
using Nano.App.ApiClient.Requests;
using Nano.App.ApiClient.Requests.Auth;
using Nano.App.ApiClient.Requests.Identity;
using Nano.Common.Consts;
using Nano.Common.Extensions;
using Nano.Data.Abstractions.Identity.Authentication.Models;
using Nano.Data.Abstractions.Identity.Extensions;
using Nano.Data.Abstractions.Identity.Models;
using Svc.Accounts.Models.Api;
using Svc.Accounts.Models.Api.Requests.Enums;
using Svc.Accounts.Models.Data;
using Svc.Accounts.Models.Data.Types;
using Svc.Emailing.Models.Api;
using Svc.Emailing.Models.Data;
using Svc.Emailing.Models.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ChangeEmailRequest = Api.Public.Requests.Accounts.ChangeEmailRequest;
using ChangePasswordRequest = Api.Public.Requests.Accounts.ChangePasswordRequest;
using ConfirmEmailRequest = Api.Public.Requests.Accounts.ConfirmEmailRequest;
using LoginRefreshRequest = Api.Public.Requests.Accounts.LogInRefreshRequest;
using ResetPasswordRequest = Api.Public.Requests.Accounts.ResetPasswordRequest;
using User = Svc.Accounts.Models.Data.User;

namespace Api.Public.Controllers;

/// <inheritdoc />
public class AccountsController(ILogger<AccountsController> logger, AccountsApi accountsApi, EmailingApi emailingApi) 
    : BasePublicController(logger)
{
    private const string RESET_PASSWORD_LINK_TEMPLATE = "auth/password/reset?userId={0}&token={1}";
    private const string CONFIRM_EMAIL_LINK_TEMPLATE = "auth/email/confirm?token={0}";
    private const string VERIFY_CHANGE_EMAIL_LINK_TEMPLATE = "auth/email/change?token={0}";

    /// <summary>
    /// Gets My user.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user response.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpGet]
    [Route("me")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetMyUserAsync(CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        var user = await accountsApi.Entity
            .GetAsync<User>(jwtUserId, cancellationToken);

        if (user == null)
        {
            return this.NotFound();
        }

        var response = new UserResponse(user);

        return this.Ok(response);
    }

    /// <summary>
    /// Get password options.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The password options.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [AllowAnonymous]
    [Route("password/options")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(PasswordOptions), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetPasswordOptionsAsync(CancellationToken cancellationToken = default)
    {
        var passwordOptions = await accountsApi.Identity
            .GetPasswordOptionsAsync(cancellationToken);

        if (passwordOptions == null)
        {
            return this.NotFound();
        }

        return this.Ok(passwordOptions);
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response, containing the jwt baerer token.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(AccessToken), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> LogInAsync([FromBody][Required]LoginRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await accountsApi
                .GetUserAsync(request.EmailAddress, cancellationToken);

            if (user == null)
            {
                return this.NotFound();
            }

            var transientClaims = GetLoginTransientClaims(user);

            var accessToken = await accountsApi.Auth
                .LogInAsync(new LogInRequest
                {
                    LogIn = new LogIn
                    {
                        AppId = "Api.Public",
                        Username = request.EmailAddress,
                        Password = request.Password,
                        IsRefreshable = true,
                        TransientClaims = transientClaims
                    }
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
    [Route("login/refresh")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(AccessToken), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> LoginRefreshAsync([FromBody][Required]LoginRefreshRequest request, CancellationToken cancellationToken = default)
    {
        var jwtToken = this.HttpContext
            .GetJwtToken();

        if (jwtToken == null)
        {
            return this.Unauthorized();
        }

        var jwtUserEmail = this.HttpContext
            .GetJwtUserEmail();

        if (jwtUserEmail == null)
        {
            return this.Unauthorized();
        }

        try
        {
            var user = await accountsApi
                .GetUserAsync(jwtUserEmail, cancellationToken);

            if (user == null)
            {
                return this.NotFound();
            }

            var transientClaims = GetLoginTransientClaims(user);

            var accessToken = await accountsApi.Auth
                .LogInRefreshAsync(new Nano.App.ApiClient.Requests.Auth.LogInRefreshRequest
                {
                    LogInRefresh = new LogInRefresh
                    {
                        Token = jwtToken,
                        RefreshToken = request.RefreshToken,
                        TransientClaims = transientClaims
                    }
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
        await accountsApi.Auth
            .LogOutAsync(cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Is Email Taken.
    /// </summary>
    /// <param name="emailAddress">The email address</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Whether the email is taken.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [AllowAnonymous]
    [Route("is-email-taken")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> IsEmailTakenAsync([FromQuery][Required][EmailAddress]string emailAddress, CancellationToken cancellationToken = default)
    {
        var isEmailAddressTaken = await accountsApi.Identity
            .IsEmailAddressTakenAsync(new IsEmailAddressTakenRequest
            {
                EmailAddress = emailAddress
            }, cancellationToken);

        return this.Ok(isEmailAddressTaken.IsTaken);
    }

    /// <summary>
    /// Get All Tenants.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The tenants.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [AllowAnonymous]
    [Route("tenants/all")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<TenantResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetAllTenantsAsync(CancellationToken cancellationToken = default)
    {
        var tenants = await accountsApi
            .GetAllTenantsAsync(cancellationToken);

        var response = tenants
            .Select(x => new TenantResponse(x));

        return this.Ok(response);
    }

    /// <summary>
    /// Get All Countries.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The countries.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [AllowAnonymous]
    [Route("countries/all")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<CountryResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetAllCountriesAsync(CancellationToken cancellationToken = default)
    {
        var tenants = await accountsApi
            .GetAllCountriesAsync(cancellationToken);

        var response = tenants
            .Select(x => new CountryResponse(x));

        return this.Ok(response);
    }

    /// <summary>
    /// Signs up a user.
    /// </summary>
    /// <param name="request">The signup request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user response.</returns>
    /// <response code="201">Created.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("signup")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> SignUpAsync([FromBody][Required]SignUpRequest request, CancellationToken cancellationToken = default)
    {
        var signUpUser = new User
        {
            TenantId = request.TenantId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Address = new Address
            {
                CountryId = request.Address.CountryId,
                City = new City
                {
                    Name = request.Address.City.Name,
                    ZipCode = request.Address.City.ZipCode
                },
                StreetName = request.Address.StreetName,
                HouseNumber = request.Address.HouseNumber
            },
            Gender = request.Gender,
            Language = request.Language
        };

        var user = await accountsApi.Identity
            .SignUpAsync(new SignUpRequest<User>
            {
                SignUp = new SignUp<User>
                {
                    Username = request.EmailAddress,
                    Password = request.Password,
                    ConfirmPassword = request.Password,
                    EmailAddress = request.EmailAddress,
                    User = signUpUser,
                    Claims =
                    {
                        { "TenanId", request.TenantId.ToString() }
                    }
                }
            }, cancellationToken);

        await emailingApi
            .SendEmailAsync(new Email
            {
                Type = EmailType.Welcome,
                UserId = user.Id,
                Data =
                {
                    { "Name", user.FullName }
                }
            }, cancellationToken);

        var response = new UserResponse(user);

        return this.Created("signup", response);
    }

    /// <summary>
    /// Forgot password.
    /// </summary>
    /// <param name="request">The forgot password request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("password/forgot")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ForgotPasswordAsync([FromBody][Required]ForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var passwordResetToken = await accountsApi.Identity
            .GetResetPasswordTokenAsync(new GenerateResetPasswordTokenRequest
            {
                ResetPasswordToken = new GenerateResetPasswordToken 
                {
                    Username = request.EmailAddress
                }
            }, cancellationToken);

        var resetPasswordLink = this.GetResetPasswordLink(passwordResetToken.UserId, passwordResetToken.Token);

        await emailingApi
            .SendEmailAsync(new Email
            {
                UserId = passwordResetToken.UserId,
                Type = EmailType.ForgotPassword, 
                Data = 
                {
                    { "ResetPasswordLink", resetPasswordLink }
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Reset password.
    /// </summary>
    /// <param name="request">The reset password request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("password/reset")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ResetPasswordAsync([FromBody][Required]ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await accountsApi.Identity
            .ResetPasswordAsync(new Nano.App.ApiClient.Requests.Identity.ResetPasswordRequest
            {
                Id = request.UserId,
                ResetPassword = new ResetPassword
                {
                    Token = request.Token,
                    Password = request.Password
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Change Password.
    /// </summary>
    /// <param name="request">The change password request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("password/change")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ChangePasswordAsync([FromBody][Required]ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        await accountsApi.Identity
            .ChangePasswordAsync(new Nano.App.ApiClient.Requests.Identity.ChangePasswordRequest
            {
                Id = jwtUserId,
                ChangePassword = new ChangePassword
                {
                    OldPassword = request.OldPassword,
                    NewPassword = request.NewPassword,
                    ConfirmNewPassword = request.NewPassword
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Get Change Email Send.
    /// </summary>
    /// <param name="request">The change email token request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("email/change/send")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetChangeEmailSendAsync([FromBody][Required] ChangeEmailTokenRequest request, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        var changeEmailToken = await accountsApi.Identity
            .GetChangeEmailTokenAsync(new GenerateChangeEmailTokenRequest
            {
                Id = jwtUserId,
                ChangeEmailToken = new GenerateChangeEmailToken
                {
                    NewEmailAddress = request.NewEmailAddress
                }
            }, cancellationToken);

        var verifyChangeEmailLink = this.GetVerifyChangeEmailLink(changeEmailToken.Token);

        await emailingApi
            .SendEmailAsync(new Email
            {
                UserId = jwtUserId,
                Type = EmailType.ChangeEmail,
                Data =
                {
                    { "VerifyChangeEmailLink", verifyChangeEmailLink }
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Change Email.
    /// </summary>
    /// <param name="request">The change email request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("email/change")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ChangeEmailAsync([FromBody][Required] ChangeEmailRequest request, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        await accountsApi.Identity
            .ChangeEmailAsync(new Nano.App.ApiClient.Requests.Identity.ChangeEmailRequest
            {
                Id = jwtUserId,
                ChangeEmail = new ChangeEmail
                {
                    Token = request.Token
                },
                SetUsername = true
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Get Confirm Email Send.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("email/confirm/send")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetConfirmEmailSendAsync(CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        var confirmEmailToken = await accountsApi.Identity
            .GetConfirmEmailTokenAsync(new GenerateConfirmEmailTokenRequest
            {
                Id = jwtUserId
            }, cancellationToken);

        var confirmEmailLink = this.GetConfirmEmailLink(confirmEmailToken.Token);

        await emailingApi
            .SendEmailAsync(new Email
            {
                UserId = jwtUserId,
                Type = EmailType.ConfirmEmail,
                Data =
                {
                    { "ConfirmEmailLink", confirmEmailLink }
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Confirm Email.
    /// </summary>
    /// <param name="request">The confirm email request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("email/confirm")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ConfirmEmailAsync([FromBody][Required]ConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        await accountsApi.Identity
            .ConfirmEmailAsync(new Nano.App.ApiClient.Requests.Identity.ConfirmEmailRequest
            {
                Id = jwtUserId,
                ConfirmEmail = new ConfirmEmail
                {
                    Token = request.Token
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Update User.
    /// </summary>
    /// <param name="request">The update user request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The user response.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPut]
    [Route("update")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> UpdateAsync([FromBody][Required]UpdateRequest request, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        var user = await accountsApi.Entity
            .GetAsync<User>(jwtUserId, cancellationToken);

        if (user == null)
        {
            return this.NotFound();
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.DateOfBirth = request.DateOfBirth;
        user.Address?.City.Name = request.Address.City.Name;
        user.Address?.City.ZipCode = request.Address.City.ZipCode;
        user.Address?.CountryId = request.Address.CountryId;
        user.Address?.StreetName = request.Address.StreetName;
        user.Address?.HouseNumber = request.Address.HouseNumber;
        user.Gender = request.Gender;
        user.Language = request.Language;

        user = await accountsApi.Entity
            .EditAndGetAsync<User>(new EditAndGetRequest
            {
                Entity = user
            }, cancellationToken);

        if (user == null)
        {
            return this.NotFound();
        }

        var response = new UserResponse(user);

        return this.Ok(response);
    }

    /// <summary>
    /// Get user Picture.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="type">THe image type.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The user picture.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("{userId:guid}/picture/{type}")]
    [Produces(HttpContentType.JPEG, HttpContentType.PNG)]
    [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetPictureAsync([FromRoute][Required] Guid userId, [FromRoute][Required] ImageType type, CancellationToken cancellationToken = default)
    {
        var userPicture = await accountsApi
            .GetUserPictureAsync(userId, type, cancellationToken);

        if (userPicture == null)
        {
            return this.NotFound();
        }

        var extension = Path.GetExtension(userPicture.Name);

        var httpContentType = extension
            .GetHttpContentType();

        return this.File(userPicture.Stream, httpContentType, userPicture.Name);
    }

    /// <summary>
    /// Add User Picture.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="file">The file.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("{userId:guid}/picture/set")]
    [RequestSizeLimit(1024 * 1024 * 1024)]
    [Consumes(HttpContentType.FORM)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> AddPictureAsync([FromRoute][Required] Guid userId, [Required][FileExtensionValidation(FileExtensions.JPG, FileExtensions.JPEG, FileExtensions.PNG)] IFormFile file, CancellationToken cancellationToken = default)
    {
        await accountsApi
            .AddUserPictureAsync(userId, file, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Remove User Picture.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpDelete]
    [Route("{userId:guid}/picture/remove")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> RemovePictureAsync([FromRoute][Required] Guid userId, CancellationToken cancellationToken = default)
    {
        await accountsApi
            .RemoveUserPictureAsync(userId, cancellationToken);

        return this.Ok();
    }


    private string GetResetPasswordLink(Guid userId, string token)
    {
        var webUri = this.HttpContext
            .GetBaseWebUri();

        return string.Concat(webUri.AbsoluteUri, string.Format(AccountsController.RESET_PASSWORD_LINK_TEMPLATE, userId, token));
    }
    private string GetConfirmEmailLink(string token)
    {
        var webUri = this.HttpContext
            .GetBaseWebUri();

        return string.Concat(webUri.AbsoluteUri, string.Format(AccountsController.CONFIRM_EMAIL_LINK_TEMPLATE, token));
    }
    private string GetVerifyChangeEmailLink(string token)
    {
        var webUri = this.HttpContext
            .GetBaseWebUri();

        return string.Concat(webUri.AbsoluteUri, string.Format(AccountsController.VERIFY_CHANGE_EMAIL_LINK_TEMPLATE, token));
    }

    private static IDictionary<string, string> GetLoginTransientClaims(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new Dictionary<string, string>
        {
            { "IsPublic", "true" }
        };
    }
}