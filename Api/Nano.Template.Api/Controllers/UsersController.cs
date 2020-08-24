using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.Security.Models;
using Nano.Template.Api.Models.Requests.Profiles;
using Nano.Template.Api.Models.Responses.Profiles;
using Nano.Template.Web.Models;
using Nano.Template.Web.Models.Api;
using Nano.Web.Api.Requests.Auth;
using Nano.Web.Api.Requests.Identity;
using Nano.Web.Const;
using Nano.Web.Controllers;
using Nano.Web.Models;

namespace Nano.Template.Api.Controllers
{
    /// <inheritdoc />
    public class UsersController : BaseController
    {
        /// <summary>
        /// Web Api.
        /// </summary>
        protected virtual WebApi WebApi { get; }

        /// <inheritdoc />
        public UsersController(ILogger logger, WebApi webApi)
            : base(logger)
        {
            this.WebApi = webApi ?? throw new ArgumentNullException(nameof(webApi));
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
        [Produces(HttpContentType.JSON, HttpContentType.XML)]
        [ProducesResponseType(typeof(GetUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public virtual async Task<IActionResult> GetUserAsync(CancellationToken cancellationToken = default)
        {
            var user = await this.WebApi
                .GetAsync<User>(this.UserId.GetValueOrDefault(), cancellationToken);

            if (user == null)
                return this.NotFound();

            var response = new GetUserResponse(user);

            return this.Ok(response);
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
        [Consumes(HttpContentType.JSON, HttpContentType.XML)]
        [Produces(HttpContentType.JSON, HttpContentType.XML)]
        [ProducesResponseType(typeof(AccessToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public virtual async Task<IActionResult> LogInAsync([FromBody][Required]SignInRequest request, CancellationToken cancellationToken = default)
        {
            var accessToken = await this.WebApi
                .LogInAsync(new LogInRequest
                {
                    Login = new Login
                    {
                        AppId = request.AppId,
                        Username = request.Username,
                        Password = request.Password
                    }
                }, cancellationToken);

            if (accessToken == null)
                return this.NotFound();

            return this.Ok(accessToken);
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
        [Consumes(HttpContentType.JSON, HttpContentType.XML)]
        [Produces(HttpContentType.JSON, HttpContentType.XML)]
        [ProducesResponseType(typeof(GetUserResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public virtual async Task<IActionResult> SignUpAsync([FromBody][Required]SignUpRequest request, CancellationToken cancellationToken = default)
        {
            var user = await this.WebApi
                .SignUpAsync(new SignUpRequest<User>
                {
                    SignUp = new SignUp<User>
                    {
                        Password = request.Password,
                        ConfirmPassword = request.Password,
                        EmailAddress = request.EmailAddress.Email,
                        Username = request.EmailAddress.Email,
                        User = new User
                        {
                            Name = request.Name
                        }
                    }
                }, cancellationToken);

            if (user == null)
                return this.NotFound();

            var response = new GetUserResponse(user);

            return this.Created("signup", response);
        }
    }
}