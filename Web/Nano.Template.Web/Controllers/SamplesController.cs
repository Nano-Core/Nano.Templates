using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Models;
using Nano.Repository.Interfaces;
using Nano.Template.Web.Models;
using Nano.Template.Web.Models.Criterias;
using Nano.Template.Web.Models.Events;
using Nano.Web.Controllers;

namespace Nano.Template.Web.Controllers;

/// <inheritdoc />
public class SamplesController : DefaultController<Sample, SampleQueryCriteria>
{
    /// <inheritdoc />
    public SamplesController(ILogger logger, IRepository repository, IEventing eventing)
        : base(logger, repository, eventing)
    {

    }

    /// <summary>
    /// Custom.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Task (Void).</returns>
    /// <response code="201">Created.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occured.</response>
    [HttpPost]
    [Route("custom")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> CustomAsync(CancellationToken cancellationToken = default)
    {
        await this.Eventing
            .PublishAsync(new SampleEvent(), cancellationToken: cancellationToken);

        return this.Accepted();
    }
}