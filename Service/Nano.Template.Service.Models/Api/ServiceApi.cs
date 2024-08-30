using System.Threading;
using System.Threading.Tasks;
using Nano.App.Api;
using Nano.Template.Service.Models.Api.Requests;
using Nano.Template.Service.Models.Data;

namespace Nano.Template.Service.Models.Api;

/// <inheritdoc />
public class ServiceApi : DefaultIdentityApi<User>
{
    /// <inheritdoc />
    public ServiceApi(ApiOptions options)
        : base(options)
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