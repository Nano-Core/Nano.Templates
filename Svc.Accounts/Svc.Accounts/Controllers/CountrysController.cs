using DynamicExpression.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Common.Consts;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Accounts.Models.Criterias;
using Svc.Accounts.Models.Data;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Svc.Accounts.Controllers;

/// <inheritdoc />
public class CountrysController(ILogger<CountrysController> logger, IRepository repository, IEventing eventing)
    : BaseEntityController<Country, CountryQueryCriteria>(logger, repository, eventing)
{
    /// <summary>
    /// Anonymously get all countries.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The countries.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("all")]
    [AllowAnonymous]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<Country>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> AllAsync(CancellationToken cancellationToken = default)
    {
        var ordering = new Ordering
        {
            By = nameof(Country.Name)
        };

        var countries = await this.Repository
            .GetManyAsync<Country>(x => true, ordering, cancellationToken);

        return this.Ok(countries);
    }
}