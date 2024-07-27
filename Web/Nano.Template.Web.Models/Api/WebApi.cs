using System.Threading.Tasks;
using System.Threading;
using Nano.App.Api;
using Nano.Template.Web.Models.Api.Requests;
using Nano.Template.Web.Models.Data;

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
    public virtual Task SamplesCustom(CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync(new SamplesCustomRequest(), cancellationToken);
    }
}