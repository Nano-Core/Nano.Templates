using Nano.App.Api;
using Nano.Data.Extensions;
using Nano.Data.MySql;
using Nano.Eventing.Extensions;
using Nano.Eventing.RabbitMq;
using Nano.Logging.Extensions;
using Nano.Logging.Serilog;
using Svc.Emailing.Data;
using Lib.Emailing.Extensions;

NanoApiApplication
    .ConfigureApp()
    .ConfigureServices(x =>
    {
        x.AddNanoLogging<SerilogProvider>();
        x.AddNanoData<MySqlProvider, EmailingDbContext>();
        x.AddNanoEventing<RabbitMqProvider>();

        x.AddResendEmailing();
    })
    .Build()
    .Run();
