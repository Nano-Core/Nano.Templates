using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Services.Interfaces;
using Nano.Templates.Simple.Models;
using Nano.Templates.Simple.Models.Criterias;
using Nano.Web.Controllers;
using Newtonsoft.Json;

namespace Nano.Templates.Simple.Controllers
{
    /// <inheritdoc />
    public class SimpleController : DefaultController<SimpleEntity, SimpleQueryCriteria>
    {
        /// <inheritdoc />
        public SimpleController(ILogger logger, IService service, IEventing eventing)
            : base(logger, service, eventing)
        {
            
        }

        /// <inheritdoc />
        public override async Task<IActionResult> Create([FromBody][FromForm][Required]SimpleEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await this.Service
                .AddAsync(entity, cancellationToken);

            var a = JsonConvert.SerializeObject(result);

            this.Logger.LogError(a);


            return this.Created("Create", result);


            //var task = base.Create(entity, cancellationToken);

            //await task.ContinueWith(x =>
            //{
            //    if (x.IsFaulted || x.IsCanceled)
            //        return;

            //    var @event = new SimpleCreatedEvent
            //    {
            //        Id = entity.Id,
            //        PropertyOne = entity.PropertyOne,
            //        PropertyTwo = entity.PropertyTwo
            //    };

            //    this.Eventing.Publish(@event);
            //}, cancellationToken);

            //return await task;
        }
    }
}