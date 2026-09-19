using System;
using Lib.Emailing.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Resend;
using SendGrid;

namespace Lib.Emailing.Extensions;

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add SendGrid Emailing to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSendGridEmailing(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddOptions<EmailingOptions>()
            .BindConfiguration(EmailingOptions.SectionName);

        services
            .AddSingleton(x => x.GetRequiredService<IOptions<EmailingOptions>>().Value);

        services
            .AddScoped<ISendGridClient>(x =>
            {
                var options = x.GetRequiredService<IOptions<EmailingOptions>>().Value;
                return new SendGridClient(options.ApiKey);
            });

        services
            .AddScoped<IEmailingService, SendGridEmailingService>();

        return services;
    }

    /// <summary>
    /// Add Resend Emailing to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddResendEmailing(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddOptions<EmailingOptions>()
            .BindConfiguration(EmailingOptions.SectionName);

        services
            .AddSingleton(x => x.GetRequiredService<IOptions<EmailingOptions>>().Value);

        services
            .AddResend(_ => { });

        services
            .AddOptions<ResendClientOptions>()
            .Configure<IOptions<EmailingOptions>>((resendOptions, emailingOptions) =>
            {
                resendOptions.ApiToken = emailingOptions.Value.ApiKey;
                resendOptions.ThrowExceptions = true;
            });

        services
            .AddScoped<IEmailingService, ResendEmailingService>();

        return services;
    }
}