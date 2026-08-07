using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Get Place Picture File Request.
/// </summary>
[GetAction("{id}/file")]
public class GetPlacePictureFileRequest : BaseRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Route]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetPlacePictureFileRequest()
    {
        this.Controller = $"{nameof(PlacePicture)}s";
    }
}