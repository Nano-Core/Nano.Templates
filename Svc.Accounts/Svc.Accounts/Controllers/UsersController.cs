using DynamicExpression.Entities;
using Lib.Images.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Annotations;
using Nano.App.Api.Controllers;
using Nano.Common.Consts;
using Nano.Common.Extensions;
using Nano.Data.Abstractions;
using Nano.Data.Abstractions.Identity;
using Nano.Data.Abstractions.Models.Abstractions;
using Nano.Eventing.Abstractions;
using Nano.Storage.Abstractions;
using Svc.Accounts.Consts;
using Svc.Accounts.Models.Api.Requests.Enums;
using Svc.Accounts.Models.Criterias;
using Svc.Accounts.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Svc.Accounts.Controllers;

/// <inheritdoc />
public class UsersController(ILogger<UsersController> logger, IRepository repository, IEventing eventing, IIdentityRepository identityRepository)
    : BaseEntityUserController<User, UserQueryCriteria>(logger, repository, eventing, identityRepository)
{
    private const string USER_PICTURE_PREFIX = "user-picture";

    private readonly IPathProvider pathProvider = null!;

    /// <inheritdoc />
    public UsersController(ILogger<UsersController> logger, IRepository repository, IEventing eventing, IIdentityRepository identityRepository, IPathProvider pathProvider)
        : this(logger, repository, eventing, identityRepository) 
    {
        this.pathProvider = pathProvider ?? throw new ArgumentNullException(nameof(pathProvider));
    }

    /// <summary>
    /// Anonymously get a yser by email.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The user.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("details/email")]
    [AllowAnonymous]
    [ResponseCache(NoStore = true)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetUserByEmailAsync([FromQuery][Required][EmailAddress] string emailAddress, CancellationToken cancellationToken = default)
    {
        var user = await this.Repository
            .GetFirstAsync<User>(x => x.IdentityUser.Email == emailAddress, new Ordering(), cancellationToken);

        if (user == null)
        {
            return this.NotFound();
        }

        return this.Ok(user);
    }

    /// <summary>
    /// Get new sign-ups.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The users.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("new-signups")]
    [AllowAnonymous]
    [ResponseCache(NoStore = true)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetNewSignUpsAsync(CancellationToken cancellationToken = default)
    {
        var user = await this.Repository
            .GetManyAsync<User>(x => x.CreatedAt > DateTimeOffset.UtcNow.AddHours(-2), cancellationToken);

        return this.Ok(user);
    }

    /// <summary>
    /// Get user Picture.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="type">THe image type.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The user picture.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("{id:guid}/user-picture/{type}")]
    [Produces(HttpContentType.JPEG, HttpContentType.PNG)]
    [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetUserPictureAsync([FromRoute][Required] Guid id, [FromRoute][Required] ImageType type, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await this.Repository
                .GetAsync<User>(id, cancellationToken);

            if (user?.UserPictureExtension == null)
            {
                return this.NotFound();
            }

            var filename = this.GetUserPictureFilename(user, type);
            var path = Path.Combine(this.pathProvider.Root, filename);
            var bytes = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            var extension = Path.GetExtension(path);

            var httpContentType = extension
                .GetHttpContentType();

            return this.File(bytes, httpContentType, filename);
        }
        catch (Exception ex)
        {
            this.Logger
                .LogError(ex, ex.Message);

            return this.NotFound();
        }
    }

    /// <summary>
    /// Add User Picture.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="file">The file.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("{id:guid}/user-picture/set")]
    [RequestSizeLimit(1024 * 1024 * 1024)]
    [Consumes(HttpContentType.FORM)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> AddUserPictureAsync([FromRoute][Required] Guid id, [Required][FileExtensionValidation(FileExtensions.JPG, FileExtensions.JPEG, FileExtensions.PNG)] IFormFile file, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(file.FileName);

        var user = await this.Repository
            .GetAsync<User>(id, cancellationToken);

        if (user == null)
        {
            return this.NotFound();
        }

        user.UserPictureExtension = extension;

        user = await this.Repository
            .UpdateAsync(user, cancellationToken);

        await this.SaveUserPictureAsync(file, user, ImageType.Thunbnail, cancellationToken);
        await this.SaveUserPictureAsync(file, user, ImageType.Full, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Remove User Picture.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpDelete]
    [Route("{id:guid}/user-picture/remove")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> RemoveUserPictureAsync([FromRoute][Required] Guid id, CancellationToken cancellationToken = default)
    {
        var user = await this.Repository
            .GetAsync<User>(id, cancellationToken);

        if (user?.UserPictureExtension == null)
        {
            return this.NotFound();
        }

        this.DeleteUserPictureAsync(user, ImageType.Thunbnail);
        this.DeleteUserPictureAsync(user, ImageType.Full);

        user.UserPictureExtension = null;

        await this.Repository
            .UpdateAsync(user, cancellationToken);

        return this.Ok();
    }


    private string GetUserPictureFilename(User user, ImageType type)
    {
        ArgumentNullException.ThrowIfNull(user);

        return $"{USER_PICTURE_PREFIX}-{type.ToString().ToLower()}-{user.Id}{user.UserPictureExtension}";
    }
    private async Task SaveUserPictureAsync(IFormFile file, User user, ImageType type, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentNullException.ThrowIfNull(user);

        await using var image = file
            .OpenReadStream();

        var size = type == ImageType.Thunbnail
            ? ImageSizes.THUMBNAIL
            : ImageSizes.FULL;

        await using var resizeImage = image
            .ResizeImage(size, size);

        var filename = this.GetUserPictureFilename(user, type);

        var path = Path.Combine(this.pathProvider.Root, filename);

        await resizeImage
            .SaveFileAsync(path, cancellationToken);
    }
    private void DeleteUserPictureAsync(User user, ImageType type)
    {
        ArgumentNullException.ThrowIfNull(user);

        var filename = this.GetUserPictureFilename(user, type);
        var path = Path.Combine(this.pathProvider.Root, filename);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}