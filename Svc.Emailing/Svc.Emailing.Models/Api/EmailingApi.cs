using Nano.App.ApiClient;
using Svc.Emailing.Models.Data;
using System.Threading;
using System.Threading.Tasks;
using Svc.Emailing.Models.Api.Requests;

namespace Svc.Emailing.Models.Api;

/// <inheritdoc />
public class EmailingApi(ApiClient apiClient) : BaseApiClient(apiClient)
{
    /// <summary>
    /// Send Email Async.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Nothing.</returns>
    public virtual Task SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync(new SendEmailRequest
        {
            Email = email
        }, cancellationToken);
    }
}