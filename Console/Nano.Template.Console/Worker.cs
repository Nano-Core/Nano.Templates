using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nano.Console.Workers;
using Nano.Eventing.Interfaces;
using Nano.Repository.Interfaces;

namespace Nano.Template.Console
{
    /// <inheritdoc />
    public class Worker : DefaultWorker
    {
        /// <inheritdoc />
        public Worker(ILogger logger, IRepository repository, IEventing eventing, IHostApplicationLifetime applicationLifetime)
            : base(logger, repository, eventing, applicationLifetime)
        {

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