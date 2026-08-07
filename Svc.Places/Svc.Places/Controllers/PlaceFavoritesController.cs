using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Common.Consts;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Places.Models.Criterias;
using Svc.Places.Models.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Svc.Places.Controllers;

/// <inheritdoc />
public class PlaceFavoritesController(ILogger<PlaceFavoritesController> logger, IRepository repository, IEventing eventing)
    : BaseEntityReadOnlyController<PlaceFavorite, PlaceFavoriteQueryCriteria>(logger, repository, eventing)
{
    /// <summary>
    /// Add.
    /// </summary>
    /// <param name="placeId">The place id.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The place favorite.</returns>
    /// <response code="201">Created.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("{placeId:guid}/add/{userId:guid}")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(PlaceFavorite), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> AddAsync([FromRoute][Required]Guid placeId, [FromRoute][Required]Guid userId, CancellationToken cancellationToken = default)
    {
        var placeFavorite = await this.Repository
            .GetFirstAsync<PlaceFavorite>(x => x.PlaceId == placeId && x.UserId == userId, 1, cancellationToken);

        if (placeFavorite == null)
        {
            placeFavorite = await this.Repository
                .AddAndGetAsync<PlaceFavorite, Guid>(new PlaceFavorite
                {
                    PlaceId = placeId,
                    UserId = userId
                }, cancellationToken);

            if (placeFavorite != null)
            {
                placeFavorite.Place!.FavoriteCount++;

                await this.Repository
                    .UpdateAsync(placeFavorite.Place, cancellationToken);
            }
        }

        return this.Created("add/{placeId}/{userId}", placeFavorite);
    }

    /// <summary>
    /// Remove.
    /// </summary>
    /// <param name="placeId">The place id.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="201">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpDelete]
    [Route("{placeId:guid}/remove/{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> RemoveAsync([FromRoute][Required]Guid placeId, [FromRoute][Required]Guid userId, CancellationToken cancellationToken = default)
    {
        var placeFavorite = await this.Repository
            .GetFirstAsync<PlaceFavorite>(x => x.PlaceId == placeId && x.UserId == userId, 1, cancellationToken);

        if (placeFavorite == null)
        {
            return this.NotFound();
        }

        placeFavorite.Place!.FavoriteCount--;

        await this.Repository
            .UpdateAsync(placeFavorite.Place, cancellationToken);

        await this.Repository
            .DeleteAsync(placeFavorite, cancellationToken);

        return this.Ok();
    }
}