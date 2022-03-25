using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Template.Web.Models.Events;
using Nano.Template.Web.Services.Interfaces;

namespace Nano.Template.Web.Eventing
{
    /// <summary>
    /// Sample Event Handler.
    /// </summary>
    public class SampleEventHandler : IEventingHandler<SampleEvent>
    {
        /// <summary>
        /// Logger.
        /// </summary>
        protected virtual ILogger Logger { get; }

        /// <summary>
        /// Sample Service.
        /// </summary>
        protected virtual ISampleService SampleService { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        /// <param name="sampleService">The <see cref="ISampleService"/>.</param>
        public SampleEventHandler(ILogger logger, ISampleService sampleService)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.SampleService = sampleService ?? throw new ArgumentNullException(nameof(sampleService));
        }

        /// <inheritdoc />
        public virtual async Task CallbackAsync(SampleEvent @event, bool isRetrying)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            try
            {
                this.Logger.LogInformation("Callback");

                await Task.FromResult(0);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, ex.Message);
            }
        }
    }
}