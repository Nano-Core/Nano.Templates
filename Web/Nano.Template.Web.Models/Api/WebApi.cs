using Nano.Web.Api;
using System.Threading.Tasks;
using System.Threading;
using Nano.Template.Web.Models.Api.Requests;

namespace Nano.Template.Web.Models.Api;

/// <inheritdoc />
public class WebApi : DefaultIdentityApi<User>
{
    /// <inheritdoc />
    public WebApi(ApiOptions options)
        : base(options)
    {

    }

    /// <summary>
    /// Samples Custom
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    public virtual async Task SamplesCustom(CancellationToken cancellationToken = default)
    {
        await this.InvokeAsync(new SamplesCustomRequest(), cancellationToken);
    }
}