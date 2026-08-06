using Api.Public.Consts;
using Api.Public.Controllers;
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
            .AddPolicy(CustomAuthorizationPolicies.PUBLIC_USER, policy =>
            {
                policy
                    .RequireClaim(CustomClaimTypes.IS_PUBLIC, "true");
            });
    })
    .Build()
    .Run();
