using Nano.Data;
using Nano.Data.MySql;

namespace Svc.Places.Data;

/// <inheritdoc />
public class PlacesDbContextFactory : BaseDbContextFactory<MySqlProvider, PlacesDbContext>;