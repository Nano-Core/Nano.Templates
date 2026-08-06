using Nano.App.Api;
using Nano.Data.Extensions;
using Nano.Data.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.RabbitMq;
using Nano.Logging.Extensions;
using Nano.Logging.Serilog;
using Svc.Locations.Data;

NanoApiApplication
    .ConfigureApp()
    .ConfigureServices(x =>
    {
        x.AddNanoLogging<SerilogProvider>();
        x.AddNanoData<MySqlProvider, LocationsDbContext>();
        x.AddNanoEventing<RabbitMqProvider>();
    })
    .Build()
    .Run();
