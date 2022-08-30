using Nano.Web.Api;

namespace Nano.Template.Web.Models.Api;

/// <inheritdoc />
public class WebApi : DefaultIdentityApi<User>
{
    /// <inheritdoc />
    public WebApi(ApiOptions options)
        : base(options)
    {

    }
}