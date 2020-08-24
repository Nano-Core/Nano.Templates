using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.Template.Api.Models.Requests.Samples;
using Nano.Template.Api.Models.Responses.Samples;
using Nano.Template.Web.Models;
using Nano.Template.Web.Models.Api;
using Nano.Template.Web.Models.Criterias;
using Nano.Web.Api.Requests;
using Nano.Web.Const;
using Nano.Web.Controllers;
using Nano.Web.Models;

namespace Nano.Template.Api.Controllers
{
    /// <inheritdoc />
    public class SamplesController : BaseController
    {
        /// <summary>
        /// Web Api.
        /// </summary>
        protected virtual WebApi WebApi { get; }

        /// <inheritdoc />
        public SamplesController(ILogger logger, WebApi webApi)
            : base(logger)
        {
            this.WebApi = webApi ?? throw new ArgumentNullException(nameof(webApi));
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
        [Route("{id}")]
        [Produces(HttpContentType.JSON, HttpContentType.XML)]
        [ProducesResponseType(typeof(GetSampleResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public virtual async Task<IActionResult> GetSampleAsync([FromRoute][Required]Guid id, CancellationToken cancellationToken = default)
        {
            var sample = await this.WebApi
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
        [Produces(HttpContentType.JSON, HttpContentType.XML)]
        [ProducesResponseType(typeof(IEnumerable<GetSampleResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public virtual async Task<IActionResult> GetSamplesAsync([FromBody][Required]QuerySampleRequest request, CancellationToken cancellationToken = default)
        {
            var samples = await this.WebApi
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
        [Consumes(HttpContentType.JSON, HttpContentType.XML)]
        [Produces(HttpContentType.JSON, HttpContentType.XML)]
        [ProducesResponseType(typeof(GetSampleResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public virtual async Task<IActionResult> CreateSampleAsync([FromBody][Required]CreateSampleRequest request, CancellationToken cancellationToken = default)
        {
            var provider = await this.WebApi
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
    }
}