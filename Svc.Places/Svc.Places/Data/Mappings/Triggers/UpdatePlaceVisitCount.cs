using EntityFrameworkCore.Triggers;
using Svc.Places.Models.Data;
using System;
using System.Linq;

namespace Svc.Places.Data.Mappings.Triggers;

internal static class UpdatePlaceVisitCount
{
    internal static void Configure(IInsertedEntry<PlaceVisit> entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        var place = entry.Context
            .Set<Place>()
            .FirstOrDefault(y => y.Id == entry.Entity.PlaceId);

        if (place == null)
        {
            return;
        }

        place.LatestVisit = entry.Entity.CreatedAt;

        entry.Context
            .Update(place);
    }
}