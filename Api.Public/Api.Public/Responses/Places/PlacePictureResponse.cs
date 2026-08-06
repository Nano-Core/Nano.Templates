using System;
using Svc.Places.Models.Data;

namespace Api.Public.Responses.Places;

/// <summary>
/// Place Picture Response.
/// </summary>
public class PlacePictureResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Order Index.
    /// </summary>
    public int OrderIndex { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="placePicture">The <see cref="PlacePicture"/>.</param>
    public PlacePictureResponse(PlacePicture placePicture)
    {
        ArgumentNullException.ThrowIfNull(placePicture);

        this.Id = placePicture.Id;
        this.OrderIndex = placePicture.OrderIndex;
    }
}