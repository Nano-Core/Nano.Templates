using Api.Admin.Consts;
using Microsoft.Extensions.DependencyInjection;
using Nano.App.Api;
using Nano.Logging.Extensions;
using Nano.Logging.Serilog;

NanoApiApplication
    .ConfigureApp()
    .ConfigureServices(x =>
    {
        x.AddNanoLogging<SerilogProvider>();

        x.AddAuthorizationBuilder()
            .AddPolicy(CustomAuthorizationPolicies.SYSTEM_ADMIN, policy =>
            {
                policy
                    .RequireClaim(CustomClaimTypes.IS_ADMIN, "true");
            });
    })
    .Build()
    .Run();
