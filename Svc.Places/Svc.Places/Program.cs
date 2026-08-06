using Nano.App.Api;
using Nano.Data.Extensions;
using Nano.Data.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.RabbitMq;
using Nano.Logging.Extensions;
using Nano.Logging.Serilog;
using Nano.Storage.Azure;
using Nano.Storage.Extensions;
using Svc.Places.Data;

NanoApiApplication
    .ConfigureApp()
    .ConfigureServices(x =>
    {
        x.AddNanoLogging<SerilogProvider>();
        x.AddNanoData<MySqlProvider, PlacesDbContext>();
        x.AddNanoEventing<RabbitMqProvider>();
        x.AddNanoStorage<AzureFileshareProvider>();
    })
    .Build()
    .Run();
