using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;

namespace Svc.Accounts.Models.Api.Requests;

/// <summary>
/// User Details By Email Request.
/// </summary>
[GetAction("details/email")]
public class UserDetailsByEmailRequest : BaseRequest
{
    /// <summary>
    /// Email Address.
    /// </summary>
    [Query]
    public virtual string EmailAddress { get; set; } = null!;
}