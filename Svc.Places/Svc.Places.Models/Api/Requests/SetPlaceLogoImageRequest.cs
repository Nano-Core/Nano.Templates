using System;
using Microsoft.AspNetCore.Http;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Set Place Logo Image Request.
/// </summary>
[PostAction("{id}/logo/set")]
public class SetPlaceLogoImageRequest : BaseRequest
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
    public SetPlaceLogoImageRequest()
    {
        this.Controller = $"{nameof(Place)}s";
    }
}