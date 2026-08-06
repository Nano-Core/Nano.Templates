using Lib.Images.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.Common.Consts;
using Svc.Places.Extensions;
using Svc.Places.Models.Criterias;
using Svc.Places.Models.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Nano.App.Api.Annotations;
using Nano.App.Api.Controllers;
using Nano.Common.Extensions;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Nano.Storage.Abstractions;

namespace Svc.Places.Controllers;

/// <inheritdoc />
public class PlacesController : BaseEntityController<Place, PlaceQueryCriteria>
{
    private readonly IPathProvider pathProvider;

    /// <inheritdoc />
    public PlacesController(ILogger<PlacesController> logger, IRepository repository, IEventing eventing, IPathProvider pathProvider)
        : base(logger, repository, eventing)
    {
        this.pathProvider = pathProvider ?? throw new ArgumentNullException(nameof(pathProvider));
    }

    /// <summary>
    /// Get Logo Image File.
    /// </summary>
    /// <param name="id">The place id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The logo image file.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("{id:guid}/logo")]
    [Produces(HttpContentType.JPEG, HttpContentType.PNG)]
    [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetLogoImageFileAsync([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
    {
        var place = await this.Repository
            .GetAsync<Place>(id, 0, cancellationToken);

        if (place == null)
        {
            return this.NotFound();
        }

        try
        {
            var filename = this.GetLogoPathAndFilename(place);
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
    /// Set Logo Image.
    /// </summary>
    /// <param name="id">The place id.</param>
    /// <param name="file">The file.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("{id:guid}/logo/set")]
    [Consumes(HttpContentType.FORM)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> SetLogoImageAsync([FromRoute][Required]Guid id, [Required][FileExtensionValidation(FileExtensions.JPG, FileExtensions.JPEG, FileExtensions.PNG)]IFormFile file, CancellationToken cancellationToken = default)
    {
        var place = await this.Repository
            .GetAsync<Place>(id, 0, cancellationToken);

        if (place == null)
        {
            return this.NotFound();
        }

        this.DeleteLogoFile(place);
        
        await using var image = file
            .OpenReadStream();

        await using var resizedImage = image
            .ResizeImage(250, 250);

        var path = this.GetLogoPathAndFilename(place);

        await resizedImage
            .SaveFileAsync(path, cancellationToken);

        return this.Ok();
    }

    /// <summary>
    /// Remove Logo Image.
    /// </summary>
    /// <param name="id">The place id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpDelete]
    [Route("{id:guid}/logo/remove")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> RemoveLogoImageAsync([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
    {
        var place = await this.Repository
            .GetAsync<Place>(id, 0, cancellationToken);

        if (place == null)
        {
            return this.NotFound();
        }

        this.DeleteLogoFile(place);

        return this.Ok();
    }


    private string GetLogoPathAndFilename(Place place)
    {
        ArgumentNullException.ThrowIfNull(place);

        var path = this.pathProvider
            .GetPlacesPath(place.Id);

        var filename = place.GetLogoFilename();

        return Path.Combine(path, filename);
    }
    private void DeleteLogoFile(Place place)
    {
        ArgumentNullException.ThrowIfNull(place);

        var pathAndFilename = this.GetLogoPathAndFilename(place);

        if (System.IO.File.Exists(pathAndFilename))
        {
            System.IO.File.Delete(pathAndFilename);
        }
    }
}