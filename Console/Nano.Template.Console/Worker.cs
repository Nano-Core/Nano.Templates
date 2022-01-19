using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nano.Console.Workers;
using Nano.Eventing.Interfaces;
using Nano.Repository.Interfaces;
using Nano.Template.Console.Services.Interfaces;

namespace Nano.Template.Console
{
    /// <inheritdoc />
    public class Worker : DefaultWorker
    {
        /// <summary>
        /// Sample Service.
        /// </summary>
        protected ISampleService SampleService { get; }

        /// <inheritdoc />
        public Worker(ILogger logger, IRepository repository, IEventing eventing, IHostApplicationLifetime applicationLifetime, ISampleService sampleService)
            : base(logger, repository, eventing, applicationLifetime)
        {
            this.SampleService = sampleService ?? throw new ArgumentNullException(nameof(sampleService));
        }

        /// <inheritdoc />
        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await base.StartAsync(cancellationToken);

            this.Logger.LogInformation("Cmd::Running...");

            Thread.Sleep(2000);

            this.Logger.LogInformation("Cmd::Completed...");

            await this.StopAsync(cancellationToken);
        }
    }
}