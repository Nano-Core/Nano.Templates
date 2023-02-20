using System.Net.Http;
using Nano.Web.Api;

namespace Nano.Template.Web.Models.Api;

/// <inheritdoc />
public class WebApi : DefaultIdentityApi<User>
{
    /// <inheritdoc />
    public WebApi(HttpClient httpClient, ApiOptions options)
        : base(httpClient, options)
    {

    }
}