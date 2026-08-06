using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Common.Consts;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Emailing.Eventing;
using Svc.Emailing.Models.Criterias;
using Svc.Emailing.Models.Data;

namespace Svc.Emailing.Controllers;

/// <inheritdoc />
public class EmailsController(ILogger<EmailsController> logger, IRepository repository, IEventing eventing)
    : BaseEntityReadOnlyController<Email, EmailQueryCriteria>(logger, repository, eventing)
{
    /// <summary>
    /// Send.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The token used when request is cancelled.</param>
    /// <returns>Void.</returns>
    /// <response code="202">Accepted.</response>
    /// <response code="404">Not Found.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Error occurred.</response>
    [HttpPost]
    [Route("send")]
    [AllowAnonymous]
    [Consumes(HttpContentType.JSON)]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public virtual async Task<IActionResult> SendAsync([FromBody][Required] Email email, CancellationToken cancellationToken = default)
    {
        await this.Eventing!
            .PublishAsync(new EmailEvent
            {
                Email = email
            }, cancellationToken: cancellationToken);
        
        return this.Accepted();
    }
}