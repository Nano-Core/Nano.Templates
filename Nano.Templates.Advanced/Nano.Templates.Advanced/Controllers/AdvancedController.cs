using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Services.Interfaces;
using Nano.Templates.Advanced.Models;
using Nano.Templates.Advanced.Models.Criterias;
using Nano.Templates.Advanced.Models.Events;
using Nano.Web.Controllers;

namespace Nano.Templates.Advanced.Controllers
{
    /// <inheritdoc />
    public class AdvancedController : DefaultController<AdvancedEntity, AdvancedQueryCriteria>
    {
        /// <inheritdoc />
        public AdvancedController(ILogger logger, IService service, IEventing eventing)
            : base(logger, service, eventing)
        {
            
        }

        /// <inheritdoc />
        public override async Task<IActionResult> Create([FromBody][FromForm][Required]AdvancedEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var task = base.Create(entity, cancellationToken);

            await task.ContinueWith(x =>
            {
                if (x.IsFaulted || x.IsCanceled)
                    return;

                var @event = new AdvancedCreatedEvent
                {
                    Id = entity.Id
                };

                this.Eventing.Publish(@event);
            }, cancellationToken);

            return await task;
        }
    }
}