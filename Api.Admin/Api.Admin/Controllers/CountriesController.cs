using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api.Admin.Requests;
using Api.Admin.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.ApiClient.Requests;
using Nano.Common.Consts;
using Svc.Accounts.Models.Api;
using Svc.Accounts.Models.Data;

namespace Api.Admin.Controllers;

/// <inheritdoc />
public class CountriesController(ILogger<CountriesController> logger, AccountsApi accountsApi)
    : BaseAdminController(logger)
{
    /// <summary>
    /// Get Country.
    /// </summary>
    /// <param name="id">The country id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The country.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpGet]
    [Route("{id:guid}")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(CountryResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetCountry([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
    {
        var country = await accountsApi.Entity
            .GetAsync<Country>(id, cancellationToken);

        if (country == null)
        {
            return this.NotFound();
        }

        var response = new CountryResponse(country);

        return this.Ok(response);
    }

    /// <summary>
    /// List all Countries.
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
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<CountryResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> ListAllCountries(CancellationToken cancellationToken = default)
    {
        var countries = await accountsApi.Entity
            .IndexAsync<Country>(new IndexRequest(), cancellationToken);

        var responses = countries
            .Select(x => new CountryResponse(x));

        return this.Ok(responses);
    }

    /// <summary>
    /// Create country.
    /// </summary>
    /// <param name="request">The create request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The created country.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("create")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(CountryResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> Createcountry([FromBody][Required]CreateCountryRequest request, CancellationToken cancellationToken = default)
    {
        var country = await accountsApi.Entity
            .CreateAsync<Country>(new CreateRequest
            {
                Entity = new Country
                {
                    Name = request.Name,
                    Code = request.Code,
                    PhonePrefix = request.PhonePrefix
                }
            }, cancellationToken);

        var response = new CountryResponse(country);

        return this.Ok(response);
    }

    /// <summary>
    /// Update Country.
    /// </summary>
    /// <param name="id">The country id.</param>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The updated country.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPut]
    [Route("{id:guid}/update")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(CountryResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> UpdateCountry([FromRoute][Required]Guid id, [FromBody]UpdateCountryRequest request, CancellationToken cancellationToken = default)
    {
        var country = await accountsApi.Entity
            .GetAsync<Country>(id, cancellationToken);

        if (country == null)
        {
            return this.NotFound();
        }

        country.Name = request.Name;

        country = await accountsApi.Entity
            .EditAsync<Country>(new EditRequest
            {
                Entity = country
            }, cancellationToken);

        if (country == null)
        {
            return this.NotFound();
        }

        var response = new CountryResponse(country);

        return this.Ok(response);
    }

    /// <summary>
    /// Delete Country.
    /// </summary>
    /// <param name="id">The country id.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Nothing.</returns>
    /// <response code="200">OK.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpDelete]
    [Route("{id:guid}/delete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> DeleteCountry([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
    {
        await accountsApi.Entity
            .DeleteAsync<Country>(id, cancellationToken);

        return this.Ok();
    }
}