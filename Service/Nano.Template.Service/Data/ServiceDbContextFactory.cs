using Nano.Data;
using Nano.Data.MySql;

namespace Nano.Template.Service.Data;

/// <inheritdoc />
public class ServiceDbContextFactory : BaseDbContextFactory<MySqlProvider, ServiceDbContext>;