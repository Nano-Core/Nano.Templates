using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nano.Data.Extensions;
using Nano.Data.Providers.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.Providers.EasyNetQ;
using Nano.Logging.Extensions;
using Nano.Logging.Providers.Serilog;
using Nano.Storage.Extensions;
using Nano.Storage.Providers.Azure;
using Nano.Template.Service.Data;
using Nano.Template.Service.Services;
using Nano.Template.Service.Services.Interfaces;
using Nano.Web;

namespace Nano.Template.Service;

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
                x.AddDataContext<MySqlProvider, ServiceDbContext>();
                x.AddEventing<EasyNetQProvider>();
                x.AddStorage<AzureFileshareProvider>();

                x.AddSingleton<ISampleService, SampleService>();
            })
            .Build()
            .Run();
    }
}