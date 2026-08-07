using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Add Place Favorite Request.
/// </summary>
[PostAction("{placeId}/add/{userId}")]
public class AddPlaceFavoriteRequest : BaseRequest
{
    /// <summary>
    /// Place Id.
    /// </summary>
    [Route(Order = 0)]
    public virtual Guid PlaceId { get; set; }

    /// <summary>
    /// User Id.
    /// </summary>
    [Route(Order = 1)]
    public virtual Guid UserId { get; set; }
}