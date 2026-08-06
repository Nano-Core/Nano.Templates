using Lib.Images.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Annotations;
using Nano.App.Api.Controllers;
using Nano.App.Exceptions;
using Nano.Common.Consts;
using Nano.Common.Extensions;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Nano.Storage.Abstractions;
using Svc.Places.Extensions;
using Svc.Places.Models.Criterias;
using Svc.Places.Models.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Svc.Places.Controllers;

/// <inheritdoc />
public class PlacePicturesController : BaseEntityEditableController<PlacePicture, PlacePictureQueryCriteria>
{
    private const int MAX_PICTURES = 25;

    private readonly IPathProvider pathProvider;

    /// <inheritdoc />
    public PlacePicturesController(ILogger<PlacePicturesController> logger, IRepository repository, IEventing eventing, IPathProvider pathProvider)
        : base(logger, repository, eventing)
    {
        this.pathProvider = pathProvider ?? throw new ArgumentNullException(nameof(pathProvider));
    }

    /// <summary>
    /// Get File.
    /// </summary>
    /// <param name="id">The picture id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The picture file.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("{id:guid}/file")]
    [Produces(HttpContentType.JPEG, HttpContentType.PNG)]
    [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetPictureFileAsync([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
    {
        var picture = await this.Repository
            .GetAsync<PlacePicture>(id, cancellationToken);

        if (picture == null)
        {
            return this.NotFound();
        }

        try
        {
            var filename = this.GetFilePathAndFilename(picture);
            var bytes = await System.IO.File.ReadAllBytesAsync(filename, cancellationToken);
            var extension = Path.GetExtension(filename);

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
    /// Add.
    /// </summary>
    /// <param name="placeId">The place id.</param>
    /// <param name="file">The file.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The picture.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("add/{placeId:guid}")]
    [Consumes(HttpContentType.FORM)]
    [ProducesResponseType(typeof(PlacePicture), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> AddPictureAsync([FromRoute][Required]Guid placeId, [Required][FileExtensionValidation(FileExtensions.JPG, FileExtensions.JPEG, FileExtensions.PNG)]IFormFile file, CancellationToken cancellationToken = default)
    {
        var place = await this.Repository
            .GetFirstAsync<Place>(x => x.Id == placeId, 1, cancellationToken);

        if (place == null)
        {
            return this.NotFound();
        }

        var pictures = (await this.Repository
                .GetManyAsync<PlacePicture>(x => x.PlaceId == placeId, 0, cancellationToken))
            .ToArray();

        if (pictures.Length > PlacePicturesController.MAX_PICTURES)
        {
            throw new BadRequestException("The maximum number of pictures has been exceeded.");
        }

        var orderIndex = 0;

        if (pictures.Length > 0)
        {
            orderIndex = pictures
                .Select(x => x.OrderIndex)
                .MaxBy(x => x);
        }

        var picture = await this.Repository
            .AddAsync(new PlacePicture
            {
                PlaceId = place.Id,
                OrderIndex = ++orderIndex
            }, cancellationToken);

        await this.SavePictureAsync(file, picture, cancellationToken);

        return this.Ok(picture);
    }

    /// <summary>
    /// Remove.
    /// </summary>
    /// <param name="id">The picture id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpDelete]
    [Route("{id:guid}/remove")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> RemovePictureAsync([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
    {
        var picture = await this.Repository
            .GetAsync<PlacePicture>(id, cancellationToken);

        if (picture == null)
        {
            return this.NotFound();
        }

        this.DeletePictureAsync(picture);

        await this.Repository
            .DeleteAsync(picture, cancellationToken);

        return this.Ok();
    }


    private string GetFilePathAndFilename(PlacePicture placePicture)
    {
        if (placePicture == null)
            throw new ArgumentNullException(nameof(placePicture));

        var path = this.pathProvider
            .GetPlacesPath(placePicture.PlaceId);

        var filename = $"{placePicture.Id}_{FileExtensions.PNG}";

        return Path.Combine(path, filename);
    }
    private async Task SavePictureAsync(IFormFile file, PlacePicture placePicture, CancellationToken cancellationToken = default)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        if (placePicture == null)
            throw new ArgumentNullException(nameof(placePicture));

        await using var image = file
            .OpenReadStream();

        await using var resizedImage = image
            .ResizeImage(800, 600);

        var path = this.GetFilePathAndFilename(placePicture);

        await resizedImage
            .SaveFileAsync(path, cancellationToken);
    }
    private void DeletePictureAsync(PlacePicture placePicture)
    {
        if (placePicture == null)
            throw new ArgumentNullException(nameof(placePicture));

        var path = this.GetFilePathAndFilename(placePicture);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}