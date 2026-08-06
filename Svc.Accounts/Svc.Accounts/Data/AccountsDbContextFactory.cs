using Nano.Data;
using Nano.Data.MySql;

namespace Svc.Accounts.Data;

/// <inheritdoc />
public class AccountsDbContextFactory : BaseDbContextFactory<MySqlProvider, AccountsDbContext>;