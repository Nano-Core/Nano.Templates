using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nano.App.ApiClient;
using Nano.App.ApiClient.Models;
using Svc.Places.Models.Api.Requests;
using Svc.Places.Models.Data;

namespace Svc.Places.Models.Api;

/// <inheritdoc />
public class PlacesApi : BaseApiClient
{
    /// <inheritdoc />
    public PlacesApi(ApiClient apiClient)
        : base(apiClient)
    {
    }

    /// <summary>
    /// Get Place Logo Image.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The place logo image stream.</returns>
    public virtual Task<NamedStream?> GetPlaceLogoImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync<GetPlaceLogoImageFileRequest, NamedStream>(new GetPlaceLogoImageFileRequest
        {
            Id = id
        }, cancellationToken);
    }

    /// <summary>
    /// Set Place Logo Image.
    /// </summary>
    /// <param name="placeId">The place id.</param>
    /// <param name="file">The image file.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    public virtual Task SetPlaceLogoImageAsync(Guid placeId, IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        return this.InvokeAsync(new SetPlaceLogoImageRequest
        {
            Id = placeId,
            File = file
        }, cancellationToken);
    }

    /// <summary>
    /// Remove Place Logo Image.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    public virtual Task RemovePlaceLogoImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync(new RemovePlaceLogoImageRequest
        {
            Id = id
        }, cancellationToken);
    }

    /// <summary>
    /// Get Place Picture.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The picture stream.</returns>
    public virtual Task<NamedStream?> GetPlacePictureAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync<GetPlacePictureFileRequest, NamedStream>(new GetPlacePictureFileRequest
        {
            Id = id
        }, cancellationToken);
    }

    /// <summary>
    /// Add Place Picture.
    /// </summary>
    /// <param name="placeId">The place id.</param>
    /// <param name="file">The picture file.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="PlacePicture"/>.</returns>
    public virtual Task<PlacePicture?> AddPlacePictureAsync(Guid placeId, IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        return this.InvokeAsync<AddPlacePictureRequest, PlacePicture>(new AddPlacePictureRequest
        {
            PlaceId = placeId,
            File = file
        }, cancellationToken);
    }

    /// <summary>
    /// Remove Place Picture.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    public virtual Task RemovePlacePictureAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync(new RemovePlacePictureRequest
        {
            Id = id
        }, cancellationToken);
    }

    /// <summary>
    /// Add Place Favorite.
    /// </summary>
    /// <param name="placeId">The place id.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="PlaceFavorite"/>.</returns>
    public virtual Task<PlaceFavorite?> AddPlaceFavoriteAsync(Guid placeId, Guid userId, CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync<AddPlaceFavoriteRequest, PlaceFavorite>(new AddPlaceFavoriteRequest
        {
            PlaceId = placeId,
            UserId = userId
        }, cancellationToken);
    }

    /// <summary>
    /// Remove Place Favorite.
    /// </summary>
    /// <param name="placeId">The place id.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    public virtual Task RemovePlaceFavoriteAsync(Guid placeId, Guid userId, CancellationToken cancellationToken = default)
    {
        return this.InvokeAsync(new RemovePlaceFavoriteRequest
        {
            PlaceId = placeId,
            UserId = userId
        }, cancellationToken);
    }
}