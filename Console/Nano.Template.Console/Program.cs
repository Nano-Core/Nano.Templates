using Microsoft.Extensions.Hosting;
using Nano.Console;
using Nano.Data.Extensions;
using Nano.Data.Providers.SqlServer;
using Nano.Eventing.Extensions;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Extensions;
using Nano.Logging.Providers.Serilog;
using Nano.Template.Console.Data;

namespace Nano.Template.Console
{
    /// <summary>
    /// Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            ConsoleApplication
                .ConfigureApp(args)
                .ConfigureServices(x =>
                {
                    x.AddLogging<SerilogProvider>();
                    x.AddDataContext<SqlServerProvider, ConsoleDbContext>();
                    x.AddEventing<EasyNetQProvider>();
                })
                .Build()
                .RunAsync()
                .ConfigureAwait(false);
        }
    }
}