using Microsoft.AspNetCore.Hosting;
using Nano.Basic.Data;
using Nano.Data.Extensions;
using Nano.Data.Providers.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Extensions;
using Nano.Logging.Providers.Serilog;
using Nano.Web;

namespace Nano.Basic
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
            WebApplication
                .ConfigureApp()
                .ConfigureServices(x =>
                {
                    x.AddLogging<SerilogProvider>();
                    x.AddDataContext<MySqlProvider, BasicDbContext>();
                    x.AddEventing<EasyNetQProvider>();
                })
                .Build()
                .Run();
        }
    }
}