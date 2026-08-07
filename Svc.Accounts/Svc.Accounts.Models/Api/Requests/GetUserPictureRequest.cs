using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Accounts.Models.Api.Requests.Enums;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Models.Api.Requests;

/// <summary>
/// Get User Picture Request.
/// </summary>
[GetAction("{id}/user-picture/{type}")]
public class GetUserPictureRequest : BaseRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Route(Order = 0)]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Type.
    /// </summary>
    [Route(Order = 1)]
    public virtual ImageType Type { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUserPictureRequest()
    {
        this.Controller = $"{nameof(User)}s";
    }
}