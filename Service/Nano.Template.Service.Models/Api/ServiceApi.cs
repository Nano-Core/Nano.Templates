using Microsoft.Extensions.Options;
using Nano.App.ApiClient;
using Nano.App.ApiClient.Config;
using Nano.Template.Service.Models.Api.Requests;
using Nano.Template.Service.Models.Data;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nano.Template.Service.Models.Api;

/// <inheritdoc />
public class ServiceApi : DefaultIdentityApi<User>
{
    /// <inheritdoc />
    public ServiceApi(IOptionsMonitor<ApiOptions> options, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        : base(options, httpClient, httpContextAccessor)
    {
    }

    /// <summary>
    /// Samples Custom
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    public virtual Task SamplesCustom(CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync(new SamplesCustomRequest(), cancellationToken);
    }
}