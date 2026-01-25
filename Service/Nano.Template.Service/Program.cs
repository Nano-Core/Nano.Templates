using Microsoft.Extensions.DependencyInjection;
using Nano.App.Api;
using Nano.Data.Extensions;
using Nano.Data.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.RabbitMq;
using Nano.Logging.Extensions;
using Nano.Logging.Serilog;
using Nano.Storage.AzureShare;
using Nano.Storage.Extensions;
using Nano.Template.Service.Data;
using Nano.Template.Service.Services;
using Nano.Template.Service.Services.Interfaces;

NanoApiApplication
    .ConfigureApp()
    .ConfigureServices(x =>
    {
        x.AddNanoLogging<SerilogProvider>();
        x.AddNanoData<MySqlProvider, ServiceDbContext>();
        x.AddNanoEventing<RabbitMqProvider>();
        x.AddNanoStorage<AzureFileshareProvider>();

        x.AddSingleton<ISampleService, SampleService>();
    })
    .Build()
    .Run();
