using Nano.Data;
using Nano.Data.MySql;

namespace Svc.Emailing.Data;

/// <inheritdoc />
public class EmailingDbContextFactory : BaseDbContextFactory<MySqlProvider, EmailingDbContext>;