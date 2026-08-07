using System;
using Nano.App.ApiClient.Annotations;
using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Api.Requests;

/// <summary>
/// Remove Place Logo Image Request.
/// </summary>
[DeleteAction("{id}/logo/remove")]
public class RemovePlaceLogoImageRequest : BaseRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    [Route]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public RemovePlaceLogoImageRequest()
    {
        this.Controller = $"{nameof(Place)}s";
    }
}