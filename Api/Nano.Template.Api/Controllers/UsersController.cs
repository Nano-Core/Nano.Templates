using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Lib.Emailing.Interfaces;
using Lib.Emailing.Models;
using Lib.Sms.Interfaces;
using Lib.Sms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Requests.Auth;
using Nano.App.Api.Requests.Identity;
using Nano.Models;
using Nano.Models.Const;
using Nano.Security;
using Nano.Security.Extensions;
using Nano.Security.Models;
using Nano.Template.Api.Const;
using Nano.Template.Api.Controllers.Requests.Users;
using Nano.Template.Api.Controllers.Responses.Users;
using Nano.Template.Api.Extensions;
using Nano.Template.Service.Models.Api;
using Nano.Template.Service.Models.Data;
using Nano.Web.Controllers;
using AccessTokenResponse = Nano.Template.Api.Controllers.Responses.Users.AccessTokenResponse;
using ResetPasswordRequest = Nano.App.Api.Requests.Identity.ResetPasswordRequest;

namespace Nano.Template.Api.Controllers;

/// <inheritdoc />
public class UsersController : BaseController
{
    private const string RESET_PASSWORD_LINK_TEMPLATE = "auth/password/reset?userId={0}&token={1}";
    private const string CONFIRM_EMAIL_LINK_TEMPLATE = "auth/email/confirm?token={0}";
    private const string VERIFY_CHANGE_EMAIL_LINK_TEMPLATE = "auth/email/change?token={0}";

    /// <summary>
    /// Sms Service.
    /// </summary>
    protected virtual ISmsService SmsService { get; }

    /// <summary>
    /// Emailing Service.
    /// </summary>
    protected virtual IEmailingService EmailingService { get; }

    /// <summary>
    /// Web Api.
    /// </summary>
    protected virtual ServiceApi ServiceApi { get; }

    /// <inheritdoc />
    public UsersController(ILogger logger, ISmsService smsService, IEmailingService emailingService, ServiceApi serviceApi)
        : base(logger)
    {
        this.SmsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
        this.EmailingService = emailingService ?? throw new ArgumentNullException(nameof(emailingService));
        this.ServiceApi = serviceApi ?? throw new ArgumentNullException(nameof(serviceApi));
    }

