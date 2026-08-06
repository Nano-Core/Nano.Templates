using System;
using System.Threading;
using System.Threading.Tasks;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Locations.Events;
using Svc.Places.Models.Data;

namespace Svc.Places.Eventing;

/// <inheritdoc />
public class UserLocationChangedEventingHandler(IRepository repository) : BaseEventHandler<UserLocationChangedEvent>
{
    /// <inheritdoc />
    public override async Task CallbackAsync(UserLocationChangedEvent @event, bool isRedelivered, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var place = await repository
            .GetFirstAsync<Place>(x => @event.Coordinate.Intersects(x.Area), cancellationToken);

        var currentPlaceVisit = await repository
            .GetFirstAsync<PlaceVisit>(x => x.LeftAt == null, cancellationToken);

        if (currentPlaceVisit != null && (place == null || place.Id != currentPlaceVisit.PlaceId))
        {
            currentPlaceVisit.LeftAt = @event.CreatedAt;
            currentPlaceVisit.Place!.VisitsCount--;

            await repository
                .UpdateAsync(currentPlaceVisit, cancellationToken);
        }

        if (place != null && (currentPlaceVisit == null || place.Id != currentPlaceVisit.PlaceId))
        {
            await repository
                .AddAsync(new PlaceVisit
                {
                    PlaceId = place.Id,
                    UserId = @event.UserId,
                    EnteredAt = @event.CreatedAt
                }, cancellationToken);

            place.VisitsCount++;

            await repository
                .UpdateAsync(place, cancellationToken);
        }
    }
}