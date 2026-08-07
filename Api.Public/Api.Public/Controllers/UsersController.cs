using Api.Public.Responses.Users;
using DynamicExpression.Entities;
using DynamicExpression.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.ApiClient.Requests;
using Nano.Common.Consts;
using Nano.Data.Abstractions.Identity.Extensions;
using NetTopologySuite.Geometries;
using Svc.Locations.Models.Api;
using Svc.Locations.Models.Criterias;
using Svc.Locations.Models.Data;
using Svc.Places.Models.Api;
using Svc.Places.Models.Criterias;
using Svc.Places.Models.Criterias.Types;
using Svc.Places.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Public.Controllers;

/// <inheritdoc />
public class UsersController(ILogger<UsersController> logger, LocationsApi locationsApi, PlacesApi placesApi)
    : BasePublicController(logger)
{
    /// <summary>
    /// Report Location.
    /// </summary>
    /// <param name="coordinate">The coordinate.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="200">Accepted.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("locations/report")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ReportLocationAsync([FromBody][Required]LatLng coordinate, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        await locationsApi.Entity
            .CreateAsync<UserLocation>(new CreateRequest
            {
                Entity = new UserLocation
                {
                    UserId = jwtUserId,
                    Coordinate = new Point(coordinate.Longitude, coordinate.Latitude)
                }
            }, cancellationToken);

        return this.Accepted();
    }

    /// <summary>
    /// Get Recent Locations.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The place visit user context responses.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpGet]
    [Route("locations/recent")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<UserLocationResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetPlacesVisitedAsync(CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        var userLocations = await locationsApi.Entity
            .QueryAsync<UserLocation, UserLocationQueryCriteria>(new QueryRequest<UserLocationQueryCriteria>
            {
                Query =
                {
                    Criteria =
                    {
                        UserId = jwtUserId
                    },
                    Paging =
                    {
                        Count = 10
                    },
                    Order =
                    {
                        By = nameof(UserLocation.CreatedAt),
                        Direction = OrderingDirection.Desc 
                    }
                }
            }, cancellationToken);

        var responses = userLocations
            .Select(x => new UserLocationResponse(x));

        return this.Ok(responses);
    }

    /// <summary>
    /// Get Visited Places.
    /// </summary>
    /// <param name="paging">The paging.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The place visit responses.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpGet]
    [Route("visited")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<PlaceVisitResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetPlacesVisitedAsync([FromQuery] Pagination? paging, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        var placeVisits = await placesApi.Entity
            .QueryAsync<PlaceVisit, PlaceVisitQueryCriteria>(new QueryRequest<PlaceVisitQueryCriteria>
            {
                Query =
                {
                    Criteria =
                    {
                        UserId = jwtUserId
                    },
                    Paging = paging ?? new Pagination()
                }
            }, cancellationToken);

        var responses = placeVisits
            .Select(x => new PlaceVisitResponse(x));

        return this.Ok(responses);
    }

    /// <summary>
    /// Get Place Visits.
    /// </summary>
    /// <param name="placeId">The place id-</param>
    /// <param name="paging">The paging.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The place visit responses.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpGet]
    [Route("{placeId:guid}/visits")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<PlaceVisitResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetPlaceVisitsAsync([FromRoute][Required] Guid placeId, [FromQuery] Pagination? paging, CancellationToken cancellationToken = default)
    {
        var jwtUserId = this.HttpContext
            .GetJwtUserId<Guid>();

        var placeVisits = await placesApi.Entity
            .QueryAsync<PlaceVisit, PlaceVisitQueryCriteria>(new QueryRequest<PlaceVisitQueryCriteria>
            {
                Query =
                {
                    Criteria =
                    {
                        PlaceId = placeId,
                        UserId = jwtUserId
                    },
                    Paging = paging ?? new Pagination()
                }
            }, cancellationToken);

        var responses = placeVisits
            .Select(x => new PlaceVisitResponse(x));

        return this.Ok(responses);
    }
}