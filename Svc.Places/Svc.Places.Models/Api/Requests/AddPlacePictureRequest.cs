using Microsoft.AspNetCore.Http;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Places.Models.Data;
using System;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Add Place Picture Request.
/// </summary>
[PostAction("add/{placeId}")]
public class AddPlacePictureRequest : BaseRequest
{
    /// <summary>
    /// Place Id.
    /// </summary>
    [Route]
    public virtual Guid PlaceId { get; set; }

    /// <summary>
    /// File.
    /// </summary>
    [Form]
    public virtual IFormFile File { get; set; } = null!;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AddPlacePictureRequest()
    {
        this.Controller = $"{nameof(PlacePicture)}s";
    }
}