using Microsoft.AspNetCore.Hosting;
using Nano.Logging.Extensions;
using Nano.Logging.Providers.Serilog;
using Nano.Web;

namespace Nano.Template.Api;

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
            })
            .Build()
            .Run();
    }
}