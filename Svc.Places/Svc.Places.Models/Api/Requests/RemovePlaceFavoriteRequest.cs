using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Remove Place Favorite Request.
/// </summary>
[DeleteAction("{placeId}/remove/{userId}")]
public class RemovePlaceFavoriteRequest : BaseRequest
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

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemovePlaceFavoriteRequest()
    {
        this.Controller = $"{nameof(PlaceFavorite)}s";
    }
}