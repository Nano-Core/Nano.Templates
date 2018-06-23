﻿using Microsoft.AspNetCore.Hosting;
using Nano.Data.Extensions;
using Nano.Data.Providers.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Extensions;
using Nano.Logging.Providers.Serilog;
using Nano.Templates.Advanced.Data;
using Nano.Web;

namespace Nano.Templates.Advanced
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
                    x.AddDataContext<MySqlProvider, AdvancedDbContext>();
                    x.AddEventing<EasyNetQProvider>();
                })
                .Build()
                .Run();
        }
    }
}