    /// <summary>
    /// Gets logged in user.
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
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetUserAsync(CancellationToken cancellationToken = default)
    {
        var user = await this.ServiceApi
            .GetAsync<User>(this.UserId.GetValueOrDefault(), cancellationToken);

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
    [ProducesResponseType(typeof(SecurityOptions.PasswordOptions), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetPasswordOptionsAsync(CancellationToken cancellationToken = default)
    {
        var passwordOptions = await this.ServiceApi
            .GetPasswordOptionsAsync(new GetPasswordOptionsRequest(), cancellationToken);

        if (passwordOptions == null)
        {
            this.NotFound();
        }

        return Ok(passwordOptions);
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
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> LogInAsync([FromBody][Required]UserLogInRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var accessToken = await this.ServiceApi
                .LogInAsync(new LogInRequest
                {
                    LogIn = new LogIn
                    {
                        AppId = request.AppId,
                        Username = request.Username,
                        Password = request.Password,
                        IsRefreshable = true
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
    [ProducesResponseType(typeof(AccessTokenResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> LoginRefreshAsync([FromBody][Required]UserLogInRefreshRequest request, CancellationToken cancellationToken = default)
    {
        var token = this.HttpContext
            .GetJwtToken();

        if (token == null)
        {
            this.Unauthorized();
        }

        var accessToken = await this.ServiceApi
            .LogInRefreshAsync(new LogInRefreshRequest
            {
                LogInRefresh =
                {
                    Token = token,
                    RefreshToken = request.RefreshToken
                }
            }, cancellationToken);

        var response = new AccessTokenResponse(accessToken);

        return this.Ok(response);
    }

    /// <summary>
    /// Signs up a user.
    /// </summary>
    /// <param name="request">The signup request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The signup response.</returns>
    /// <response code="201">Created.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("signup")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> SignUpAsync([FromBody][Required]UserSignUpRequest request, CancellationToken cancellationToken = default)
    {
        var user = await this.ServiceApi
            .SignUpAsync(new SignUpRequest<User>
            {
                SignUp = new SignUp<User>
                {
                    Username = request.EmailAddress,
                    Password = request.Password,
                    ConfirmPassword = request.Password,
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber,
                    User = new User
                    {
                        Name = request.Name
                    }
                }
            }, cancellationToken);

        if (user == null)
        {
            return this.NotFound();
        }

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
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ForgotPasswordAsync([FromBody][Required] UserForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var passwordResetToken = await this.ServiceApi
            .GetResetPasswordTokenAsync(new GenerateResetPasswordTokenRequest
            {
                ResetPasswordToken =
                {
                    EmailAddress = request.EmailAddress
                }
            }, cancellationToken);

        var resetPasswordLink = this.GetResetPasswordLink(passwordResetToken.UserId, passwordResetToken.Token);

        await this.EmailingService
            .SendEmailTemplateAsync(new EmailTemplate
            {
                TemplateId = EmailTemplateIds.UserForgotPassword,
                Receiver =
                {
                    EmailAddress = request.EmailAddress
                },
                Data = new
                {
                    ResetPasswordLink = resetPasswordLink
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
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ResetPasswordAsync([FromBody][Required] UserResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        await this.ServiceApi
            .ResetPasswordAsync(new ResetPasswordRequest
            {
                ResetPassword =
                {
                    UserId = request.UserId,
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
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ChangePasswordAsync([FromBody][Required] UserChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var userId = this.HttpContext
            .GetJwtUserId();

        if (userId == null)
        {
            return this.NotFound();
        }

        await this.ServiceApi
            .ChangePasswordAsync(new ChangePasswordRequest
            {
                ChangePassword =
                {
                    UserId = userId.Value,
                    OldPassword = request.OldPassword,
                    NewPassword = request.NewPassword,
                    ConfirmNewPassword = request.NewPassword
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Get Confirm Email Token.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("email/confirm/token")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetConfirmEmailTokenAsync(CancellationToken cancellationToken = default)
    {
        var userId = this.HttpContext
            .GetJwtUserId();

        if (userId == null)
        {
            return this.NotFound();
        }

        var userEmail = this.HttpContext
            .GetJwtUserEmail();

        if (userEmail == null)
        {
            return this.NotFound();
        }

        var confirmEmailToken = await this.ServiceApi
            .GetConfirmEmailTokenAsync(new GenerateConfirmEmailTokenRequest
            {
                ConfirmEmailToken =
                {
                    UserId = userId.Value
                }
            }, cancellationToken);

        var confirmEmailLink = this.GetConfirmEmailLink(confirmEmailToken.Token);

        await this.EmailingService
            .SendEmailTemplateAsync(new EmailTemplate
            {
                TemplateId = EmailTemplateIds.UserConfirmEmail,
                Receiver =
                {
                    EmailAddress = userEmail
                },
                Data = new
                {
                    ConfirmEmailLink = confirmEmailLink
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
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ConfirmEmailAsync([FromBody][Required] UserConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        await this.ServiceApi
            .ConfirmEmailAsync(new ConfirmEmailRequest
            {
                ConfirmEmail =
                {
                    UserId = request.UserId,
                    Token = request.Token
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Get Change Email Token.
    /// </summary>
    /// <param name="request">The change email token request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("email/change/token")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetChangeEmailTokenAsync([FromBody][Required] UserChangeEmailTokenRequest request, CancellationToken cancellationToken = default)
    {
        var userId = this.HttpContext
            .GetJwtUserId();

        if (userId == null)
        {
            return this.NotFound();
        }

        var userEmail = this.HttpContext
            .GetJwtUserEmail();

        if (userEmail == null)
        {
            throw new NullReferenceException(nameof(userEmail));
        }

        var changeEmailToken = await this.ServiceApi
            .GetChangeEmailTokenAsync(new GenerateChangeEmailTokenRequest
            {
                ChangeEmailToken =
                {
                    UserId = userId.Value,
                    NewEmailAddress = request.NewEmailAddress
                }
            }, cancellationToken);


        var verifyChangeEmailLink = this.GetVerifyChangeEmailLink(changeEmailToken.Token);

        await this.EmailingService
            .SendEmailTemplateAsync(new EmailTemplate
            {
                TemplateId = EmailTemplateIds.UserChangeEmail,
                Receiver =
                {
                    EmailAddress = userEmail
                },
                Data = new
                {
                    VerifyChangeEmailLink = verifyChangeEmailLink
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
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ChangeEmailAsync([FromBody][Required]UserChangeEmailRequest request, CancellationToken cancellationToken = default)
    {
        await this.ServiceApi
            .ChangeEmailAsync(new ChangeEmailRequest
            {
                ChangeEmail =
                {
                    UserId = request.UserId,
                    Token = request.Token
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Get Confirm Phone Token.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("phone/confirm/token")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetConfirmPhoneTokenAsync(CancellationToken cancellationToken = default)
    {
        var userId = this.HttpContext
            .GetJwtUserId();

        if (userId == null)
        {
            return this.NotFound();
        }

        var user = await this.ServiceApi
            .GetAsync<User>(userId.Value, cancellationToken);

        var confirmPhoneNumberToken = await this.ServiceApi
            .GetConfirmPhoneTokenAsync(new GenerateConfirmPhoneTokenRequest
            {
                ConfirmPhoneToken =
                {
                    UserId = userId.Value
                }
            }, cancellationToken);

        await this.SmsService
            .SendSmsAsync(new Message
            {
                Text = confirmPhoneNumberToken.Token,
                Receiver =
                {
                    PhoneNumber = user.IdentityUser.PhoneNumber
                }
            }, cancellationToken);
        return this.Ok();
    }

    /// <summary>
    /// Confirm Phone.
    /// </summary>
    /// <param name="request">The confirm phone request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("phone/confirm")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ConfirmPhoneAsync([FromBody][Required]UserConfirmPhoneRequest request, CancellationToken cancellationToken = default)
    {
        var userId = this.HttpContext
            .GetJwtUserId();

        if (userId == null)
        {
            return this.NotFound();
        }

        await this.ServiceApi
            .ConfirmPhoneAsync(new ConfirmPhoneRequest
            {
                ConfirmPhone =
                {
                    UserId = userId.Value,
                    Token = request.Token
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Get Change Phone Token.
    /// </summary>
    /// <param name="request">The change phone token request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("phone/change/token")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetChangePhoneTokenAsync([FromBody][Required]UserChangePhoneTokenRequest request, CancellationToken cancellationToken = default)
    {
        var userId = this.HttpContext
            .GetJwtUserId();

        if (userId == null)
        {
            return this.NotFound();
        }

        var user = await this.ServiceApi
            .GetAsync<User>(userId.Value, cancellationToken);

        var changePhoneNumberToken = await this.ServiceApi
            .GetChangePhoneTokenAsync(new GenerateChangePhoneTokenRequest
            {
                ChangePhoneToken =
                {
                    UserId = userId.Value,
                    NewPhoneNumber = request.NewPhoneNumber
                }
            }, cancellationToken);

        await this.SmsService
            .SendSmsAsync(new Message
            {
                Text = changePhoneNumberToken.Token,
                Receiver =
                {
                    PhoneNumber = user.IdentityUser.PhoneNumber
                }
            }, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Change Phone.
    /// </summary>
    /// <param name="request">The change phone request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("phone/change")]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ChangePhoneAsync([FromBody][Required]UserChangePhoneRequest request, CancellationToken cancellationToken = default)
    {
        var userId = this.HttpContext
            .GetJwtUserId();

        if (userId == null)
        {
            return this.NotFound();
        }

        await this.ServiceApi
            .ChangePhoneAsync(new ChangePhoneRequest
            {
                ChangePhone =
                {
                    UserId = userId.Value,
                    Token = request.Token
                }
            }, cancellationToken);

        return this.Ok();
    }


    private string GetResetPasswordLink(Guid userId, string token)
    {
        var webUri = this.HttpContext
            .GetBaseWebUri();

        return string.Concat(webUri.AbsoluteUri, string.Format(UsersController.RESET_PASSWORD_LINK_TEMPLATE, userId, token));
    }
    private string GetConfirmEmailLink(string token)
    {
        var webUri = this.HttpContext
            .GetBaseWebUri();

        return string.Concat(webUri.AbsoluteUri, string.Format(UsersController.CONFIRM_EMAIL_LINK_TEMPLATE, token));
    }
    private string GetVerifyChangeEmailLink(string token)
    {
        var webUri = this.HttpContext
            .GetBaseWebUri();

        return string.Concat(webUri.AbsoluteUri, string.Format(UsersController.VERIFY_CHANGE_EMAIL_LINK_TEMPLATE, token));
    }
}