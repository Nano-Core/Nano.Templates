using Nano.Data;
using Nano.Data.Providers.MySql;

namespace Nano.Template.Service.Data;

/// <inheritdoc />
public class ServiceContextFactory : BaseDbContextFactory<MySqlProvider, ServiceDbContext>;