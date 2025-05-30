﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Requests;
using Nano.Models;
using Nano.Models.Const;
using Nano.Security.Extensions;
using Nano.Template.Api.Controllers.Requests.Samples;
using Nano.Template.Api.Controllers.Responses.Samples;
using Nano.Template.Service.Models.Api;
using Nano.Template.Service.Models.Criterias;
using Nano.Template.Service.Models.Data;
using Nano.Web.Attributes;
using Nano.Web.Controllers;

namespace Nano.Template.Api.Controllers;

/// <inheritdoc />
public class SamplesController : BaseController
{
    /// <summary>
    /// Web Api.
    /// </summary>
    protected virtual ServiceApi ServiceApi { get; }

    /// <inheritdoc />
    public SamplesController(ILogger logger, ServiceApi serviceApi)
        : base(logger)
    {
        this.ServiceApi = serviceApi ?? throw new ArgumentNullException(nameof(serviceApi));
    }

    /// <summary>
    /// Gets a sample.
    /// </summary>
    /// <param name="id">The sample id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The get sample response.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpGet]
    [Route("{id:guid}")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(GetSampleResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetSampleAsync([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
    {
        var sample = await this.ServiceApi
            .GetAsync<Sample>(id, cancellationToken);

        if (sample == null)
            return this.NotFound();

        var response = new GetSampleResponse(sample);

        return this.Ok(response);
    }

    /// <summary>
    /// Gets all samples.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of get sample responses.</returns>
    /// <response code="200">Success.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("query")]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(IEnumerable<GetSampleResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> GetSamplesAsync([FromBody][Required]QuerySampleRequest request, CancellationToken cancellationToken = default)
    {
        var samples = await this.ServiceApi
            .QueryAsync<Sample, SampleQueryCriteria>(new QueryRequest<SampleQueryCriteria>
            {
                Query =
                {
                    Criteria =
                    {
                        Name = request.Name
                    },
                    Order = request.Order,
                    Paging = request.Paging
                }
            }, cancellationToken);

        if (samples == null)
            return this.NotFound();

        var response = samples
            .Select(x => new GetSampleResponse(x));

        return this.Ok(response);
    }

    /// <summary>
    /// Creates a Sample.
    /// </summary>
    /// <param name="request">The create sample request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The get sample response.</returns>
    /// <response code="201">Created.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("create")]
    [Consumes(HttpContentType.JSON)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType(typeof(GetSampleResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> CreateSampleAsync([FromBody][Required]CreateSampleRequest request, CancellationToken cancellationToken = default)
    {
        var provider = await this.ServiceApi
            .CreateAsync<Sample>(new CreateRequest
            {
                Entity = new Sample
                {
                    Name = request.Name
                }
            }, cancellationToken);

        var response = new GetSampleResponse(provider);

        return this.Created("create", response);
    }

    /// <summary>
    /// Upload file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>The uploaded file.</returns>
    /// <response code="200">OK.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("upload")]
    [RequestSizeLimit(1024 * 1024 * 5)]
    [Consumes(HttpContentType.FORM)]
    [Produces(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UploadAsync([Required]IFormFile file, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        return this.Ok();
    }
}