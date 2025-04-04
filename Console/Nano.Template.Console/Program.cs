﻿using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nano.Console;
using Nano.Data.Extensions;
using Nano.Data.Providers.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Extensions;
using Nano.Logging.Providers.Serilog;
using Nano.Template.Console.Data;
using Nano.Template.Console.Services;
using Nano.Template.Console.Services.Interfaces;

namespace Nano.Template.Console;

/// <summary>
/// Program.
/// </summary>
public class Program
{
    /// <summary>
    /// Main.
    /// </summary>
    /// <param name="args"></param>
    public static Task Main(string[] args)
    {
        return ConsoleApplication
            .ConfigureApp(args)
            .ConfigureServices(x =>
            {
                x.AddLogging<SerilogProvider>();
                x.AddDataContext<MySqlProvider, ConsoleDbContext>();
                x.AddEventing<EasyNetQProvider>();

                x.AddSingleton<ISampleService, SampleService>();
            })
            .Build()
            .RunAsync();
    }
}