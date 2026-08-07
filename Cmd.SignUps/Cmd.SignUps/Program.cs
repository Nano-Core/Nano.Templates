using Nano.App.Console;
using Nano.Logging.Extensions;
using Nano.Logging.Serilog;

NanoConsoleApplication
    .ConfigureApp(args)
    .ConfigureServices(x =>
    {
        x.AddNanoLogging<SerilogProvider>();
    })
    .Build()
    .Run();