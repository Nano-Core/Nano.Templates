using Microsoft.AspNetCore.Hosting;
using Nano.App;
using Nano.App.Extensions;
using Nano.Data.Providers.MySql;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Providers.Serilog;
using Nano.Templates.Spatial.Data;

namespace Nano.Templates.Spatial
{
    /// <summary>
    /// Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        public static void Main()
        {
            BaseApplication
                .ConfigureApp<SpatialApplication>()
                .ConfigureServices(x =>
                {
                    x.AddLogging<SerilogProvider>();
                    x.AddDataContext<MySqlProvider, SpatialDbContext>();
                    x.AddEventing<EasyNetQProvider>();
                })
                .Build()
                .Run();
        }
    }
}