﻿using Microsoft.AspNetCore.Hosting;
using Nano.Data.Extensions;
//using Nano.Data.Providers.MySql;
using Nano.Data.Providers.SqlServer;
using Nano.Eventing.Extensions;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Extensions;
using Nano.Logging.Providers.Serilog;
using Nano.Template.Web.Data;
using Nano.Web;

namespace Nano.Template.Web
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
                    x.AddDataContext<SqlServerProvider /*MySqlProvider*/, WebDbContext>();
                    x.AddEventing<EasyNetQProvider>();
                })
                .Build()
                .Run();
        }
    }
}