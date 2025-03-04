using System;
using Lib.Sms.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twilio.Clients;

namespace Lib.Sms.Extensions;

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Sms to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSms(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services
            .AddConfigOptions<SmsOptions>(SmsOptions.SectionName, out var options);

        services
            .AddSingleton<ITwilioRestClient>(new TwilioRestClient(options.AccountId, options.AuthToken));

        services
            .AddSingleton<ISmsService, SmsService>();

        return services;
    }

    private static IServiceCollection AddConfigOptions<TOption>(this IServiceCollection services, string name, out TOption options)
        where TOption : class, new()
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (name == null)
            throw new ArgumentNullException(nameof(name));

        var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        var section = configuration.GetSection(name);

        options = section.Get<TOption>() ?? new TOption();

        services
            .AddSingleton(options)
            .Configure<TOption>(section);

        return services;
    }
}