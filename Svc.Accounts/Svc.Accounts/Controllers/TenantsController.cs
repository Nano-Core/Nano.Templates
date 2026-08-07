using System.Collections.Generic;
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
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Svc.Accounts.Controllers;

/// <inheritdoc />
public class TenantsController(ILogger<TenantsController> logger, IRepository repository, IEventing eventing)
    : BaseEntityController<Tenant, TenantQueryCriteria>(logger, repository, eventing)
{
    /// <summary>
    /// Anonymously get all tenants.
    /// </summary>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The tenants.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("all")]
    [AllowAnonymous]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<Tenant>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> AllAsync(CancellationToken cancellationToken = default)
    {
        var query = new Query
        {
            Order = new Ordering
            {
                By = nameof(Tenant.Name)
            }
        };

        var tenants = await this.Repository
            .GetManyAsync<Tenant>(query, cancellationToken);

        return this.Ok(tenants);
    }
}