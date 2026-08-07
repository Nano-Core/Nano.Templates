using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Emailing.Models.Data;

namespace Svc.Emailing.Models.Api.Requests;

/// <summary>
/// Add Place Favorite Request.
/// </summary>
[PostAction("send")]
public class SendEmailRequest : BaseRequest
{
    /// <summary>
    /// Email
    /// </summary>
    [Body]
    public virtual Email Email { get; set; } = null!;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SendEmailRequest()
    {
        this.Controller = $"{nameof(Email)}s";
    }
}