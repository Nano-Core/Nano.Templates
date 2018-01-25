using Microsoft.AspNetCore.Hosting;
using Nano.App;
using Nano.App.Extensions;
using Nano.Data.Providers.MySql;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Providers.Serilog;
using Nano.Templates.Simple.Data;

namespace Nano.Templates.Simple
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
                .ConfigureApp<SimpleApplication>()
                .ConfigureServices(x =>
                {
                    x.AddLogging<SerilogProvider>();
                    x.AddDataContext<MySqlProvider, SimpleDbContext>();
                    x.AddEventing<EasyNetQProvider>();
                })
                .Build()
                .Run();
        }
    }
}