using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Get Place Logo File Request.
/// </summary>
[GetAction("{id}/logo")]
public class GetPlaceLogoImageFileRequest : BaseRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Route]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetPlaceLogoImageFileRequest()
    {
        this.Controller = $"{nameof(Place)}s";
    }
}