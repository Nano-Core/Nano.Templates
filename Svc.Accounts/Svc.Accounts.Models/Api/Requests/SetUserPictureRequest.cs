using System;
using Microsoft.AspNetCore.Http;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Accounts.Models.Data;

namespace Svc.Accounts.Models.Api.Requests;

/// <summary>
/// Add User Picture Request.
/// </summary>
[PostAction("{id}/user-picture/set")]
public class SetUserPictureRequest : BaseRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Route]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// File.
    /// </summary>
    [Form]
    public virtual IFormFile File { get; set; } = null!;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SetUserPictureRequest()
    {
        this.Controller = $"{nameof(User)}s";
    }
}