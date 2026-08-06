using System;
using System.IO;
using Nano.Storage.Abstractions;

namespace Svc.Places.Extensions;

internal static class PathProviderExtensions
{
    private const string ROOT_PLACES_PATH = "places";

    internal static string GetPlacesPath(this IPathProvider pathProvider, Guid placeId)
    {
        ArgumentNullException.ThrowIfNull(pathProvider);

        return Path.Combine(pathProvider.Root, PathProviderExtensions.ROOT_PLACES_PATH, placeId.ToString());
    }
}