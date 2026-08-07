using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Remove Place Picture Request.
/// </summary>
[DeleteAction("{id}/remove")]
public class RemovePlacePictureRequest : BaseRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Route]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemovePlacePictureRequest()
    {
        this.Controller = $"{nameof(PlacePicture)}s";
    }
}