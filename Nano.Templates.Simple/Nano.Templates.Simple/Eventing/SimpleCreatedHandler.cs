using System;
using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Services.Interfaces;
using Nano.Templates.Simple.Models.Events;

namespace Nano.Templates.Simple.Eventing
{
    /// <summary>
    /// Simple Created Handler.
    /// </summary>
    public class SimpleCreatedHandler : IEventingHandler<SimpleCreatedEvent>
    {
        /// <summary>
        /// Logger.
        /// </summary>
        protected virtual ILogger Logger { get; }

        /// <summary>
        /// Service.
        /// </summary>
        protected virtual IService Service { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        /// <param name="service">The <see cref="IService"/>.</param>
        public SimpleCreatedHandler(ILogger logger, IService service)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.Logger = logger;
            this.Service = service;
        }

        /// <inheritdoc />
        public void Callback(SimpleCreatedEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            this.Logger.LogInformation("Callback Invoked.");
        }
    }
}