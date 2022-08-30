using Nano.Data;
using Nano.Data.Providers.SqlServer; 
//using Nano.Data.Providers.MySql;

namespace Nano.Template.Console.Data;

/// <inheritdoc />
public class ConsoleDbContextFactory : BaseDbContextFactory<SqlServerProvider /*MySqlProvider*/, ConsoleDbContext>
{

}