using Nano.Data;
using Nano.Data.MySql;

namespace Svc.Locations.Data;

/// <inheritdoc />
public class LocationsDbContextFactory : BaseDbContextFactory<MySqlProvider, LocationsDbContext>;