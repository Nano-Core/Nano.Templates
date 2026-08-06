using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Models.Api.Requests;

/// <summary>
/// Remove User Picture Request.
/// </summary>
[DeleteAction("{id}/user-picture/remove")]
public class RemoveUserPictureRequest : BaseRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Route]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemoveUserPictureRequest()
    {
        this.Controller = $"{nameof(User)}s";
    }
}