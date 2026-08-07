using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api.Admin.Requests;
using Api.Admin.Responses;
using DynamicExpression.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.ApiClient.Requests;
using Nano.Common.Consts;
using Svc.Accounts.Models.Api;
using Svc.Accounts.Models.Data;

namespace Api.Admin.Controllers;

/// <inheritdoc />
public class TenantsController(ILogger<TenantsController> logger, AccountsApi accountsApi)
    : BaseAdminController(logger)
{
    /// <summary>
    /// Get Tenant.
    /// </summary>
    /// <param name="id">The tenant id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The tenant.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("{id:guid}")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(TenantResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetTenant([FromRoute][Required] Guid id, CancellationToken cancellationToken = default)
    {
        var tenant = await accountsApi.Entity
            .GetAsync<Tenant>(id, cancellationToken);

        if (tenant == null)
        {
            return this.NotFound();
        }

        var response = new TenantResponse(tenant);

        return this.Ok(response);
    }

    /// <summary>
    /// Get Tenants.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The tenants.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<TenantResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ListTenants([FromQuery]Query? query, CancellationToken cancellationToken = default)
    {
        var tenants = await accountsApi.Entity
            .IndexAsync<Tenant>(new IndexRequest
            {
                Query = query ?? new Query()
            }, cancellationToken);

        var responses = tenants
            .Select(x => new TenantResponse(x));

        return this.Ok(responses);
    }

    /// <summary>
    /// Create Tenant.
    /// </summary>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The created tenant.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("create")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(TenantResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> CreateTenant([FromBody][Required]CreateTenantRequest request, CancellationToken cancellationToken = default)
    {
        var tenant = await accountsApi.Entity
            .CreateAsync<Tenant>(new CreateRequest
            {
                Entity = new Tenant
                {
                    Name = request.Name
                }
            }, cancellationToken);

        var response = new TenantResponse(tenant);

        return this.Ok(response);
    }

    /// <summary>
    /// Update Tenant.
    /// </summary>
    /// <param name="tenantId">The tenant id.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The updated tenant.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPut]
    [Route("{tenantId:guid}/update")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(TenantResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> UpdateTenant([FromRoute][Required]Guid tenantId, [FromBody]UpdateTenantRequest request, CancellationToken cancellationToken = default)
    {
        var tenant = await accountsApi.Entity
            .GetAsync<Tenant>(tenantId, cancellationToken);

        if (tenant == null)
        {
            return this.NotFound();
        }

        tenant.Name = request.Name;

        tenant = await accountsApi.Entity
            .EditAsync<Tenant>(new EditRequest
            {
                Entity = tenant
            }, cancellationToken);

        if (tenant == null)
        {
            return this.NotFound();
        }

        var response = new TenantResponse(tenant);

        return this.Ok(response);
    }

    /// <summary>
    /// Delete Tenant.
    /// </summary>
    /// <param name="tenantId">The tenant id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Nothing.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpDelete]
    [Route("{tenantId:guid}/delete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> DeleteTenant([FromRoute][Required]Guid tenantId, CancellationToken cancellationToken = default)
    {
        await accountsApi.Entity
            .DeleteAsync<Tenant>(tenantId, cancellationToken);

        return this.Ok();
    }
}