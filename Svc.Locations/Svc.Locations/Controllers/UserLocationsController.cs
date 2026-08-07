using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Common.Consts;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Locations.Models.Criterias;
using Svc.Locations.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Svc.Locations.Events;

namespace Svc.Locations.Controllers;

/// <inheritdoc />
public class UserLocationsController(ILogger<UserLocationsController> logger, IRepository repository, IEventing eventing)
    : BaseEntityReadOnlyController<UserLocation, UserLocationQueryCriteria>(logger, repository, eventing)
{
    /// <summary>
    /// Create.
    /// </summary>
    /// <param name="userLocation">The user location.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The user location.</returns>
    /// <response code="201">Created.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("create")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(UserLocation), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> CreateAsync([FromBody][Required] UserLocation userLocation, CancellationToken cancellationToken = default)
    {
        var userLocationCreated = await this.Repository
            .AddAsync(userLocation, cancellationToken);

        await this.Eventing!
            .PublishAsync(new UserLocationChangedEvent
            {
                UserId = userLocationCreated.UserId,
                CreatedAt = userLocationCreated.CreatedAt,
                Coordinate = userLocationCreated.Coordinate
            }, cancellationToken: cancellationToken);

        return this.Created("create", userLocationCreated);
    }
}