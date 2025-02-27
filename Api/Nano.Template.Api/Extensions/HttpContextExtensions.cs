using System;
using Microsoft.AspNetCore.Http;

namespace Nano.Template.Api.Extensions;

/// <summary>
/// Http Context Extensions.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Get Base Web Uri.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
    /// <returns>The <see cref="Uri"/>.</returns>
    public static Uri GetBaseWebUri(this HttpContext httpContext)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        var host = httpContext.Request.Host.Host;
        var indexOfDot = host.IndexOf('.');
        var baseHost = host[(indexOfDot + 1)..];
        var webHost = $"https://app.{baseHost}";

        return new Uri(webHost);
    }
}